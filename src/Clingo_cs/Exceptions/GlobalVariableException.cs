using System;

namespace Clingo_cs.Exceptions
{
    public class GlobalVariableException : Exception
    {
        public GlobalVariableException() : base() { }
        public GlobalVariableException(string msg) : base(msg) { }
    }
}
