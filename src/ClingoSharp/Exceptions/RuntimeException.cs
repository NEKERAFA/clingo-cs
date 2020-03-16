using System;

namespace ClingoSharp.Exceptions
{
    public class RuntimeException : Exception
    {
        public RuntimeException() : base() { }
        public RuntimeException(string message) : base(message) { }
    }
}
