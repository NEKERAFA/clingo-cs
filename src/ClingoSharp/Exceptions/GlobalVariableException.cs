using System;

namespace ClingoSharp.Exceptions
{
    public class GlobalVariableException : Exception
    {
        public GlobalVariableException() : base() { }
        public GlobalVariableException(string msg) : base(msg) { }
    }
}