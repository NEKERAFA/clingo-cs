using System;

namespace ClingoSharp.NativeWrapper.Interfaces.Modules
{
    /// <summary>
    /// Inspection of atoms occurring in ground logic programs
    /// </summary>
    public interface ISymbolicAtoms : IClingoModule
    {
        /// <summary>
        /// Get the number of different atoms occurring in a logic program.
        /// </summary>
        /// <param name="atoms">the target</param>
        /// <param name="size">the number of atoms</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetSize(IntPtr atoms, out UIntPtr size);

        /// <summary>
        /// Get a forward iterator to the beginning of the sequence of all symbolic atoms optionally restricted to a given signature.
        /// </summary>
        /// <param name="atoms">the target</param>
        /// <param name="signature">optional signature</param>
        /// <param name="iterator">the resulting iterator</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetBeginIterator(IntPtr atoms, ulong? signature, out ulong iterator);

        /// <summary>
        /// Iterator pointing to the end of the sequence of symbolic atoms.
        /// </summary>
        /// <param name="atoms">the target</param>
        /// <param name="iterator">the resulting iterator</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetEndIterator(IntPtr atoms, out ulong iterator);

        /// <summary>
        /// Find a symbolic atom given its symbolic representation.
        /// </summary>
        /// <param name="atoms">the target</param>
        /// <param name="symbol">the symbol to lookup</param>
        /// <param name="iterator">iterator pointing to the symbolic atom or to the end of the sequence if no corresponding atom is found</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool Find(IntPtr atoms, ulong symbol, out ulong iterator);

        /// <summary>
        /// Check if two iterators point to the same element (or end of the sequence).
        /// </summary>
        /// <param name="atoms">the target</param>
        /// <param name="iteratorA">the first iterator</param>
        /// <param name="iteratorB">the second iterator</param>
        /// <param name="equal">whether the two iterators are equal</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool IteratorIsEqualTo(IntPtr atoms, ulong iteratorA, ulong iteratorB, out bool equal);

        /// <summary>
        /// <para>Check whether an atom is external.</para>
        /// <para>An atom is external if it has been defined using an external directive and has not been released or defined by a rule.</para>
        /// </summary>
        /// <param name="atoms">the target</param>
        /// <param name="iterator">iterator to the atom</param>
        /// <param name="symbol">whether the atom is a external</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetSymbol(IntPtr atoms, ulong iterator, out ulong symbol);

        /// <summary>
        /// Check whether an atom is a fact.
        /// </summary>
        /// <remarks>
        /// This does not determine if an atom is a cautious consequence. The grounding or solving component's simplifications can only detect this in some cases.
        /// </remarks>
        /// <param name="atoms">the target</param>
        /// <param name="iterator">iterator to the atom</param>
        /// <param name="fact">whether the atom is a fact</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool IsFact(IntPtr atoms, ulong iterator, out bool fact);

        /// <summary>
        /// Get the symbolic representation of an atom.
        /// </summary>
        /// <param name="atoms">the target</param>
        /// <param name="iterator">iterator to the atom</param>
        /// <param name="external">the resulting symbol</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool IsExternal(IntPtr atoms, ulong iterator, out bool external);

        /// <summary>
        /// <para>Returns the (numeric) aspif literal corresponding to the given symbolic atom.</para>
        /// <para>Such a literal can be mapped to a solver literal (see the Theory Propagation module) or be used in rules in aspif format (see the Program Building module).</para>
        /// </summary>
        /// <param name="atoms">the target</param>
        /// <param name="iterator">iterator to the atom</param>
        /// <param name="literal">the associated literal</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetLiteral(IntPtr atoms, ulong iterator, out int literal);

        /// <summary>
        /// Get the predicate signatures occurring in a logic program.
        /// </summary>
        /// <param name="atoms">the target</param>
        /// <param name="signatures">the resulting signatures</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetSignatures(IntPtr atoms, out ulong[] signatures);

        /// <summary>
        /// Get an iterator to the next element in the sequence of symbolic atoms.
        /// </summary>
        /// <param name="atoms">the target</param>
        /// <param name="iterator">the current iterator</param>
        /// <param name="next">the succeeding iterator</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetNext(IntPtr atoms, ulong iterator, out ulong next);

        /// <summary>
        /// Check whether the given iterator points to some element with the sequence of symbolic atoms or to the end of the sequence.
        /// </summary>
        /// <param name="atoms">the target</param>
        /// <param name="iterator">the iterator</param>
        /// <param name="valid">whether the iterator points to some element within the sequence</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool IsValid(IntPtr atoms, ulong iterator, out bool valid);
    }
}
