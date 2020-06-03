using ClingoSharp.NativeWrapper.Interfaces.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ClingoSharp
{
    /// <summary>
    /// <para>This class provides read-only access to the atom base of the grounder.</para>
    /// <para>It implements <see cref="ICollection{T}"/> of <see cref="SymbolicAtom"/> and <see cref="IDictionary{TKey, TValue}"/> of &lt; <see cref="Symbol"/> , <see cref="SymbolicAtom"/> &gt;.</para>
    /// </summary>
    public sealed class SymbolicAtoms : ICollection<SymbolicAtom>, IDictionary<Symbol, SymbolicAtom>
    {
        #region Attributes

        private static ISymbolicAtoms m_symbolicAtomsModule = null;

        private readonly IntPtr m_clingoSymbolicAtoms;

        #endregion

        #region Class Properties

        internal static ISymbolicAtoms SymbolicAtomsModule
        {
            get
            {
                if (m_symbolicAtomsModule == null)
                    m_symbolicAtomsModule = Clingo.ClingoRepository.GetModule<ISymbolicAtoms>();

                return m_symbolicAtomsModule;
            }
        }

        #endregion

        #region Interface Properties

        /// <summary>
        /// <para>The list of predicate signatures occurring in the program.</para>
        /// <para>The <see cref="bool"/> indicates the sign of the signature.</para>
        /// </summary>
        public List<Tuple<string, int, bool>> Signatures
        {
            get
            {
                Clingo.HandleClingoError(SymbolicAtomsModule.GetSignatures(m_clingoSymbolicAtoms, out ulong[] signatures));
                return signatures.Select(s => new Tuple<string, int, bool>(
                    Symbol.SymbolModule.GetSignatureName(s),
                    Convert.ToInt32(Symbol.SymbolModule.GetSignatureArity(s)),
                    Symbol.SymbolModule.IsSignaturePositive(s)
                )).ToList();
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get
            {
                Clingo.HandleClingoError(SymbolicAtomsModule.GetSize(m_clingoSymbolicAtoms, out UIntPtr size));
                return Convert.ToInt32(size.ToUInt32());
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc/>
        public ICollection<Symbol> Keys
        {
            get
            {
                return Values.Select(symbolicAtom => (Symbol)symbolicAtom).ToList();
            }
        }

        /// <inheritdoc/>
        public ICollection<SymbolicAtom> Values
        {
            get
            {
                Clingo.HandleClingoError(SymbolicAtomsModule.GetBeginIterator(m_clingoSymbolicAtoms, 0, out ulong clingoIterator));
                var iterator = new SymbolicAtomIterator(m_clingoSymbolicAtoms, clingoIterator);

                List<SymbolicAtom> symbolicAtoms = new List<SymbolicAtom>();
                foreach (var symbolicAtom in iterator)
                {
                    symbolicAtoms.Add(symbolicAtom);
                }

                return symbolicAtoms;
            }
        }

        /// <inheritdoc/>
        public SymbolicAtom this[Symbol key]
        { 
            get
            {
                if (!TryGetValue(key, out SymbolicAtom value))
                {
                    throw new KeyNotFoundException();
                }

                return value;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region Class Methods

        public static implicit operator IntPtr(SymbolicAtoms symbolicAtoms)
        {
            return symbolicAtoms.m_clingoSymbolicAtoms;
        }

        #endregion

        #region Constructors

        public SymbolicAtoms(IntPtr clingoSymbolicAtoms)
        {
            m_clingoSymbolicAtoms = clingoSymbolicAtoms;
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
            Clingo.HandleClingoError(Symbol.SymbolModule.CreateSignature(name, Convert.ToUInt32(arity), positive, out ulong signature));
            Clingo.HandleClingoError(SymbolicAtomsModule.GetBeginIterator(m_clingoSymbolicAtoms, signature, out ulong clingoIterator));
            
            var iterator = new SymbolicAtomIterator(m_clingoSymbolicAtoms, clingoIterator);
            foreach (var symbolicAtom in iterator)
            {
                yield return symbolicAtom;
            }
        }

        /// <inheritdoc/>
        public bool ContainsKey(Symbol key)
        {
            if (key != null)
            {
                throw new ArgumentNullException();
            }

            Clingo.HandleClingoError(SymbolicAtomsModule.Find(m_clingoSymbolicAtoms, (ulong)key, out ulong iterator));
            Clingo.HandleClingoError(SymbolicAtomsModule.IsValid(m_clingoSymbolicAtoms, iterator, out bool valid));

            return valid;
        }

        /// <inheritdoc/>
        public IEnumerator<SymbolicAtom> GetEnumerator()
        {
            Clingo.HandleClingoError(SymbolicAtomsModule.GetBeginIterator(m_clingoSymbolicAtoms, null, out ulong iterator));
            return new SymbolicAtomIterator(m_clingoSymbolicAtoms, iterator).GetEnumerator();
        }

        /// <inheritdoc/>
        public bool TryGetValue(Symbol key, out SymbolicAtom value)
        {
            if (key != null)
            {
                throw new ArgumentNullException();
            }

            Clingo.HandleClingoError(SymbolicAtomsModule.Find(m_clingoSymbolicAtoms, (ulong)key, out ulong iterator));
            Clingo.HandleClingoError(SymbolicAtomsModule.IsValid(m_clingoSymbolicAtoms, iterator, out bool valid));

            value = valid ? new SymbolicAtom(m_clingoSymbolicAtoms, iterator) : null;
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
            Clingo.HandleClingoError(SymbolicAtomsModule.GetBeginIterator(m_clingoSymbolicAtoms, null, out ulong clingoIterator));
            var iterator = new SymbolicAtomIterator(m_clingoSymbolicAtoms, clingoIterator);

            foreach (var symbolicAtom in iterator)
            {
                yield return new KeyValuePair<Symbol, SymbolicAtom>(symbolicAtom, symbolicAtom);
            }
        }

        /// <inheritdoc/>
        public void Add(SymbolicAtom item)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public void Clear()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public bool Contains(SymbolicAtom item)
        {
            return ContainsKey(item);
        }

        /// <inheritdoc/>
        public void CopyTo(SymbolicAtom[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException("arrayIndex");
            if (Count > (array.Length - arrayIndex)) throw new ArgumentException();

            Clingo.HandleClingoError(SymbolicAtomsModule.GetBeginIterator(m_clingoSymbolicAtoms, null, out ulong clingoIterator));
            var iterator = new SymbolicAtomIterator(m_clingoSymbolicAtoms, clingoIterator);

            int currentPos = arrayIndex;
            foreach (var symbolicAtom in iterator)
            {
                array[currentPos] = symbolicAtom;
                currentPos++;
            }
        }

        /// <inheritdoc/>
        public bool Remove(SymbolicAtom item)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public void Add(Symbol key, SymbolicAtom value)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public bool Remove(Symbol key)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<Symbol, SymbolicAtom> item)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<Symbol, SymbolicAtom> item)
        {
            if (Symbol.SymbolModule.IsEqualTo(item.Key, item.Value))
            {
                return ContainsKey(item.Key);
            }

            return false;
        }

        public void CopyTo(KeyValuePair<Symbol, SymbolicAtom>[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException("arrayIndex");
            if (Count > (array.Length - arrayIndex)) throw new ArgumentException();

            Clingo.HandleClingoError(SymbolicAtomsModule.GetBeginIterator(m_clingoSymbolicAtoms, null, out ulong clingoIterator));
            var iterator = new SymbolicAtomIterator(m_clingoSymbolicAtoms, clingoIterator);

            int currentPos = arrayIndex;
            foreach (var symbolicAtom in iterator)
            {
                array[currentPos] = new KeyValuePair<Symbol, SymbolicAtom>(symbolicAtom, symbolicAtom);
                currentPos++;
            }
        }

        public bool Remove(KeyValuePair<Symbol, SymbolicAtom> item)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
