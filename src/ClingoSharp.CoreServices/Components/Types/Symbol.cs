using ClingoSharp.CoreServices.Interfaces;

namespace ClingoSharp.CoreServices.Components.Types
{
    /// <summary>
    /// <para>Represents a symbol.</para>
    /// <para>This includes numbers, strings, functions (including constants when arguments are empty and tuples when the name is empty), <c>#inf</c> and <c>#sup</c>.</para>
    /// </summary>
    public sealed class Symbol : IClingoObject
    {
        public ulong Value { get; set; }

        /// <summary>
        /// Convert a symbol object into the basic type.
        /// </summary>
        /// <param name="literal"></param>
        public static implicit operator ulong(Symbol symbol)
        {
            return symbol.Value;
        }

        /// <summary>
        /// Convert a basic type into a symbol object.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Symbol(ulong value)
        {
            return new Symbol() { Value = value };
        }
    }
}
