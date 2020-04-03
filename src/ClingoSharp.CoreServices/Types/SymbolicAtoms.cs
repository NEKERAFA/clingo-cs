using ClingoSharp.CoreServices.Interfaces;
using System;

namespace ClingoSharp.CoreServices.Types
{
    /// <summary>
    /// Object to inspect symbolic atoms in a program—the relevant Herbrand base gringo uses to instantiate programs.
    /// </summary>
    public sealed class SymbolicAtoms : IClingoObject
    {
        public IntPtr Object { get; set; }
    }
}
