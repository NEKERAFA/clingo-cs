using ClingoSharp.CoreServices.Components.Types;
using ClingoSharp.NativeWrapper.Types;
using System;
using System.Linq;

namespace ClingoSharp.NativeWrapper.Utils
{
    internal static class MapperHelper
    {
        public static Symbol[] MapSymbols(ulong[] clingoSymbols)
        {
            return clingoSymbols?.Select(symbol => (Symbol)symbol).ToArray();
        }

        public static ulong[] MapSymbols(Symbol[] symbols)
        {
            return symbols?.Select(symbol => (ulong)symbol).ToArray();
        }

        public static Location Map(clingo_location clingoLocation)
        {
            return new Location()
            {
                BeginFile = clingoLocation.begin_file,
                EndFile = clingoLocation.end_file,
                BeginLine = (uint)clingoLocation.begin_line,
                EndLine = (uint)clingoLocation.end_line,
                BeginColumn = (uint)clingoLocation.begin_column,
                EndColumn = (uint)clingoLocation.end_column
            };
        }

        public static clingo_part Map(Part part)
        {
            return new clingo_part()
            {
                name = part.Name,
                params_list = MapSymbols(part.Params),
                size = new UIntPtr(Convert.ToUInt32(part.Params == null ? 0 : part.Params.Length))
            };
        }

        public static clingo_part[] MapParts(Part[] parts)
        {
            return parts?.Select(part => Map(part)).ToArray();
        }
    }
}
