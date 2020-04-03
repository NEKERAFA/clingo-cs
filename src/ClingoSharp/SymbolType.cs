using ClingoSharp.Enums;
using System.Collections.Generic;

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
            "Infimun",
            "Number",
            "String",
            "Function",
            "Supremum"
        };

        #endregion

        #region Class Properties

        public static SymbolType Infimum => new SymbolType(0);
        public static SymbolType Number => new SymbolType(1);
        public static SymbolType String => new SymbolType(2);
        public static SymbolType Function => new SymbolType(3);
        public static SymbolType Supremum => new SymbolType(4);

        #endregion

        #region Instance Properties

        public new string Name => SymbolNames[Value];

        #endregion

        #region Constructors

        private SymbolType(int value) : base(value) { }

        #endregion

        #region Class Methods

        /// <inheritdoc/>
        public new static IEnumerable<string> GetNames()
        {
            return (string[])SymbolNames.Clone();
        }

        /// <inheritdoc/>
        public new static IEnumerable<Enumeration> GetValues()
        {
            return new SymbolType[] { Infimum, Number, String, Function, Supremum };
        }

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

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"CligoSharp.SymbolType<{Name}>";
        }

        #endregion
    }
}