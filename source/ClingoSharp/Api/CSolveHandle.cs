using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.Api
{
    internal static class CSolveHandle
    {
        static CSolveHandle()
        {
            CClingo.LoadClingoLibrary();
        }

        #region Interact with a running search.

        /// <summary>
        /// Callback function called during search to notify when the search is finished or a model is ready.
        /// </summary>
        /// <param name="eventType">Enumeration of solve events</param>
        /// <param name="eventData">the current event</param>
        /// <param name="data">user data of the callback</param>
        /// <param name="goon"></param>
        /// <returns></returns>
        public delegate int SolveEventCallback(int eventType, IntPtr eventData, IntPtr data, int[] goon);

        /// <summary>
        /// Enumeration of solve modes.
        /// </summary>
        public enum Mode
        {
            Async = 1,
            Yield = 2
        }

        /// <summary>
        /// Enumeration of solve events
        /// </summary>
        public enum EventType
        {
            Model = 0,
            Statistics = 1,
            Finish = 2
        }

        [DllImport(CClingo.ClingoLib, EntryPoint = "clingo_solve_handle_resume", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Resume(IntPtr handle);

        [DllImport(CClingo.ClingoLib, EntryPoint = "clingo_solve_handle_resume", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Model(IntPtr handle, [Out] IntPtr[] model);

        [DllImport(CClingo.ClingoLib, EntryPoint = "clingo_solve_handle_get", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Get(IntPtr handle, [Out] CControl.SolveResult[] result);

        [DllImport(CClingo.ClingoLib, EntryPoint = "clingo_solve_handle_close", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Close(IntPtr handle);

        #endregion
    }
}
