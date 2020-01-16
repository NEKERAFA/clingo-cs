using ClingoSharp.Api;
using System;

namespace ClingoSharp
{
    [Serializable]
    public class ClingoException : Exception
    {
        public ClingoException(string clingoError) : base(clingoError)
        {
        }
    }
}
