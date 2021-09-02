using System;

namespace ClingoSharp
{
    /// <summary>
    /// Captures a symbolic atom and provides properties to inspect its state
    /// </summary>
    public sealed class SymbolicAtom
    {
        #region Attributes

        private readonly IntPtr m_symbolicAtoms;
        private readonly ulong m_position;

        #endregion

        #region Instance Properties

        /// <summary>
        /// Whether the atom is an external atom.
        /// </summary>
        public bool IsExternal 
        {
            get
            {
                Clingo.HandleClingoError(SymbolicAtoms.SymbolicAtomsModule.IsExternal(this, this, out bool external));
                return external;
            }
        }

        /// <summary>
        /// Whether the atom is a fact.
        /// </summary>
        public bool IsFact
        {
            get
            {
                Clingo.HandleClingoError(SymbolicAtoms.SymbolicAtomsModule.IsFact(this, this, out bool fact));
                return fact;
            }
        }

        /// <summary>
        /// The program literal associated with the atom.
        /// </summary>
        public int Literal
        {
            get
            {
                Clingo.HandleClingoError(SymbolicAtoms.SymbolicAtomsModule.GetLiteral(this, this, out int literal));
                return literal;
            }
        }

        /// <summary>
        /// The representation of the atom in form of a symbol.
        /// </summary>
        public Symbol Symbol
        {
            get
            {
                Clingo.HandleClingoError(SymbolicAtoms.SymbolicAtomsModule.GetSymbol(this, this, out ulong symbol));
                return new Symbol(symbol);
            }
        }

        #endregion

        #region Class Methods

        public static implicit operator IntPtr(SymbolicAtom symbolicAtom)
        {
            return symbolicAtom.m_symbolicAtoms;
        }

        public static implicit operator ulong(SymbolicAtom symbolicAtom)
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

        public SymbolicAtom(IntPtr symbolicAtoms, ulong position)
        {
            m_symbolicAtoms = symbolicAtoms;
            m_position = position;
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Check if the atom matches the given signature.
        /// </summary>
        /// <param name="name">The name of the function</param>
        /// <param name="arity">The arity of the function</param>
        /// <returns>Whether the function matches</returns>
        /// <seealso cref="Symbol.Match(string, int)"/>
        public bool Match(string name, int arity)
        {
            return Symbol.Match(name, arity);
        }

        #endregion
    }
}
