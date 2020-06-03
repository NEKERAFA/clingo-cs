using System;

namespace ClingoSharp.NativeWrapper.Enums
{
    [Flags]
    public enum SolveResult
    {
        clingo_solve_result_satisfiable = 1,
        clingo_solve_result_unsatisfiable = 2,
        clingo_solve_result_exhausted = 4,
        clingo_solve_result_interrupted = 8
    }
}
