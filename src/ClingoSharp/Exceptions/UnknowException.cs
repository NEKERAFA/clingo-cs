using System;

namespace ClingoSharp.Exceptions
{
    class UnknownException : Exception
    {
        public UnknownException() : base() { }
        public UnknownException(string message) : base(message) { }
    }
}