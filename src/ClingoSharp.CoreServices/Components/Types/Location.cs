using ClingoSharp.CoreServices.Interfaces;

namespace ClingoSharp.CoreServices.Components.Types
{
    /// <summary>
    /// Represents a source code location marking its beginnig and end.
    /// </summary>
    /// <remarks>
    /// Not all locations refer to physical files. By convention, such locations use a name put in angular brackets as filename.
    /// </remarks>
    public sealed class Location : IClingoObject
    {
        /// <summary>
        /// the file where the location begins
        /// </summary>
        public string BeginFile { get; set; }

        /// <summary>
        /// the file where the location ends
        /// </summary>
        public string EndFile { get; set; }

        /// <summary>
        /// the line where the location begins
        /// </summary>
        public uint BeginLine { get; set; }

        /// <summary>
        /// the line where the location ends
        /// </summary>
        public uint BeginColumn { get; set; }

        /// <summary>
        /// the column where the location begins
        /// </summary>
        public uint EndLine { get; set; }

        /// <summary>
        /// the column where the location ends
        /// </summary>
        public uint EndColumn { get; set; }
    }
}
