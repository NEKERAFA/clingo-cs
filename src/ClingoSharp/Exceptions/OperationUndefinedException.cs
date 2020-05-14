using System;
namespace ClingoSharp.Exceptions
{
    class OperationUndefinedException : Exception
    {
        public OperationUndefinedException() : base() { }
        public OperationUndefinedException(string message) : base(message) { }
    }
}
