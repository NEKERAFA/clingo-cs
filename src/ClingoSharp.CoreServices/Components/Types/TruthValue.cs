using ClingoSharp.CoreServices.Interfaces;

namespace ClingoSharp.CoreServices.Components.Types
{
    /// <summary>
    /// Corresponding type to <see cref=TruthValueType"/>
    /// </summary>
    public sealed class TruthValue : IClingoObject
    {
        public int Value { get; set; }

        /// <summary>
        /// Convert a truth value object into the basic type.
        /// </summary>
        /// <param name="literal"></param>
        public static implicit operator int(TruthValue literal)
        {
            return literal.Value;
        }

        /// <summary>
        /// Convert a basic type into a truth value object.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator TruthValue(int value)
        {
            return new TruthValue() { Value = value };
        }
    }
}
