namespace ClingoSharp.CoreServices.Components.Enums
{
    /// <summary>
    /// Enumeration of warning codes
    /// </summary>
    public enum WarningCode
    {
        /// <summary>
        /// undefined arithmetic operation or weight of aggregate
        /// </summary>
        OperationUndefined = 0,

        /// <summary>
        /// to report multiple errors; a corresponding runtime error is raised later
        /// </summary>
        RuntimeError = 1,

        /// <summary>
        /// undefined atom in program
        /// </summary>
        AtomUndefined = 2,

        /// <summary>
        /// same file included multiple times
        /// </summary>
        FileIncluded = 3,

        /// <summary>
        /// CSP variable with unbounded domain
        /// </summary>
        VariableUnbounded = 4,

        /// <summary>
        /// global variable in tuple of aggregate element
        /// </summary>
        GlobalVariable = 5,

        /// <summary>
        /// other kinds of warnings
        /// </summary>
        Other = 6
    }
}
