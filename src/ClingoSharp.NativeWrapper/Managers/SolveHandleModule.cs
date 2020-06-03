using ClingoSharp.NativeWrapper.Enums;
using ClingoSharp.NativeWrapper.Interfaces.Modules;
using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Managers
{
    public class SolveHandleModule : ISolveHandle
    {
        #region Clingo C API Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_handle_get(IntPtr handle, [Out] Enums.SolveResult[] result);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clingo_solve_handle_wait(IntPtr handle, double timeout, [Out] bool[] result);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_handle_model(IntPtr handle, [Out] IntPtr[] model);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_handle_resume(IntPtr handle);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_handle_cancel(IntPtr handle);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_handle_close(IntPtr handle);

        #endregion

        #region Module implementation

        public bool Get(IntPtr handle, out SolveResult result)
        {
            SolveResult[] resultPtr = new SolveResult[1];
            var success = clingo_solve_handle_get(handle, resultPtr);
            result = resultPtr[0];
            return success != 0;
        }

        public void Wait(IntPtr handle, double timeout, out bool result)
        {
            bool[] resultPtr = new bool[1];
            clingo_solve_handle_wait(handle, timeout, resultPtr);
            result = resultPtr[0];
        }

        public bool Model(IntPtr handle, out IntPtr model)
        {
            IntPtr[] modelPtr = new IntPtr[1];
            var success = clingo_solve_handle_model(handle, modelPtr);
            model = modelPtr[0];
            return success != 0;
        }

        public bool Resume(IntPtr handle)
        {
            var success = clingo_solve_handle_resume(handle);
            return success != 0;
        }

        public bool Cancel(IntPtr handle)
        {
            var success = clingo_solve_handle_cancel(handle);
            return success != 0;
        }

        public bool Close(IntPtr handle)
        {
            var success = clingo_solve_handle_close(handle);
            return success != 0;
        }

        #endregion
    }
}
