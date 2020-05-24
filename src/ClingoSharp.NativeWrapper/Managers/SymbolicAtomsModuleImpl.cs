using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Components.Types;
using ClingoSharp.CoreServices.Interfaces.Modules;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Managers
{
    public class SymbolicAtomsModuleImpl : ISymbolicAtomsModule
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

        public bool GetSize(SymbolicAtoms atoms, out UIntPtr size)
        {
            UIntPtr[] sizePtr = new UIntPtr[1];
            var success = clingo_symbolic_atoms_size(atoms.Object, sizePtr);
            size = sizePtr[0];
            return success != 0;
        }

        public bool GetBeginIterator(SymbolicAtoms atoms, Signature signature, out SymbolicAtomIterator iterator)
        {
            ulong[] iteratorPtr = new ulong[1];
            ulong[] signaturePtr = signature == null ? null : new ulong[] { signature };
            var success = clingo_symbolic_atoms_begin(atoms.Object, signaturePtr, iteratorPtr);
            iterator = iteratorPtr[0];
            return success != 0;
        }

        public bool GetEndIterator(SymbolicAtoms atoms, out SymbolicAtomIterator iterator)
        {
            ulong[] iteratorPtr = new ulong[1];
            var success = clingo_symbolic_atoms_end(atoms.Object, iteratorPtr);
            iterator = iteratorPtr[0];
            return success != 0;
        }

        public bool Find(SymbolicAtoms atoms, Symbol symbol, out SymbolicAtomIterator iterator)
        {
            ulong[] iteratorPtr = new ulong[1];
            var success = clingo_symbolic_atoms_find(atoms.Object, symbol, iteratorPtr);
            iterator = iteratorPtr[0];
            return success != 0;
        }

        public bool IteratorIsEqualTo(SymbolicAtoms atoms, SymbolicAtomIterator iteratorA, SymbolicAtomIterator iteratorB, out bool equal)
        {
            bool[] equalPtr = new bool[1];
            var success = clingo_symbolic_atoms_iterator_is_equal_to(atoms.Object, iteratorA, iteratorB, equalPtr);
            equal = equalPtr[0];
            return success != 0;
        }

        public bool GetSymbol(SymbolicAtoms atoms, SymbolicAtomIterator iterator, out Symbol symbol)
        {
            ulong[] symbolPtr = new ulong[1];
            var success = clingo_symbolic_atoms_symbol(atoms.Object, iterator, symbolPtr);
            symbol = symbolPtr[0];
            return success != 0;
        }

        public bool IsFact(SymbolicAtoms atoms, SymbolicAtomIterator iterator, out bool fact)
        {
            bool[] factPtr = new bool[1];
            var success = clingo_symbolic_atoms_is_fact(atoms.Object, iterator, factPtr);
            fact = factPtr[0];
            return success != 0;
        }

        public bool IsExternal(SymbolicAtoms atoms, SymbolicAtomIterator iterator, out bool external)
        {
            bool[] externalPtr = new bool[1];
            var success = clingo_symbolic_atoms_is_external(atoms.Object, iterator, externalPtr);
            external = externalPtr[0];
            return success != 0;
        }

        public bool GetLiteral(SymbolicAtoms atoms, SymbolicAtomIterator iterator, out Literal literal)
        {
            int[] literalPtr = new int[1];
            var success = clingo_symbolic_atoms_literal(atoms.Object, iterator, literalPtr);
            literal = literalPtr[0];
            return success != 0;
        }

        public bool GetSignatures(SymbolicAtoms atoms, out Signature[] signatures)
        {
            UIntPtr[] size = new UIntPtr[1];
            var success = clingo_symbolic_atoms_signatures_size(atoms.Object, size);

            if (success != 0)
            {
                ulong[] clingoSingnatures = new ulong[Convert.ToInt32(size[0].ToUInt32())];
                success = clingo_symbolic_atoms_signatures(atoms.Object, clingoSingnatures, size[0]);
                signatures = clingoSingnatures.Select(s => (Signature)s).ToArray();
                return success != 0;
            }

            signatures = null;
            return false;
        }

        public bool GetNext(SymbolicAtoms atoms, SymbolicAtomIterator iterator, out SymbolicAtomIterator next)
        {
            ulong[] nextIteratorPtr = new ulong[1];
            var success = clingo_symbolic_atoms_next(atoms.Object, iterator, nextIteratorPtr);
            next = nextIteratorPtr[0];
            return success != 0;
        }

        public bool IsValid(SymbolicAtoms atoms, SymbolicAtomIterator iterator, out bool valid)
        {
            bool[] validPtr = new bool[1];
            var success = clingo_symbolic_atoms_is_valid(atoms.Object, iterator, validPtr);
            valid = validPtr[0];
            return success != 0;
        }

        #endregion
    }
}