using ClingoSharp.CoreServices.Enums;
using System;

namespace ClingoSharp.CoreServices.Interfaces.Modules
{
    public interface ISolveHandleModule : IModule
    {
        /// <summary>
        /// Get the next solve result.
        /// </summary>
        /// Blocks until the result is ready. When yielding partial solve results can be obtained, i.e., when a model is ready, the result will be satisfiable but neither the search exhausted nor the optimality proven.
        /// <param name="handle">a handle object</param>
        /// <param name="result">the solve result</param>
        /// <returns>True if the function is success, false otherwise</returns>
        bool Get(IntPtr handle, out SolveResult result);

        /// <summary>
        /// Wait for the specified amount of time to check if the next result is ready.
        /// </summary>
        /// <param name="handle">a handle object</param>
        /// <param name="timeout">the maximum time to wait</param>
        /// <param name="result">whether the search has finished</param>
        void Wait(IntPtr handle, double timeout, out bool result);

        /// <summary>
        /// Get the next model (or zero if there are no more models).
        /// </summary>
        /// <param name="handle">a handle object</param>
        /// <param name="model">the model (it is <see cref="IntPtr.Zero"/> if there are no more models)</param>
        /// <returns>True if the function is success, false otherwise</returns>
        bool Model(IntPtr handle, out IntPtr model);

        /// <summary>
        /// Discards the last model and starts the search for the next one.
        /// </summary>
        /// If the search has been started asynchronously, this function continues the search in the background.
        /// <param name="handle">a handle object</param>
        /// <returns>True if the function is success, false otherwise</returns>
        bool Resume(IntPtr handle);

        /// <summary>
        /// Stop the running search and block until done.
        /// </summary>
        /// <param name="handle">a handle object</param>
        /// <returns>True if the function is success, false otherwise</returns>
        bool Cancel(IntPtr handle);

        /// <summary>
        /// Stop the running search and block until done.
        /// </summary>
        /// Blocks until the search is stopped (as if an implicit cancel was called before the handle is released).
        /// <param name="handle">a handle object</param>
        /// <returns>True if the function is success, false otherwise</returns>
        bool Close(IntPtr handle);
    }
}
