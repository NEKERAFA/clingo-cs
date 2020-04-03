using ClingoSharp.CoreServices.Interfaces;

namespace ClingoSharp.CoreServices.Types
{
    /// <summary>
    /// <para>Represents a predicate signature.</para>
    /// <para>Signatures have a name and an arity, and can be positive or negative(to represent classical negation).</para>
    /// </summary>
    public sealed class Signature : IClingoObject
    {
        public ulong Value { get; set; }

        /// <summary>
        /// Convert a signature object into the basic type.
        /// </summary>
        /// <param name="literal"></param>
        public static implicit operator ulong(Signature signature)
        {
            return signature.Value;
        }

        /// <summary>
        /// Convert a basic type into a signature object.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Signature(ulong value)
        {
            return new Signature() { Value = value };
        }
    }
}
