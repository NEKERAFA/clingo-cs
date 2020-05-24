using ClingoSharp.CoreServices.Interfaces;

namespace ClingoSharp.CoreServices.Components.Types
{
    /// <summary>
    /// Unsigned integer type used in various places.
    /// </summary>
    public sealed class Id : IClingoObject
    {
        public uint Value { get; set; }

        /// <summary>
        /// Convert a id object into the basic type.
        /// </summary>
        /// <param name="literal"></param>
        public static implicit operator uint(Id id)
        {
            return id.Value;
        }

        /// <summary>
        /// Convert a basic type into a id object.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Id(uint value)
        {
            return new Id() { Value = value };
        }
    }
}
