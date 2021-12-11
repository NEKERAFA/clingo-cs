using System;

namespace Clingo_cs.Exceptions
{
    public class CircularFileIncludedException : Exception
    {
        public CircularFileIncludedException() : base() { }
        public CircularFileIncludedException(string msg) : base(msg) { }
    }
}
