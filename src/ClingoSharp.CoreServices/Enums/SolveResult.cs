using System;

namespace ClingoSharp.CoreServices.Enums
{
    [Flags]
    public enum SolveResult
    {
        Satisfiable = 1,
        Unsatisfiable = 2,
        Exhausted = 4,
        Interrupted = 8
    }
}
