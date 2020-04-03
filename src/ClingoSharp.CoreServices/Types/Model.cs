using ClingoSharp.CoreServices.Interfaces;
using System;

namespace ClingoSharp.CoreServices.Types
{
    /// <summary>
    /// Object representing a model.
    /// </summary>
    public sealed class Model : IClingoObject
    {
        public IntPtr Object { get; set; }
    }
}
