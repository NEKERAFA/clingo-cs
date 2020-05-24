using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Components.Enums;
using ClingoSharp.CoreServices.Components.Types;
using ClingoSharp.CoreServices.Interfaces.Modules;
using ClingoSharp.NativeWrapper.Enums;
using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Managers
{
    public class SolveHandleModuleImpl : ISolveHandleModule
    {
        #region Clingo C API Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_handle_get(IntPtr handle, [Out] clingo_solve_result[] result);

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

        public bool Get(SolveHandle handle, out SolveResult result)
        {
            clingo_solve_result[] resultPtr = new clingo_solve_result[1];
            var success = clingo_solve_handle_get(handle.Object, resultPtr);
            result = (SolveResult)resultPtr[0];
            return success != 0;
        }

        public void Wait(SolveHandle handle, double timeout, out bool result)
        {
            bool[] resultPtr = new bool[1];
            clingo_solve_handle_wait(handle.Object, timeout, resultPtr);
            result = resultPtr[0];
        }

        public bool Model(SolveHandle handle, out Model model)
        {
            IntPtr[] modelPtr = new IntPtr[1];
            var success = clingo_solve_handle_model(handle.Object, modelPtr);
            model = new Model() { Object = modelPtr[0] };
            return success != 0;
        }

        public bool Resume(SolveHandle handle)
        {
            var success = clingo_solve_handle_resume(handle.Object);
            return success != 0;
        }

        public bool Cancel(SolveHandle handle)
        {
            var success = clingo_solve_handle_cancel(handle.Object);
            return success != 0;
        }

        public bool Close(SolveHandle handle)
        {
            var success = clingo_solve_handle_close(handle.Object);
            return success != 0;
        }

        #endregion
    }
}
