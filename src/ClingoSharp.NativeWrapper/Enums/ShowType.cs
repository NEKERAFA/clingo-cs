using System;

namespace ClingoSharp.NativeWrapper.Enums
{
    [Flags]
    internal enum clingo_show_type
    {
        clingo_show_type_none = 0,
        clingo_show_type_csp = 1,
        clingo_show_type_shown = 2,
        clingo_show_type_atoms = 4,
        clingo_show_type_terms = 8,
        clingo_show_type_all = 15,
        clingo_show_type_complement = 16
    }
}
