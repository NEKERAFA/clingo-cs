using ClingoSharp.CoreServices.Interfaces;
using System;

namespace ClingoSharp.CoreServices.Components.Types
{
    /// <summary>
    /// Object to add clauses during search.
    /// </summary>
    public sealed class SolveControl : IClingoObject
    {
        public IntPtr Object { get; set; }
    }
}
