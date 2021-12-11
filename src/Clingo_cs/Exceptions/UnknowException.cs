using System;

namespace Clingo_cs.Exceptions
{
    class UnknownException : Exception
    {
        public UnknownException() : base() { }
        public UnknownException(string message) : base(message) { }
    }
}