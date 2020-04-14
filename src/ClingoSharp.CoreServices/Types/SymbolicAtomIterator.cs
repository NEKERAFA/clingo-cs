using ClingoSharp.CoreServices.Interfaces;
using ClingoSharp.CoreServices.Interfaces.Modules;

namespace ClingoSharp.CoreServices.Types
{
    /// <summary>
    /// <para>Object to iterate over symbolic atoms.</para>
    /// <para>Such an iterator either points to a symbolic atom within a sequence of symbolic atoms or to the end of the sequence.</para>
    /// </summary>
    /// <remarks>
    /// Iterators are valid as long as the underlying sequence is not modified. Operations that can change this sequence are <see cref="IControlModule.Ground(Control, Part[], Callbacks.GroundCallback)"/>, clingo_control_cleanup(), and functions that modify the underlying non-ground program.
    /// </remarks>
    public sealed class SymbolicAtomIterator : IClingoObject
    {
        public ulong Value { get; set; }

        /// <summary>
        /// Convert a symbolic atom iterator object into the basic type.
        /// </summary>
        /// <param name="literal"></param>
        public static implicit operator ulong(SymbolicAtomIterator iterator)
        {
            return iterator.Value;
        }

        /// <summary>
        /// Convert a basic type into a symbolic atom iterator object.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator SymbolicAtomIterator(ulong value)
        {
            return new SymbolicAtomIterator() { Value = value };
        }
    }
}
