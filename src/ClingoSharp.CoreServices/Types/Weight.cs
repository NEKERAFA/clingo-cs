using ClingoSharp.CoreServices.Interfaces;

namespace ClingoSharp.CoreServices.Types
{
    /// <summary>
    /// Signed integer type for weights in sum aggregates and minimize constraints.
    /// </summary>
    public sealed class Weight : IClingoObject
    {
        public int Value { get; set; }

        /// <summary>
        /// Convert a weight object into the basic type.
        /// </summary>
        /// <param name="literal"></param>
        public static implicit operator int(Weight literal)
        {
            return literal.Value;
        }

        /// <summary>
        /// Convert a basic type into a weight object.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Weight(int value)
        {
            return new Weight() { Value = value };
        }
    }
}
