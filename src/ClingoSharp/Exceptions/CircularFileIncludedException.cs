using System;

namespace ClingoSharp.Exceptions
{
    public class CircularFileIncludedException : Exception
    {
        public CircularFileIncludedException() : base() { }
        public CircularFileIncludedException(string msg) : base(msg) { }
    }
}