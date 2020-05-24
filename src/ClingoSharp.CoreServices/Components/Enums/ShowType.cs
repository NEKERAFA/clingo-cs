using System;

namespace ClingoSharp.CoreServices.Components.Enums
{
    /// <summary>
    /// Enumeration of bit flags to select symbols in models
    /// </summary>
    [Flags]
    public enum ShowType
    {
        /// <summary>
        /// Select CSP assignments
        /// </summary>
        CSP = 1,

        /// <summary>
        /// Select shown atoms and terms
        /// </summary>
        Shown = 2,

        /// <summary>
        /// Select all atoms
        /// </summary>
        Atoms = 4,

        /// <summary>
        /// Select all terms
        /// </summary>
        Terms = 8,

        /// <summary>
        /// Select everything
        /// </summary>
        All = 15,

        /// <summary>
        /// Select false instead of true atoms (<see cref="Atoms"/>) or terms (<see cref="Terms"/>).
        /// </summary>
        Complement = 16
    }
}
