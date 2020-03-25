using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Callbacks;
using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.EventHandlers;
using ClingoSharp.CoreServices.Interfaces.Modules;
using ClingoSharp.CoreServices.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ClingoSharp
{
    /// <summary>
    /// Control object for the grounding/solving process.
    /// </summary>
    public class Control : IDisposable
    {
        #region Attributes

        private static readonly IControlModule m_module;
        private CoreServices.Types.Control m_clingoControl;
        // Track whether Dispose has been called.
        private bool disposed = false;

        #endregion

        #region Constructors

        static Control()
        {
            m_module = Repository.GetModule<IControlModule>();
        }

        /// <summary>
        /// Create a new <see cref="Control"/> object
        /// </summary>
        /// <param name="args">Arguments to the grounder and solver.</param>
        /// <param name="logger">Function to intercept messages normally printed to standard error.</param>
        /// <param name="messageLimit">The maximum number of messages passed to the logger.</param>
        public Control(List<string> args = null, Action<MessageCode, string> logger = null, int messageLimit = 20)
        {
            void loggerCallback(WarningCode code, string message, IntPtr data)
            {
                if (logger != null)
                {
                    MessageCode messageCode = Enums.Enumeration.GetValue((int)code) as MessageCode;
                    logger(messageCode, message);
                }
            }

            Clingo.HandleClingoError(m_module.New(args?.ToArray(), loggerCallback, Convert.ToUInt32(messageLimit), out CoreServices.Types.Control controlPtr));

            m_clingoControl = controlPtr;
        }

        #endregion Constructors

        #region Destructors

        ~Control()
        {
            Dispose(false);
        }

        #endregion

        #region Instance Methods

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                m_module.Free(m_clingoControl);
                m_clingoControl = null;
                disposed = true;
            }
        }

        /// <summary>
        /// Extend the logic program with the given non-ground logic program in string form.
        /// </summary>
        /// <param name="name">The name of program block to add.</param>
        /// <param name="parameters">The parameters of the program block to add.</param>
        /// <param name="program">The non-ground program in string form.</param>
        public void Add(string name, List<string> parameters, string program)
        {
            Clingo.HandleClingoError(m_module.Add(m_clingoControl, name, parameters?.ToArray(), program));
        }

        /// <summary>
        /// Ground the given list of program parts specified by tuples of names and arguments.
        /// </summary>
        /// <param name="parts">List of tuples of program names and program arguments to ground.</param>
        public void Ground(List<Tuple<string, List<Symbol>>> parts, object context = null)
        {
            // Creates a wrapper from the context object to a ground callback
            bool groundCallback(Location location, string name, CoreServices.Types.Symbol[] arguments, SymbolCallback symbolCallback)
            {
                try
                {
                    // Get the method
                    MethodInfo method = context.GetType().GetMethod(name);
                    object result = method.Invoke(context, arguments);

                    // Convert return value in symbol object
                    var newSymbols = new List<CoreServices.Types.Symbol>();
                    if (result is Symbol)
                    {

                        newSymbols.Add(((Symbol)result).m_clingoSymbol);
                    }
                    else if (result is IEnumerable<Symbol>)
                    {
                        foreach (var symbol in (result as IEnumerable<Symbol>))
                        {
                            newSymbols.Add(symbol.m_clingoSymbol);
                        }
                    }
                    else
                    {
                        return false;
                    }
                    
                    return symbolCallback(newSymbols.ToArray());
                }
                catch
                {
                    return false;
                }
            }

            // Converts the tuple list into a Part array
            Part[] partArray = parts == null ? null : parts.Select(p => new Part()
            {
                Name = p.Item1,
                Params = p.Item2.Select(s => s.m_clingoSymbol).ToArray()
            }).ToArray();

            Clingo.HandleClingoError(m_module.Ground(m_clingoControl, partArray, (context != null) ? (GroundCallback)groundCallback : null));
        }

        /// <summary>
        /// Starts a search
        /// </summary>
        /// <param name="assumptions">List of (atom, boolean) tuples or program literals that serve as assumptions for the solve call, e.g., solving under assumptions <c>[(Function("a"), True)]</c> only admits answer sets that contain atom <c>a</c>.</param>
        /// <param name="onModel">Optional callback for intercepting models. A <see cref="Model"/> object is passed to the callback. The search can be interruped from the model callback by returning <see cref="false"/>.</param>
        /// <param name="onStatistics">Optional callback to update statistics. The step and accumulated statistics are passed as arguments.</param>
        /// <param name="onFinish">Optional callback called once search has finished. A <see cref="SolveResult"/> also indicating whether the solve call has been interrupted is passed to the callback.</param>
        /// <param name="yield">The resulting <see cref="SolveHandle"/> is iterable yielding <see cref="Model"/> objects.</param>
        /// <param name="async">The solve call and the method <see cref="SolveHandle.Resume"/> of the returned handle are non-blocking.</param>
        /// <returns>The return value depends on the parameters. If either yield or async is true, then a <see cref="SolveHandle"/> is returned. Otherwise, a <see cref="SolveResult"/> is returned.</returns>
        public Union<SolveHandle, SolveResult> Solve(List<Union<Tuple<Symbol, bool>, int>> assumptions = null, Func<Model, bool> onModel = null, Action<StatisticsMap, StatisticsMap> onStatistics = null, Action<SolveResult> onFinish = null, bool yield = false, bool async = false)
        {
            // Creates a wrapper between the argument callbacks and the solve event notify callback
            bool notifyCallback(SolveEventType type, IntPtr eventPtr, out bool goon)
            {
                goon = true;

                try
                {
                    switch (type)
                    {
                        case SolveEventType.Model:
                            if (onModel != null)
                            {
                                var model = new Model(new CoreServices.Types.Model() { Object = eventPtr });
                                goon = onModel(model);
                            }

                            return true;

                        case SolveEventType.Statistics:
                            if (onStatistics != null)
                            {
                                IntPtr[] statistics = new IntPtr[2];
                                Marshal.Copy(eventPtr, statistics, 0, 2);

                                var stepStatistics = new StatisticsMap();
                                var acumulatedStatistics = new StatisticsMap();
                                onStatistics(stepStatistics, acumulatedStatistics);
                            }

                            return true;

                        case SolveEventType.Finish:
                            if (onFinish != null)
                            {
                                var solveResult = new SolveResult();
                                onFinish(solveResult);
                            }

                            return true;

                        default:
                            return false;
                    }
                }
                catch
                {
                    // If a (non-recoverable) clingo API function fails in this callback, it must return false.
                    return false;
                }
            }

            // Converts the asumptions into a Literal array
            Literal[] literalArray = assumptions == null ? null : assumptions.Select(a => new Literal()
            {
                ///Value = a.GetLiteralValue()
            }).ToArray();

            SolveMode mode = SolveMode.None;
            if (yield) { mode |= SolveMode.Yield; }
            if (async) { mode |= SolveMode.Async; }

            Clingo.HandleClingoError(m_module.Solve(m_clingoControl, mode, null, (onModel == null) && (onStatistics == null) && (onFinish == null) ? null : (SolveEventHandler)notifyCallback, out CoreServices.Types.SolveHandle handlePtr));

            var handle = new SolveHandle(handlePtr);

            // If neither yield nor async is true, returns a SolveResult
            if (!yield && !async)
            {
                return handle.Get();
            }

            return handle;
        }

        #endregion
    }
}
