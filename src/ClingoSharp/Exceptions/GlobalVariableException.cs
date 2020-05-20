using System;

namespace ClingoSharp.Exceptions
{
    class GlobalVariableException : Exception
    {
        public GlobalVariableException() : base() { }
        public GlobalVariableException(string message) : base(message) { }
    }
}
