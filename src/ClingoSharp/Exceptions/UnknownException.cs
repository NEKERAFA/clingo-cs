using System;
using System.Collections.Generic;
using System.Text;

namespace ClingoSharp.Exceptions
{
    class UnknownException : Exception
    {
        public UnknownException() : base() { }
        public UnknownException(string message) : base(message) { }
    }
}
