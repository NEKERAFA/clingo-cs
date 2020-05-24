using ClingoSharp.CoreServices.Interfaces;

namespace ClingoSharp.CoreServices.Components.Types
{
    /// <summary>
    /// Signed integer type used for aspif and solver literals.
    /// </summary>
    public sealed class Literal : IClingoObject
    {
        public int Value { get; set; }

        /// <summary>
        /// Convert a literal object into the basic type.
        /// </summary>
        /// <param name="literal"></param>
        public static implicit operator int(Literal literal)
        {
            return literal.Value;
        }

        /// <summary>
        /// Convert a basic type into a literal object.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Literal(int value)
        {
            return new Literal() { Value = value };
        }
    }
}
