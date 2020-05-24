using ClingoSharp.Enums;

namespace ClingoSharp
{
    /// <summary>
    /// Enumeration of the different types of symbols.
    /// </summary>
    public sealed class SymbolType : Enumeration
    {
        #region Class attributes

        private static string[] SymbolNames => new string[]
        {
            "Infimum",
            "Number",
            "String",
            "Function",
            "Supremum"
        };

        #endregion

        #region Class Properties

        /// <summary>
        /// The <c>#inf</c> symbol.
        /// </summary>
        public static SymbolType Infimum => new SymbolType(0);

        /// <summary>
        /// A numeric symbol, e.g., <c>1</c>.
        /// </summary>
        public static SymbolType Number => new SymbolType(1);

        /// <summary>
        /// A string symbol, e.g., <c>"a"</c>.
        /// </summary>
        public static SymbolType String => new SymbolType(2);

        /// <summary>
        /// A function symbol, e.g., <c>c</c>, <c>(1, "a")</c> or <c>f(1, "a")</c>.
        /// </summary>
        public static SymbolType Function => new SymbolType(3);

        /// <summary>
        /// The <c>#sup</c> symbol.
        /// </summary>
        public static SymbolType Supremum => new SymbolType(4);

        #endregion

        #region Constructors

        private SymbolType(int value) : base(value, SymbolNames[value]) { }

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