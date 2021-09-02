using ClingoSharp.Enums;
using ClingoSharp.NativeWrapper.Callbacks;
using ClingoSharp.NativeWrapper.Enums;
using ClingoSharp.NativeWrapper.Interfaces.Modules;
using ClingoSharp.NativeWrapper.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using static ClingoSharp.NativeWrapper.Enums.SolveEventType;
using static ClingoSharp.NativeWrapper.Enums.SolveMode;

namespace ClingoSharp
{
    /// <summary>
    /// Control object for the grounding/solving process.
    /// </summary>
    public sealed class Control : IDisposable
    {
        #region Attributes

        private static IControl m_controlModule = null;
        private IntPtr m_clingoControl;

        // Track whether Dispose has been called.
        private bool disposed = false;

        #endregion

        #region Class Properties
        
        internal static IControl ControlModule
        {
            get
            {
                if (m_controlModule == null)
                    m_controlModule = Clingo.ClingoRepository.GetModule<IControl>();

                return m_controlModule;
            }
        }
        
        #endregion

        #region Instance Properties

        public SymbolicAtoms SymbolicAtoms
        {
            get
            {
                Clingo.HandleClingoError(ControlModule.GetSymbolicAtoms(m_clingoControl, out IntPtr symbolicAtoms));
                return new SymbolicAtoms(symbolicAtoms);
            }
        }

        #endregion

        #region Constructors

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
                MessageCode messageCode = Enumeration.GetValue<MessageCode>((int)code);

                if (logger != null)
                {
                    logger(messageCode, message);
                }
                else
                {
                    Clingo.HandleClingoWarning(code, message);
                }
            }

            Clingo.HandleClingoError(ControlModule.New(args?.ToArray(), loggerCallback, Convert.ToUInt32(messageLimit), out IntPtr controlPtr));

            m_clingoControl = controlPtr;
        }

        #endregion Constructors

        #region Destructors

        ~Control()
        {
            Dispose(false);
        }

        #endregion

        #region Class Methods

        public static implicit operator IntPtr(Control control)
        {
            return control.m_clingoControl;
        }

        #endregion

        #region Instance Methods

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed && m_clingoControl != IntPtr.Zero)
            {
                ControlModule.Free(m_clingoControl);
                m_clingoControl = IntPtr.Zero;
                disposed = true;
            }

            if (disposing)
                GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Extend the logic program with the given non-ground logic program in string form.
        /// </summary>
        /// <param name="name">The name of program block to add.</param>
        /// <param name="parameters">The parameters of the program block to add.</param>
        /// <param name="program">The non-ground program in string form.</param>
        public void Add(string name, List<string> parameters, string program)
        {
            Clingo.HandleClingoError(ControlModule.Add(m_clingoControl, name, parameters?.ToArray(), program));
        }

        public Symbol GetConst(string name)
        {
            Clingo.HandleClingoError(ControlModule.GetConst(m_clingoControl, name, out ulong symbol));
            return new Symbol(symbol);
        }

        public void Load(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException(filename);

            Clingo.HandleClingoError(ControlModule.Load(m_clingoControl, filename));
        }

        /// <summary>
        /// Ground the given list of program parts specified by tuples of names and arguments.
        /// </summary>
        /// <param name="parts">List of tuples of program names and program arguments to ground.</param>
        public void Ground(List<Tuple<string, List<Symbol>>> parts, object context = null)
        {
            // Creates a wrapper from the context object to a ground callback
            int groundCallback(Location[] location, string name, ulong[] arguments, UIntPtr argumentsSize, IntPtr data, SymbolCallback symbolCallback, IntPtr symbolCallbackData)
            {
                try
                {
                    // Get the method
                    MethodInfo method = context.GetType().GetMethod(name);
                    object result = method.Invoke(context, arguments.Cast<object>().ToArray());

                    // Convert return value in symbol object
                    var newSymbols = new List<Symbol>();
                    if (result is Symbol)
                    {

                        newSymbols.Add((Symbol)result);
                    }
                    else if (result is IEnumerable<Symbol>)
                    {
                        foreach (var symbol in result as IEnumerable<Symbol>)
                        {
                            newSymbols.Add(symbol);
                        }
                    }
                    else
                    {
                        return 0;
                    }
                    
                    return symbolCallback(newSymbols.Select(symbol => (ulong)symbol).ToArray(), new UIntPtr(Convert.ToUInt32(newSymbols.Count)), symbolCallbackData);
                }
                catch
                {
                    return 0;
                }
            }

            // Converts the tuple list into a Part array
            Part[] partArray = parts?.Select(part => new Part
            {
                name = part.Item1,
                params_list = part.Item2.Select(symbol => (ulong)symbol).ToArray(),
                size = new UIntPtr(Convert.ToUInt32(part.Item2.Count))
            }).ToArray();

            Clingo.HandleClingoError(ControlModule.Ground(m_clingoControl, partArray, (context != null) ? (GroundCallback)groundCallback : null));
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
            int notifyCallback(SolveEventType type, IntPtr eventPtr, IntPtr data, ref int goon)
            {
                switch (type)
                {
                    case clingo_solve_event_type_model:
                        if (onModel != null)
                        {
                            var model = new Model(eventPtr);
                            var result = onModel(model);

                            goon = result ? 1 : 0;
                        }

                        break;

                    case clingo_solve_event_type_statistics:
                        if (onStatistics != null)
                        {
                            IntPtr[] statistics = new IntPtr[2];
                            Marshal.Copy(eventPtr, statistics, 0, 2);

                            var stepStatistics = new StatisticsMap();
                            var acumulatedStatistics = new StatisticsMap();
                            onStatistics(stepStatistics, acumulatedStatistics);
                        }

                        break;

                    case clingo_solve_event_type_finish:
                        if (onFinish != null)
                        {
                            var solveResult = new SolveResult();
                            onFinish(solveResult);
                        }

                        break;
                }

                return 1;
            }

            // Converts the asumptions into a literals list
            var symbolicAtoms = SymbolicAtoms;
            List<int> literals = new List<int>();
            if (assumptions != null)
            {
                foreach (var assumption in assumptions)
                {
                    if (assumption.IsType<int>())
                    {
                        literals.Add(assumption.Get<int>());
                    }
                    else
                    {
                        var symbolicLiteral = assumption.Get<Tuple<Symbol, bool>>();
                        if (symbolicAtoms.TryGetValue(symbolicLiteral.Item1, out SymbolicAtom symbolicAtom))
                        {
                            var literal = symbolicAtom.Literal;
                            if (!symbolicLiteral.Item2) literal = -literal;
                            literals.Add(literal);
                        }
                        else if (symbolicLiteral.Item2)
                        {
                            literals.Add(1);
                            literals.Add(-1);
                        }
                    }
                }
            }

            SolveMode mode = clingo_solve_mode_none;
            if (yield) { mode |= clingo_solve_mode_yield; }
            if (async) { mode |= clingo_solve_mode_async; }

            Clingo.HandleClingoError(ControlModule.Solve(this, mode, literals.ToArray(), (onModel == null) && (onStatistics == null) && (onFinish == null) ? null : (SolveEventCallback)notifyCallback, out IntPtr handlePtr));

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
