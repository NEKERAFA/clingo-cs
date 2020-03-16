using System;

namespace ClingoSharp.Exceptions
{
    class LogicException : Exception
    {
        public LogicException() : base() { }
        public LogicException(string message) : base(message) { }
    }
}
