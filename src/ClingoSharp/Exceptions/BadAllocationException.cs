using System;

namespace ClingoSharp.Exceptions
{
    /// <summary>
    /// From <c>c++</c>: Type of the exceptions thrown by the standard definitions of operator new and operator new[] when they fail to allocate the requested storage space.
    /// </summary>
    class BadAllocationException : Exception
    {
        public BadAllocationException() : base() { }
        public BadAllocationException(string message) : base(message) { }
    }
}
