using System;

namespace ClingoSharp.Exceptions
{
    class AtomUndefinedException : Exception
    {
        public AtomUndefinedException() : base() { }
        public AtomUndefinedException(string message) : base(message) { }
    }
}
