using System;

namespace Clingo_cs.Exceptions
{
    public class AtomUndefinedException : Exception
    {
        public AtomUndefinedException() : base() { }
        public AtomUndefinedException(string message) : base(message) { }
    }
}
