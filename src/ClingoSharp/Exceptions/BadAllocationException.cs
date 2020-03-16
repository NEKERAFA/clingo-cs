using System;

namespace ClingoSharp.Exceptions
{
    class BadAllocationException : Exception
    {
        public BadAllocationException() : base() { }
        public BadAllocationException(string message) : base(message) { }
    }
}
