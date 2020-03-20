using ClingoSharp.Enums;
using System.Collections.Generic;

namespace ClingoSharp
{
    /// <summary>
    /// Enumeration of the different types of symbols.
    /// </summary>
    public class SymbolType : Enumeration
    {
        #region Class attributes

        protected new static string[] m_names => new string[]
        {
            "Infimun",
            "Number",
            "String",
            "Function",
            "Supremum"
        };

        #endregion

        #region Class Properties

        public static SymbolType Infimun => new SymbolType(0);
        public static SymbolType Number => new SymbolType(1);
        public static SymbolType String => new SymbolType(2);
        public static SymbolType Function => new SymbolType(3);
        public static SymbolType Supremun => new SymbolType(4);

        #endregion

        #region Constructors

        private SymbolType(int value) : base(value) { }

        #endregion

        #region

        /// <summary>
        /// Gets a iterator of the constants in the enumeration
        /// </summary>
        /// <returns>A <see cref="Enumerable"/> iterator with the constants in the enumeration</returns>
        public new static IEnumerable<Enumeration> GetValues()
        {
            return new SymbolType[] { Infimun, Number, String, Function, Supremun };
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