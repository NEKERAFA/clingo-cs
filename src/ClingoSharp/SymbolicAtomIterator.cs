using System.Collections;
using System.Collections.Generic;
using ClingoSymbolicAtoms = ClingoSharp.CoreServices.Types.SymbolicAtoms;
using ClingoSymbolicAtomIterator = ClingoSharp.CoreServices.Types.SymbolicAtomIterator;

namespace ClingoSharp
{
    public sealed class SymbolicAtomIterator : IEnumerable<SymbolicAtom>
    {
        #region Attributes

        private readonly ClingoSymbolicAtoms m_symbolicAtoms;
        private ClingoSymbolicAtomIterator m_currentPosition;

        #endregion

        #region Constructors

        public SymbolicAtomIterator(ClingoSymbolicAtoms symbolicAtoms, ClingoSymbolicAtomIterator startPosition)
        {
            m_symbolicAtoms = symbolicAtoms;
            m_currentPosition = startPosition;
        }

        #endregion

        #region Class Methods

        public static implicit operator ClingoSymbolicAtoms(SymbolicAtomIterator iterator)
        {
            return iterator.m_symbolicAtoms;
        }

        public static implicit operator ClingoSymbolicAtomIterator(SymbolicAtomIterator iterator)
        {
            return iterator.m_currentPosition;
        }

        #endregion

        #region Instance Methods

        public IEnumerator<SymbolicAtom> GetEnumerator()
        {
            Clingo.HandleClingoError(SymbolicAtoms.GetSymbolicAtomsModule().IsValid(this, this, out bool valid));
            
            while (valid)
            {
                yield return new SymbolicAtom(this, this);
                Clingo.HandleClingoError(SymbolicAtoms.GetSymbolicAtomsModule().GetNext(this, this, out m_currentPosition));
                Clingo.HandleClingoError(SymbolicAtoms.GetSymbolicAtomsModule().IsValid(this, this, out valid));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
