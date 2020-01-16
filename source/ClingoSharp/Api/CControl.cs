using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.Api
{
    /// <summary>
    /// Functions to control the grounding and solving process.
    /// </summary>
    internal static class CControl
    {
        static CControl()
        {
            CClingo.LoadClingoLibrary();
        }

        #region Grounding and Solving

        /// <summary>
        /// Struct used to specify the program parts that have to be grounded.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class Part
        {
            public string Name;
            public ulong[] Params;
            public UIntPtr Size;
        }

        [Flags]
        public enum SolveResult
        {
            Satisfiable = 1,
            Unsatisfiable = 2,
            Exhausted = 4,
            Interrupted = 8
        }

        /// <summary>
        /// Callback function to implement external functions.
        /// </summary>
        /// <param name="location">location from which the external function was called</param>
        /// <param name="name">name of the called external function</param>
        /// <param name="arguments">arguments of the called external function</param>
        /// <param name="arguments_size">number of arguments</param>
        /// <param name="data">user data of the callback</param>
        /// <param name="symbol_callback">function to inject symbols</param>
        /// <param name="symbol_callback_data">user data for the symbol callback (must be passed untouched)</param>
        /// <returns></returns>
        public delegate int GroundCallback(CClingo.Location location, string name, int[] arguments, UIntPtr arguments_size, UIntPtr data, CSymbolicAtoms.SymbolCallback symbol_callback, UIntPtr symbol_callback_data);

        /// <summary>
        /// Create a new control object.
        /// </summary>
        /// <param name="arguments">C string array of command line arguments</param>
        /// <param name="arguments_size">size of the arguments array</param>
        /// <param name="logger">callback functions for warnings and info messages</param>
        /// <param name="logger_data">user data for the logger callback</param>
        /// <param name="message_limit">maximum number of times the logger callback is called</param>
        /// <param name="control">resulting control object</param>
        [DllImport(CClingo.ClingoLib, EntryPoint = "clingo_control_new", CallingConvention = CallingConvention.Cdecl)]
        public static extern int New(string[] arguments, UIntPtr arguments_size, CClingo.LoggerCallback logger, IntPtr logger_data, uint message_limit, [Out] IntPtr[] control);

        /// <summary>
        /// Free a control object created with <see cref="New(string[], UIntPtr, CClingo.LoggerCallback, IntPtr, uint, IntPtr[])"/>.
        /// </summary>
        /// <param name="control">the target.</param>
        [DllImport(CClingo.ClingoLib, EntryPoint = "clingo_control_free", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Free(IntPtr control);

        /// <summary>
        /// Extend the logic program with the given non-ground logic program in string form.
        /// This function puts the given program into a block of form: #program name(parameters).
        /// After extending the logic program, the corresponding program parts are typically grounded with <see cref="Ground(IntPtr, Part[], UIntPtr, GroundCallback, IntPtr)"/>
        /// </summary>
        /// <param name="control"></param>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <param name="parameters_size"></param>
        /// <param name="program"></param>
        /// <returns></returns>
        [DllImport(CClingo.ClingoLib, EntryPoint = "clingo_control_add", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Add(IntPtr control, string name, string[] parameters, UIntPtr parameters_size, string program);

        /// <summary>
        /// Ground the selected parts of the current (non-ground) logic program.
        /// After grounding, logic programs can be solved with clingo_control_solve().
        /// </summary>
        /// <param name="control">the target</param>
        /// <param name="parts">array of parts to ground</param>
        /// <param name="parts_size">size of the parts array</param>
        /// <param name="ground_callback">callback to implement external functions</param>
        /// <param name="ground_callback_data">user data for ground_callback</param>
        /// <returns></returns>
        [DllImport(CClingo.ClingoLib, EntryPoint = "clingo_control_ground", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Ground(IntPtr control, Part[] parts, UIntPtr parts_size, GroundCallback ground_callback, IntPtr ground_callback_data);

        [DllImport(CClingo.ClingoLib, EntryPoint = "clingo_control_solve", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Solve(IntPtr control, int mode, int[] assumptions, UIntPtr assumptions_size, CSolveHandle.SolveEventCallback notify, IntPtr data, [Out] IntPtr[] handler);
            
        #endregion
    }
}
