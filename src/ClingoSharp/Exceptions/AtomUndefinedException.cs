using System;

namespace ClingoSharp.Exceptions
{
    public class AtomUndefinedException : Exception
    {
        public AtomUndefinedException() : base() { }
        public AtomUndefinedException(string message) : base(message) { }
    }
}