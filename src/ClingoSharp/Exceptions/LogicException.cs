using System;

namespace ClingoSharp.Exceptions
{
    /// <summary>
    /// From <c>c++</c>: This class defines the type of objects thrown as exceptions to report errors in the internal logical of the program, such as violation of logical preconditions or class invariants.
    /// </summary>
    class LogicException : Exception
    {
        public LogicException() : base() { }
        public LogicException(string message) : base(message) { }
    }
}
