using System;

namespace ClingoSharp.Exceptions
{
    public class VariableUnboundedException : Exception
    {
        public VariableUnboundedException() : base() { }
        public VariableUnboundedException(string msg) : base(msg) { }
    }
}