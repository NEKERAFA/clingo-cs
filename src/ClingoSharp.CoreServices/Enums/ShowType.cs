using System;

namespace ClingoSharp.CoreServices.Enums
{
    [Flags]
    public enum ShowType
    {
        CSP = 1,
        Shown = 2,
        Atoms = 4,
        Terms = 8,
        All = 15,
        Complement = 16
    }
}
