using ClingoSharp.NativeWrapper.Enums;

namespace ClingoSharp.NativeWrapper.Interfaces.Modules
{
    /// <summary>
    /// Functions used throughout all modules and version information
    /// </summary>
    public interface IClingo : IClingoModule
    {
        /// <summary>
        /// Obtains the clingo version as string
        /// </summary>
        /// <returns>A string in format <c>mayor.minor.revision</c></returns>
        string GetVersion();

        /// <summary>
        /// Gets the last error code set by a clingo API call
        /// </summary>
        /// <returns>A <see cref="ErrorCode"/> enumeration</returns>
        ErrorCode GetErrorCode();

        /// <summary>
        /// Gets the last error message set if an API call fails.
        /// </summary>
        /// <returns>An error message or <c>null</c></returns>
        string GetErrorMessage();
    }
}
