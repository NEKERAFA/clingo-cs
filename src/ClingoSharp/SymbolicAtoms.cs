using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Interfaces.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using ClingoSymbolicAtomIterator = ClingoSharp.CoreServices.Types.SymbolicAtomIterator;
using ClingoSymbolicAtoms = ClingoSharp.CoreServices.Types.SymbolicAtoms;
using ClingoSignature = ClingoSharp.CoreServices.Types.Signature;
using System.Linq;
using ClingoSharp.CoreServices.Interfaces;

namespace ClingoSharp
{
    /// <summary>
    /// <para>This class provides read-only access to the atom base of the grounder.</para>
    /// <para>It implements <see cref="IReadOnlyCollection{T}"/> of <see cref="SymbolicAtom"/> and <see cref="IReadOnlyDictionary{TKey, TValue}"/> of &lt; <see cref="Symbol"/> , <see cref="SymbolicAtom"/> &gt;.</para>
    /// </summary>
    public sealed class SymbolicAtoms : IReadOnlyCollection<SymbolicAtom>, IReadOnlyDictionary<Symbol, SymbolicAtom>
    {
        #region Attributes

        private static readonly ISymbolicAtomsModule m_module;
        private readonly ClingoSymbolicAtoms m_clingoSymbolicAtoms;

        #endregion

        #region Properties

        /// <summary>
        /// <para>The list of predicate signatures occurring in the program.</para>
        /// <para>The <see cref="bool"/> indicates the sign of the signature.</para>
        /// </summary>
        public List<Tuple<string, int, bool>> Signatures
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetSignatures(this, out ClingoSignature[] signatures));
                return signatures.Select(s => new Tuple<string, int, bool>(
                    Symbol.GetSymbolModule().GetName(s),
                    Convert.ToInt32(Symbol.GetSymbolModule().GetArity(s)),
                    Symbol.GetSymbolModule().IsPositive(s)
                )).ToList();
            }
        }

        /// <inheritdoc/>
        public SymbolicAtom this[Symbol key] {
            get
            {
                if (!TryGetValue(key, out SymbolicAtom value))
                {
                    throw new KeyNotFoundException();
                }

                return value;
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetSize(this, out UIntPtr size));
                return Convert.ToInt32(size.ToUInt32());
            }
        }

        /// <inheritdoc/>
        public IEnumerable<Symbol> Keys
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetBeginIterator(this, null, out ClingoSymbolicAtomIterator clingoIterator));
                var iterator = new SymbolicAtomIterator(this, clingoIterator);

                foreach (var symbolicAtom in iterator)
                {
                    yield return symbolicAtom;
                }
            }
        }

        /// <inheritdoc/>
        public IEnumerable<SymbolicAtom> Values
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetBeginIterator(this, null, out ClingoSymbolicAtomIterator iterator));
                return new SymbolicAtomIterator(this, iterator);
            }
        }

        #endregion

        #region Constructors

        static SymbolicAtoms()
        {
            m_module = Repository.GetModule<ISymbolicAtomsModule>();
        }

        public SymbolicAtoms(ClingoSymbolicAtoms clingoSymbolicAtoms)
        {
            m_clingoSymbolicAtoms = clingoSymbolicAtoms;
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Gets the asociated API module in clingo
        /// </summary>
        /// <returns>The asociated module</returns>
        public static IClingoModule GetModule()
        {
            return m_module;
        }

        /// <summary>
        /// Gets the symbolic atoms module in clingo
        /// </summary>
        /// <returns>The symbolic atoms module</returns>
        public static ISymbolicAtomsModule GetSymbolicAtomsModule()
        {
            return m_module;
        }

        public static implicit operator ClingoSymbolicAtoms(SymbolicAtoms symbolicAtoms)
        {
            return symbolicAtoms.m_clingoSymbolicAtoms;
        }

        public static implicit operator SymbolicAtoms(ClingoSymbolicAtoms clingoSymbolicAtoms)
        {
            return new SymbolicAtoms(clingoSymbolicAtoms);
        }

        #endregion

        #region Instance Methods


        /// <summary>
        /// Return an iterator over the symbolic atoms with the given signature
        /// </summary>
        /// <param name="name">The name of the signature</param>
        /// <param name="arity">The arity of the signature</param>
        /// <param name="positive">The sign of the signature</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Symbol"/></returns>
        public IEnumerator<Symbol> BySignature(string name, int arity, bool positive = true)
        {
            Clingo.HandleClingoError(Symbol.GetSymbolModule().CreateSignature(name, Convert.ToUInt32(arity), positive, out ClingoSignature signature));
            Clingo.HandleClingoError(m_module.GetBeginIterator(this, signature, out ClingoSymbolicAtomIterator clingoIterator));
            
            var iterator = new SymbolicAtomIterator(this, clingoIterator);
            foreach (var symbolicAtom in iterator)
            {
                yield return symbolicAtom;
            }
        }

        /// <inheritdoc/>
        public bool ContainsKey(Symbol key)
        {
            return TryGetValue(key, out var _);
        }

        /// <inheritdoc/>
        public IEnumerator<SymbolicAtom> GetEnumerator()
        {
            Clingo.HandleClingoError(m_module.GetBeginIterator(this, null, out ClingoSymbolicAtomIterator iterator));
            return new SymbolicAtomIterator(this, iterator).GetEnumerator();
        }

        /// <inheritdoc/>
        public bool TryGetValue(Symbol key, out SymbolicAtom value)
        {
            if (key != null)
            {
                throw new ArgumentNullException();
            }

            Clingo.HandleClingoError(m_module.Find(this, key, out ClingoSymbolicAtomIterator iterator));
            Clingo.HandleClingoError(m_module.IsValid(this, iterator, out bool valid));

            value = valid ? new SymbolicAtom(this, iterator) : null;
            return valid;
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator<KeyValuePair<Symbol, SymbolicAtom>> IEnumerable<KeyValuePair<Symbol, SymbolicAtom>>.GetEnumerator()
        {
            Clingo.HandleClingoError(m_module.GetBeginIterator(this, null, out ClingoSymbolicAtomIterator clingoIterator));
            var iterator = new SymbolicAtomIterator(this, clingoIterator);

            foreach (var symbolicAtom in iterator)
            {
                yield return new KeyValuePair<Symbol, SymbolicAtom>(symbolicAtom, symbolicAtom);
            }
        }

        #endregion
    }
}
