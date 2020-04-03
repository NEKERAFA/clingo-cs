using ClingoLiteral = ClingoSharp.CoreServices.Types.Literal;
using ClingoSymbol = ClingoSharp.CoreServices.Types.Symbol;
using ClingoSymbolicAtomIterator = ClingoSharp.CoreServices.Types.SymbolicAtomIterator;
using ClingoSymbolicAtoms = ClingoSharp.CoreServices.Types.SymbolicAtoms;

namespace ClingoSharp
{
    public sealed class SymbolicAtom
    {
        #region Attributes

        private readonly ClingoSymbolicAtoms m_symbolicAtoms;
        private readonly ClingoSymbolicAtomIterator m_position;

        #endregion

        #region Properties

        public bool IsExternal 
        {
            get
            {
                Clingo.HandleClingoError(SymbolicAtoms.GetSymbolicAtomsModule().IsExternal(this, this, out bool external));
                return external;
            }
        }

        public bool IsFact
        {
            get
            {
                Clingo.HandleClingoError(SymbolicAtoms.GetSymbolicAtomsModule().IsFact(this, this, out bool fact));
                return fact;
            }
        }

        public int Literal
        {
            get
            {
                Clingo.HandleClingoError(SymbolicAtoms.GetSymbolicAtomsModule().GetLiteral(this, this, out ClingoLiteral literal));
                return literal;
            }
        }

        public Symbol Symbol
        {
            get
            {
                Clingo.HandleClingoError(SymbolicAtoms.GetSymbolicAtomsModule().GetSymbol(this, this, out ClingoSymbol symbol));
                return new Symbol(symbol);
            }
        }

        #endregion

        #region Class Methods

        public static implicit operator ClingoSymbolicAtoms(SymbolicAtom symbolicAtom)
        {
            return symbolicAtom.m_symbolicAtoms;
        }

        public static implicit operator ClingoSymbolicAtomIterator(SymbolicAtom symbolicAtom)
        {
            return symbolicAtom.m_position;
        }

        public static implicit operator Symbol(SymbolicAtom symbolicAtom)
        {
            return symbolicAtom.Symbol;
        }

        public static implicit operator int(SymbolicAtom symbolicAtom)
        {
            return symbolicAtom.Literal;
        }

        #endregion

        #region Constructors

        internal SymbolicAtom(ClingoSymbolicAtoms symbolicAtoms, ClingoSymbolicAtomIterator position)
        {
            m_symbolicAtoms = symbolicAtoms;
            m_position = position;
        }

        #endregion

        #region Instance Methods

        public bool Match(string name, int arity)
        {
            return Symbol.Match(name, arity);
        }

        #endregion
    }
}
