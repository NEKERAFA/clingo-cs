namespace ClingoSharp.CoreServices.Enums
{
    /// <summary>
    /// Enumeration of error codes.
    /// </summary>
    /// <remarks>
    /// Errors can only be recovered from if explicitly mentioned; most functions do not provide strong exception guarantees. This means that in case of errors associated objects cannot be used further. If such an object has a free function, this function can and should still be called.
    /// </remarks>
    public enum ErrorCode
    {
        /// <summary>
        /// successful API calls
        /// </summary>
        Success = 0,

        /// <summary>
        /// errors only detectable at runtime like invalid input
        /// </summary>
        RuntimeError = 1,

        /// <summary>
        /// wrong usage of the clingo API
        /// </summary>
        LogicError = 2,

        /// <summary>
        /// memory could not be allocated
        /// </summary>
        BadAlloc = 3,

        /// <summary>
        /// errors unrelated to clingo
        /// </summary>
        Unknown = 4
    }
}
