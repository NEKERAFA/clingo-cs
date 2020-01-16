using System;

namespace ClingoSharp.Api
{
    internal static class CSymbolicAtoms
    {
        static CSymbolicAtoms()
        {
            CClingo.LoadClingoLibrary();
        }

        #region Inspection of atoms occurring in ground logic programs.

        public delegate int SymbolCallback(int[] symbols, UIntPtr symbols_size, UIntPtr data);

        #endregion
    }
}
