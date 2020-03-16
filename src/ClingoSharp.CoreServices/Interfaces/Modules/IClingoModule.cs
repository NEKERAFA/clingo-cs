using ClingoSharp.CoreServices.Enums;

namespace ClingoSharp.CoreServices.Interfaces.Modules
{
    public interface IClingoModule : IModule
    {
        /// <summary>
        /// Obtains the clingo version as string
        /// </summary>
        /// <returns>A string in format "mayor.minor.revision"</returns>
        string GetVersion();

        /// <summary>
        /// Gets the last error code set by a clingo API call
        /// </summary>
        /// <returns>A <see cref="ErrorCode"/> enumeration</returns>
        ErrorCode GetErrorCode();

        /// <summary>
        /// Gets the last error message set if an API call fails.
        /// </summary>
        /// <returns>An error message or <see cref="null"/></returns>
        string GetErrorMessage();
    }
}
