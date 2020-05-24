using System;

namespace ClingoSharp.CoreServices.Components.Enums
{
    /// <summary>
    /// Enumeration of bit masks for solve call results.
    /// </summary>
    /// <remarks>
    /// Neither <see cref="Satisfiable"/> nor <see cref="Exhausted"/> is set if the search is interrupted and no model was found.
    /// </remarks>
    [Flags]
    public enum SolveResult
    {
        /// <summary>
        /// The last solve call found a solution
        /// </summary>
        Satisfiable = 1,

        /// <summary>
        /// The last solve call did not find a solution
        /// </summary>
        Unsatisfiable = 2,

        /// <summary>
        /// The last solve call completely exhausted the search space
        /// </summary>
        Exhausted = 4,

        /// <summary>
        /// The last solve call was interrupted
        /// </summary>
        Interrupted = 8
    }
}
