using ClingoSharp.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ClingoSharp
{
    public class Control
    {
        private IntPtr _c_control;
        internal IntPtr CControlPointer { get { return _c_control; } }

        public delegate void Logger(MessageCode code, string message);

        /// <summary>
        /// Control object to the grounding/solving process.
        /// </summary>
        /// <param name="arguments">Arguments to the grounder and solver</param>
        /// <param name="logger">Function to intercept messages normally printed to standard error</param>
        /// <param name="messageLimit">The maximum number of messages passed to the logger</param>
        public Control(List<string> arguments = null, Logger logger = null, int messageLimit = 20)
        {
            try
            {
                // Wrappers the clingo logger function
                void LoggerWrapper(CClingo.Warning code, string message, IntPtr data)
                {
                    if (logger != null)
                    {
                        MessageCode codeEnum = MessageCode.Unknown;
                        if (Enum.IsDefined(typeof(MessageCode), code))
                        {
                            codeEnum = (MessageCode)code;
                        }

                        logger(codeEnum, message);
                    }
                }

                // Creates pointers
                string[] argsPtr = (arguments != null) ? arguments.ToArray() : Array.Empty<string>();
                UIntPtr argsSize = new UIntPtr((uint)argsPtr.Length);
                IntPtr[] controlPtr = new IntPtr[1];

                // Creates control object
                CClingo.CallProcedureModule("Control.New", () => CControl.New(argsPtr, argsSize, LoggerWrapper, IntPtr.Zero, (uint)messageLimit, controlPtr));
                _c_control = controlPtr[0];
            }
            catch (CClingo.CClingoError err)
            {
                throw new ClingoException(err.Message);
            }
        }

        ~Control()
        {
            Console.WriteLine("Free");
            CControl.Free(_c_control);
        }

        /// <summary>
        /// Extend the logic program with the given non-ground logic program in string form.
        /// </summary>
        /// <param name="name">The name of program block to add</param>
        /// <param name="parameters">The parameters of the program block to add</param>
        /// <param name="program">The non-ground program in string form</param>
        public void Add(string name, List<string> parameters, string program)
        {
            try
            {
                // Creates pointers
                string[] paramsPtr = (parameters != null) ? parameters.ToArray() : Array.Empty<string>();
                UIntPtr paramsSize = new UIntPtr((uint)paramsPtr.Length);

                // Call add procedure
                CClingo.CallProcedureModule("Control.Add", () => CControl.Add(_c_control, name, paramsPtr, paramsSize, program));
            }
            catch (CClingo.CClingoError err)
            {
                throw new ClingoException(err.Message);
            }
        }

        /// <summary>
        /// Ground the given list of program parts specified by tuples of names and arguments.
        /// </summary>
        /// <param name="parts">List of tuples of program names and program arguments to ground.</param>
        public void Ground(List<Tuple<string, List<Symbol>>> parts)
        {
            try
            {
                // Wrappers the clingo ground_callback
                int GroundCallbackWrapper(CClingo.Location location, string name, int[] arguments, UIntPtr arguments_size, UIntPtr data, CSymbolicAtoms.SymbolCallback symbol_callback, UIntPtr symbol_callback_data)
                {
                    Console.WriteLine(string.Format("ground wrapper: {0}, {1}", name, arguments));
                    return 1;
                }

                // Creates the list of program parts
                CControl.Part[] partsPtr = (parts != null) ? 
                    parts.Select(p => {
                        ulong[] paramList = (p.Item2 != null) ? p.Item2.Select(s => s.CSymbol).ToArray() : Array.Empty<ulong>();

                        return new CControl.Part()
                        {
                            Name = p.Item1,
                            Params = paramList,
                            Size = new UIntPtr((uint)paramList.Length)
                        };
                    }).ToArray() 
                : Array.Empty<CControl.Part>();

                // Calls ground procedure
                CClingo.CallProcedureModule("Control.Ground", () => CControl.Ground(_c_control, partsPtr, new UIntPtr((uint)partsPtr.Length), GroundCallbackWrapper, IntPtr.Zero));
            }
            catch (CClingo.CClingoError err)
            {
                throw new ClingoException(err.Message);
            }
        }

        /// <summary>
        /// Starts a search.
        /// </summary>
        /// <param name="assumptions">List of (atom, boolean) tuples or program literals that serve as assumptions for the solve call, e.g., solving under assumptions [(Function("a"), True)] only admits answer sets that contain atom a.</param>
        public void Solve(List<Assumption> assumptions = null, bool async = false)
        {
            try
            {
                // Wrappers the clingo solve event
                int SolveEventWrapper(int eventType, IntPtr eventData, IntPtr data, int[] goon)
                {
                    Console.WriteLine("solve wrapper: " + eventType);
                    return 1;
                }

                // Creates pointers
                IntPtr[] handlerPtr = new IntPtr[1];
                // Calls solve clingo result
                CClingo.CallProcedureModule("Control.Solve", () => CControl.Solve(_c_control, 2, Array.Empty<int>(), UIntPtr.Zero, null, IntPtr.Zero, handlerPtr));

                // Aquí hago tampas
                while (true)
                {
                    CClingo.CallProcedureModule("SolveHandler.Resume", () => CSolveHandle.Resume(handlerPtr[0]));

                    // Gets the model
                    IntPtr[] modelPtr = new IntPtr[1];
                    CClingo.CallProcedureModule("SolveHandler.Model", () => CSolveHandle.Model(handlerPtr[0], modelPtr));

                    if (modelPtr[0] == IntPtr.Zero) break;
                }

                CControl.SolveResult[] solveResultPtr = new CControl.SolveResult[1];
                CClingo.CallProcedureModule("SolveHandler.Get", () => CSolveHandle.Get(handlerPtr[0], solveResultPtr));
                CClingo.CallProcedureModule("SolveHandler.Close", () => CSolveHandle.Close(handlerPtr[0]));
            }
            catch (CClingo.CClingoError err)
            {
                throw new ClingoException(err.Message);
            }
        }
    }
}
