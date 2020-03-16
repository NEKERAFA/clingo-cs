using ClingoSharp.Enums;
using System.Collections.Generic;

namespace ClingoSharp
{
    public class SymbolType : Enumeration
    {
        private static string[] TypeNames => new string[]
        {
            "Infimun",
            "Number",
            "String",
            "Function",
            "Supremum"
        };

        public static SymbolType Infimun => new SymbolType(0);
        public static SymbolType Number => new SymbolType(1);
        public static SymbolType String => new SymbolType(2);
        public static SymbolType Function => new SymbolType(3);
        public static SymbolType Supremun => new SymbolType(4);

        private SymbolType(int value) : base(value, TypeNames[value]) { }

        public new static IEnumerable<string> GetNames()
        {
            return TypeNames;
        }

        public new static IEnumerable<Enumeration> GetValues()
        {
            return new SymbolType[] { Infimun, Number, String, Function, Supremun };
        }

        public override int CompareTo(Enumeration other)
        {
            if ((other == null) || !(other is SymbolType))
            {
                return 1;
            }

            return Value.CompareTo(other.Value);
        }

        public override bool Equals(Enumeration other)
        {
            if ((other == null) || !(other is SymbolType))
            {
                return false;
            }

            return Value.Equals(other.Value);
        }

        public override string ToString()
        {
            return $"CligoSharp.SymbolType<{Name}>";
        }
    }
}