using System;

namespace ClingoSharp.NativeWrapper.Enums
{
    [Flags]
    public enum SolveMode
    {
        clingo_solve_mode_none = 0,
        clingo_solve_mode_async = 1,
        clingo_solve_mode_yield = 2
    }
}
