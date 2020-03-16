using AutoMapper;
using ClingoSharp.CoreServices.Types;
using clingo_symbol = System.UInt64;
using clingo_literal = System.Int32;

namespace ClingoSharp.NativeWrapper.Extensions
{
    internal static class MapperExtension
    {
        public static void CreateClingoMaps(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Symbol, clingo_symbol>()
                .ConvertUsing(src => src.Value);

            cfg.CreateMap<clingo_symbol, Symbol>()
                .ConvertUsing(src => new Symbol() { Value = src });

            cfg.CreateMap<Literal, clingo_literal>()
                .ConvertUsing(src => src.Value);

            cfg.CreateMap<clingo_literal, Literal>()
                .ConvertUsing(src => new Literal() { Value = src });
        }
    }
}
