using ClingoSharp.NativeWrapper.Interfaces.Modules;
using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Managers
{
    public class SymbolicAtomsModuleImpl : ISymbolicAtoms
    {
        #region Clingo C API Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_size(IntPtr atoms, [Out] UIntPtr[] size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_begin(IntPtr atoms, ulong[] signature, [Out] ulong[] iterator);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_end(IntPtr atoms, [Out] ulong[] iterator);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_find(IntPtr atoms, ulong symbol, [Out] ulong[] iterator);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_iterator_is_equal_to(IntPtr atoms, ulong a, ulong b, [Out] bool[] equal);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_symbol(IntPtr atoms, ulong iterator, [Out] ulong[] symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_is_fact(IntPtr atoms, ulong iterator, [Out] bool[] fact);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_is_external(IntPtr atoms, ulong iterator, [Out] bool[] external);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_literal(IntPtr atoms, ulong iterator, int[] literal);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_signatures_size(IntPtr atoms, [Out] UIntPtr[] size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_signatures(IntPtr atoms, [Out] ulong[] signatures, UIntPtr size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_next(IntPtr atoms, ulong iterator, [Out] ulong[] next);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbolic_atoms_is_valid(IntPtr atoms, ulong iterator, [Out] bool[] valid);

        #endregion

        #region Module implementation

        public bool GetSize(IntPtr atoms, out UIntPtr size)
        {
            UIntPtr[] sizePtr = new UIntPtr[1];
            var success = clingo_symbolic_atoms_size(atoms, sizePtr);
            size = sizePtr[0];
            return success != 0;
        }

        public bool GetBeginIterator(IntPtr atoms, ulong? signature, out ulong iterator)
        {
            ulong[] iteratorPtr = new ulong[1];
            ulong[] signaturePtr = signature == null ? null : new ulong[] { signature.Value };
            var success = clingo_symbolic_atoms_begin(atoms, signaturePtr, iteratorPtr);
            iterator = iteratorPtr[0];
            return success != 0;
        }

        public bool GetEndIterator(IntPtr atoms, out ulong iterator)
        {
            ulong[] iteratorPtr = new ulong[1];
            var success = clingo_symbolic_atoms_end(atoms, iteratorPtr);
            iterator = iteratorPtr[0];
            return success != 0;
        }

        public bool Find(IntPtr atoms, ulong symbol, out ulong iterator)
        {
            ulong[] iteratorPtr = new ulong[1];
            var success = clingo_symbolic_atoms_find(atoms, symbol, iteratorPtr);
            iterator = iteratorPtr[0];
            return success != 0;
        }

        public bool IteratorIsEqualTo(IntPtr atoms, ulong iteratorA, ulong iteratorB, out bool equal)
        {
            bool[] equalPtr = new bool[1];
            var success = clingo_symbolic_atoms_iterator_is_equal_to(atoms, iteratorA, iteratorB, equalPtr);
            equal = equalPtr[0];
            return success != 0;
        }

        public bool GetSymbol(IntPtr atoms, ulong iterator, out ulong symbol)
        {
            ulong[] symbolPtr = new ulong[1];
            var success = clingo_symbolic_atoms_symbol(atoms, iterator, symbolPtr);
            symbol = symbolPtr[0];
            return success != 0;
        }

        public bool IsFact(IntPtr atoms, ulong iterator, out bool fact)
        {
            bool[] factPtr = new bool[1];
            var success = clingo_symbolic_atoms_is_fact(atoms, iterator, factPtr);
            fact = factPtr[0];
            return success != 0;
        }

        public bool IsExternal(IntPtr atoms, ulong iterator, out bool external)
        {
            bool[] externalPtr = new bool[1];
            var success = clingo_symbolic_atoms_is_external(atoms, iterator, externalPtr);
            external = externalPtr[0];
            return success != 0;
        }

        public bool GetLiteral(IntPtr atoms, ulong iterator, out int literal)
        {
            int[] literalPtr = new int[1];
            var success = clingo_symbolic_atoms_literal(atoms, iterator, literalPtr);
            literal = literalPtr[0];
            return success != 0;
        }

        public bool GetSignatures(IntPtr atoms, out ulong[] signatures)
        {
            UIntPtr[] size = new UIntPtr[1];
            var success = clingo_symbolic_atoms_signatures_size(atoms, size);

            if (success != 0)
            {
                signatures = new ulong[Convert.ToInt32(size[0].ToUInt32())];
                success = clingo_symbolic_atoms_signatures(atoms, signatures, size[0]);
                return success != 0;
            }

            signatures = null;
            return false;
        }

        public bool GetNext(IntPtr atoms, ulong iterator, out ulong next)
        {
            ulong[] nextIteratorPtr = new ulong[1];
            var success = clingo_symbolic_atoms_next(atoms, iterator, nextIteratorPtr);
            next = nextIteratorPtr[0];
            return success != 0;
        }

        public bool IsValid(IntPtr atoms, ulong iterator, out bool valid)
        {
            bool[] validPtr = new bool[1];
            var success = clingo_symbolic_atoms_is_valid(atoms, iterator, validPtr);
            valid = validPtr[0];
            return success != 0;
        }

        #endregion
    }
}