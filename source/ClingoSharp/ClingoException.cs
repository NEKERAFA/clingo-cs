using ClingoSharp.API;
using System;

namespace ClingoSharp
{
    [Serializable]
    public class ClingoException : Exception
    {
        enum ErrorCode
        {
            Success = 0,
            RuntimeError = 1,
            LogicError = 2,
            BadAlloc = 3,
            Unknown = 4
        }

        public ClingoException() : base(string.Format("{0} ({1})", ((ErrorCode)CClingo.GetLastError()).ToString(), CClingo.GetLastErrorMessage()))
        {
        }
    }
}
