using System;

namespace ClingoSharp.CoreServices.Components.Enums
{
    /// <summary>
    /// Enumeration of solve modes.
    /// </summary>
    [Flags]
    public enum SolveMode
    {
        /// <summary>
        /// Enable non-blocking search
        /// </summary>
        Async = 1,

        /// <summary>
        /// Yield models in calls to clingo_solve_handle_model
        /// </summary>
        Yield = 2
    }
}
