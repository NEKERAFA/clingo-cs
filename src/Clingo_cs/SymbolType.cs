using Clingo_cs.Enums;
using static Clingo_c.Clingo_c;

namespace Clingo_cs.Symbols
{
    /// <summary>
    /// Enumeration of symbols types.
    /// </summary>
    public sealed class SymbolType : Enumeration
    {
        #region Class Properties

        /// <summary>
        /// A function symbol, e.g., <c>c</c>, <c>(1,"a")</c>, or <c>f(1,"a")</c>.
        /// </summary>
        public static SymbolType Function => new SymbolType((int)clingo_symbol_type_t.clingo_symbol_type_function, "Function");

        /// <summary>
        /// The <c>#inf symbol</c>
        /// </summary>
        public static SymbolType Infimum => new SymbolType((int)clingo_symbol_type_t.clingo_symbol_type_infimum, "Infimum");

        /// <summary>
        /// A numeric symbol, e.g., <c>1</c>.
        /// </summary>
        public static SymbolType Number => new SymbolType((int)clingo_symbol_type_t.clingo_symbol_type_number, "Number");

        /// <summary>
        /// A string symbol, e.g., <c>"a"</c>
        /// </summary>
        public static SymbolType String => new SymbolType((int)clingo_symbol_type_t.clingo_symbol_type_string, "String");

        /// <summary>
        /// The <c>#sup symbol</c>
        /// </summary>
        public static SymbolType Supremum => new SymbolType((int)clingo_symbol_type_t.clingo_symbol_type_supremum, "Supremum");

        #endregion

        #region Constructors

        private SymbolType(int value, string name) : base(value, name) { }

        #endregion

        #region Instance methods

        /// <inheritdoc/>
        public override int CompareTo(Enumeration other)
        {
            if ((other == null) || !(other is SymbolType))
            {
                return 1;
            }

            return Value.CompareTo(other.Value);
        }

        /// <inheritdoc/>
        public override bool Equals(Enumeration other)
        {
            if ((other == null) || !(other is SymbolType))
            {
                return false;
            }

            return Value.Equals(other.Value);
        }

        #endregion
    }
}
