using System;

namespace ClingoSharp.Exceptions
{
    class VariableUnboundedException : Exception
    {
        public VariableUnboundedException() : base() { }
        public VariableUnboundedException(string message) : base(message) { }
    }
}
