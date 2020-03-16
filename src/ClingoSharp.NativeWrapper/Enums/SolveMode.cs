using System;

namespace ClingoSharp.NativeWrapper.Enums
{
    [Flags]
    internal enum clingo_solve_mode
    {
        clingo_solve_mode_none = 0,
        clingo_solve_mode_async = 1,
        clingo_solve_mode_yield = 2 
    }
}
