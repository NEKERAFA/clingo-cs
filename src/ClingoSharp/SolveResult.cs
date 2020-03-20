namespace ClingoSharp
{
    /// <summary>
    /// Captures the result of a solve call.
    /// </summary>
    public class SolveResult
    {
        #region Properties

        /// <summary>
        /// <c>true</c> if the search space was exhausted.
        /// </summary>
        public bool IsExhausted { get; internal set; }

        /// <summary>
        /// <c>true</c> if the search was interrupted.
        /// </summary>
        public bool IsInterrupted { get; internal set; }

        /// <summary>
        /// <c>true</c> if the problem is satisfiable, <c>false</c> if the problem is unsatisfiable, or <c>null</c> if the satisfiablity is not known.
        /// </summary>
        public bool? IsSatisfiable { get; internal set; }

        /// <summary>
        /// <c>true</c> if the satisfiablity is not known.
        /// </summary>
        /// This is equivalent to satisfiable is <c>null</c>
        public bool IsUnknown { get; internal set; }

        /// <summary>
        /// <c>true</c> if the problem is unsatisfiable, <c>false</c> if the problem is satisfiable, or <c>null</c> if the satisfiablity is not known.
        /// </summary>
        public bool? IsUnSatisfiable { get; internal set; }

        #endregion

        #region Constructors

        internal SolveResult() { }

        #endregion
    }
}
