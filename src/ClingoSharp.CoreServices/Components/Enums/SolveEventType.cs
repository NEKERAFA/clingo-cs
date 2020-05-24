namespace ClingoSharp.CoreServices.Components.Enums
{
    /// <summary>
    /// Enumeration of solve events
    /// </summary>
    public enum SolveEventType
    {
        /// <summary>
        /// Issued if a model is found
        /// </summary>
        Model = 0,

        /// <summary>
        /// Issued when the statistics can be updated
        /// </summary>
        Statistics = 1,

        /// <summary>
        /// Issued if the search has completed
        /// </summary>
        Finish = 2
    }
}
