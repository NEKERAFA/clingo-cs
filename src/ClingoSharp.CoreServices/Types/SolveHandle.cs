using ClingoSharp.CoreServices.Interfaces;
using System;

namespace ClingoSharp.CoreServices.Types
{
    /// <summary>
    /// Search handle to a solve call.
    /// </summary>
    public sealed class SolveHandle : IClingoObject
    {
        public IntPtr Object { get; set; }
    }
}
