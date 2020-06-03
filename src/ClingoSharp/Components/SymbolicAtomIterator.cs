using ClingoSharp.NativeWrapper.Interfaces.Modules;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ClingoSharp
{
    /// <summary>
    /// Implements <see cref="IEnumerable{T}"/> of <see cref="SymbolicAtom"/>.
    /// </summary>
    public sealed class SymbolicAtomIterator : IEnumerable<SymbolicAtom>
    {
        #region Attributes

        private readonly IntPtr m_symbolicAtoms;
        private ulong m_currentPosition;

        #endregion

        #region Constructors

        public SymbolicAtomIterator(IntPtr symbolicAtoms, ulong startPosition)
        {
            m_symbolicAtoms = symbolicAtoms;
            m_currentPosition = startPosition;
        }

        #endregion

        #region Class Methods

        public static implicit operator IntPtr(SymbolicAtomIterator iterator)
        {
            return iterator.m_symbolicAtoms;
        }

        public static implicit operator ulong(SymbolicAtomIterator iterator)
        {
            return iterator.m_currentPosition;
        }

        #endregion

        #region Instance Methods

        public IEnumerator<SymbolicAtom> GetEnumerator()
        {
            Clingo.HandleClingoError(SymbolicAtoms.SymbolicAtomsModule.IsValid(this, this, out bool valid));
            
            while (valid)
            {
                yield return new SymbolicAtom(this, this);
                Clingo.HandleClingoError(SymbolicAtoms.SymbolicAtomsModule.GetNext(this, this, out m_currentPosition));
                Clingo.HandleClingoError(SymbolicAtoms.SymbolicAtomsModule.IsValid(this, this, out valid));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
