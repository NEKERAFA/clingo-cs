using System;

namespace ClingoSharp.Exceptions
{
    /// <summary>
    /// From <c>c++</c>: This class defines the type of objects thrown as exceptions to report errors that can only be detected during runtime.
    /// </summary>
    public class RuntimeException : Exception
    {
        public RuntimeException() : base() { }
        public RuntimeException(string message) : base(message) { }
    }
}