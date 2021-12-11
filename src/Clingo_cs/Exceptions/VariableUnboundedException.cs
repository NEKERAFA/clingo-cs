using System;

namespace Clingo_cs.Exceptions
{
    public class VariableUnboundedException : Exception
    {
        public VariableUnboundedException() : base() { }
        public VariableUnboundedException(string msg) : base(msg) { }
    }
}
