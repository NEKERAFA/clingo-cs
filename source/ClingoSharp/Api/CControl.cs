using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.API
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
        /// Create a new control object.
        /// </summary>
        /// <param name="arguments">C string array of command line arguments</param>
        /// <param name="arguments_size">size of the arguments array</param>
        /// <param name="logger">callback functions for warnings and info messages</param>
        /// <param name="logger_data">user data for the logger callback</param>
        /// <param name="message_limit">maximum number of times the logger callback is called</param>
        /// <param name="control">resulting control object</param>
        [DllImport(CClingo.ClingoLib, EntryPoint = "clingo_control_new", CallingConvention = CallingConvention.Cdecl)]
        public static extern int New(string[] arguments, UIntPtr arguments_size, CClingo.Logger logger, IntPtr logger_data, uint message_limit, [Out] IntPtr[] control);

        /// <summary>
        /// Free a control object created with <see cref="ControlNew(string[], UIntPtr, IntPtr, IntPtr, uint, IntPtr[])"/>.
        /// </summary>
        /// <param name="control">the target.</param>
        [DllImport(CClingo.ClingoLib, EntryPoint = "clingo_control_free", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Free(IntPtr control);

        [DllImport(CClingo.ClingoLib, EntryPoint = "clingo_control_load", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Load(IntPtr control, string file);

        #endregion
    }
}
