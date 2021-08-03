using System;

namespace ClingoSharp.Exceptions
{
    class FileIncludedException : Exception
    {
        public FileIncludedException() : base() { }
        public FileIncludedException(string message) : base(message) { }
    }
}
