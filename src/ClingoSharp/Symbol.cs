using System.Collections.Generic;

namespace ClingoSharp
{
    public class Symbol
    {
        internal ulong ClingoSymbol { get; set; }
        public SymbolType Type { get; internal set; }

        internal Symbol() {}

        internal CoreServices.Types.Symbol ConvertTo()
        {
            return new CoreServices.Types.Symbol()
            {
                Value = ClingoSymbol
            };
        }
    }
}
