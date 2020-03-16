using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.Interfaces.Modules;
using ClingoSharp.NativeWrapper.Enums;
using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper
{
    public class SolveHandleModuleImpl : ISolveHandleModule
    {
        #region Clingo C API Functions

        /// <summary>
        /// Get the next solve result.
        /// </summary>
        /// Blocks until the result is ready. When yielding partial solve results can be obtained, i.e., when a model is ready, the result will be satisfiable but neither the search exhausted nor the optimality proven.
        /// <param name="handle">the target</param>
        /// <param name="result">the solve result</param>
        /// <returns>whether the call was successful; might set one of the following error codes: <see cref="clingo_error.clingo_error_bad_alloc"/>, <see cref="clingo_error.clingo_error_runtime"/> if solving fails</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_handle_get(IntPtr handle, [Out] clingo_solve_result[] result);

        /// <summary>
        /// Wait for the specified amount of time to check if the next result is ready.
        /// </summary>
        /// If the time is set to zero, this function can be used to poll if the search is still active. If the time is negative, the function blocks until the search is finished.
        /// <param name="handle">the target</param>
        /// <param name="timeout">the maximum time to wait</param>
        /// <param name="result">whether the search has finished</param>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clingo_solve_handle_wait(IntPtr handle, double timeout, [Out] bool[] result);

        /// <summary>
        /// Get the next model (or zero if there are no more models).
        /// </summary>
        /// <param name="handle">the target</param>
        /// <param name="model">the model (it is <see cref="IntPtr.Zero"/> if there are no more models)</param>
        /// <returns>whether the call was successful; might set one of the following error codes: <see cref="clingo_error.clingo_error_bad_alloc"/>, <see cref="clingo_error.clingo_error_runtime"/> if solving fails</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_handle_model(IntPtr handle, [Out] IntPtr[] model);

        /// <summary>
        /// Discards the last model and starts the search for the next one.
        /// </summary>
        /// If the search has been started asynchronously, this function continues the search in the background.
        /// <param name="handle">the target</param>
        /// <returns>whether the call was successful; might set one of the following error codes: <see cref="clingo_error.clingo_error_bad_alloc"/>, <see cref="clingo_error.clingo_error_runtime"/> if solving fails</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_handle_resume(IntPtr handle);

        /// <summary>
        /// Stop the running search and block until done.
        /// </summary>
        /// <param name="handle">the target</param>
        /// <returns>whether the call was successful; might set one of the following error codes: <see cref="clingo_error.clingo_error_bad_alloc"/> <see cref="clingo_error.clingo_error_runtime"/> if solving fails</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_handle_cancel(IntPtr handle);

        /// <summary>
        /// Stops the running search and releases the handle.
        /// </summary>
        /// Blocks until the search is stopped (as if an implicit cancel was called before the handle is released).
        /// <param name="handle">the target</param>
        /// <returns>whether the call was successful; might set one of the following error codes: <see cref="clingo_error.clingo_error_bad_alloc"/>, <see cref="clingo_error.clingo_error_runtime"/> if solving fails</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_solve_handle_close(IntPtr handle);

        #endregion

        #region Module implementation

        public bool Get(IntPtr handle, out SolveResult result)
        {
            clingo_solve_result[] resultPtr = new clingo_solve_result[1];
            
            var success = clingo_solve_handle_get(handle, resultPtr);

            result = (SolveResult)resultPtr[0];

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
