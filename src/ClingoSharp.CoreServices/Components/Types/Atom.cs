using ClingoSharp.CoreServices.Interfaces;

namespace ClingoSharp.CoreServices.Components.Types
{
    /// <summary>
    /// Unsigned integer type used for aspif atoms.
    /// </summary>
    public sealed class Atom : IClingoObject
    {
        public uint Value { get; set; }

        /// <summary>
        /// Convert a atom object into the basic type.
        /// </summary>
        /// <param name="literal"></param>
        public static implicit operator uint(Atom atom)
        {
            return atom.Value;
        }

        /// <summary>
        /// Convert a basic type into a atom object.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Atom(uint value)
        {
            return new Atom() { Value = value };
        }
    }
}
