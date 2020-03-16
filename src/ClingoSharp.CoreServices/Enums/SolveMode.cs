using System;

namespace ClingoSharp.CoreServices.Enums
{
    [Flags]
    public enum SolveMode
    {
        None = 0,
        Async = 1,
        Yield = 2
    }
}
