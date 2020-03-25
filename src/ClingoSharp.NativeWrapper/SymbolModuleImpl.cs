using AutoMapper;
using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.Interfaces.Modules;
using ClingoSharp.CoreServices.Types;
using ClingoSharp.NativeWrapper.Enums;
using ClingoSharp.NativeWrapper.Extensions;
using System;
using System.Runtime.InteropServices;
using clingo_symbol = System.UInt64;
using clingo_signature = System.UInt64;

namespace ClingoSharp.NativeWrapper
{
    public class SymbolModuleImpl : ISymbolModule
    {
        private readonly IMapper m_mapper;

        #region Constructors

        public SymbolModuleImpl()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateClingoMaps();
            });

            m_mapper = config.CreateMapper();
        }


        #endregion

        #region Clingo C API Functions

        #region Signature Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_signature_create(string name, uint arity, bool positive, [Out] clingo_signature[] signature);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern string clingo_signature_name(clingo_signature signature);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern uint clingo_signature_arity(clingo_signature signature);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool clingo_signature_is_positive(clingo_signature signature);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool clingo_signature_is_negative(clingo_signature signature);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool clingo_signature_is_equal_to(clingo_signature a, clingo_signature b);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool clingo_signature_is_less_than(clingo_signature a, clingo_signature b);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern UIntPtr clingo_signature_hash(clingo_signature signature);

        #endregion

        #region Symbol Construction

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clingo_symbol_create_number(int number, [Out] clingo_symbol[] symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clingo_symbol_create_supremum([Out] clingo_symbol[] symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clingo_symbol_create_infimum([Out] clingo_symbol[] symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_create_string(string value, [Out] clingo_symbol[] symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_create_id(string name, bool positive, [Out] clingo_symbol[] symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_create_function(string name, clingo_symbol[] arguments, UIntPtr arguments_size, bool positive, [Out] clingo_symbol[] symbol);

        #endregion

        #region Symbol Inspection

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_number(clingo_symbol symbol, [Out] int[] number);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_name(clingo_symbol symbol, [Out] string[] name);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_string(clingo_symbol symbol, [Out] string[] value);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_is_positive(clingo_symbol symbol, [Out] bool[] positive);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_is_negative(clingo_symbol symbol, [Out] bool[] negative);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_arguments(clingo_symbol symbol, [Out] IntPtr arguments, [Out] UIntPtr[] arguments_size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern clingo_symbol_type clingo_symbol_type(clingo_symbol symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_to_string_size(clingo_symbol symbol, [Out] UIntPtr[] size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_to_string(clingo_symbol symbol, [Out] char[] value, UIntPtr size);

        #endregion

        #region Symbol Comparasion

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool clingo_symbol_is_equal_to(clingo_symbol a, clingo_symbol b);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool clingo_symbol_is_less_than(clingo_symbol a, clingo_symbol b);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern UIntPtr clingo_symbol_hash(clingo_symbol symbol);

        #endregion

        #endregion

        #region Module implementation

        #region Signature Functions

        public bool CreateSignature(string name, uint arity, bool positive, out Signature signature)
        {
            clingo_signature[] signaturePtr = new clingo_signature[1];

            var result = clingo_signature_create(name, arity, positive, signaturePtr);
            signature = new Signature() { Value = signaturePtr[0] };

            return result != 0;
        }

        public string GetName(Signature signature)
        {
            return clingo_signature_name(signature.Value);
        }

        public uint GetArity(Signature signature)
        {
            return clingo_signature_arity(signature.Value);
        }

        public bool IsPositive(Signature signature)
        {
            return clingo_signature_is_positive(signature.Value);
        }

        public bool IsNegative(Signature signature)
        {
            return clingo_signature_is_negative(signature.Value);
        }

        public bool IsEqualTo(Signature signatureA, Signature signatureB)
        {
            return clingo_signature_is_equal_to(signatureA.Value, signatureB.Value);
        }

        public bool IsLessThan(Signature signatureA, Signature signatureB)
        {
            return clingo_signature_is_less_than(signatureA.Value, signatureB.Value);
        }

        public UIntPtr GetHash(Signature signature)
        {
            return clingo_signature_hash(signature.Value);
        }

        #endregion

        #region Symbol Construction

        public void CreateNumber(int number, out Symbol symbol)
        {
            clingo_symbol[] symbolPtr = new clingo_symbol[1];
            clingo_symbol_create_number(number, symbolPtr);
            symbol = new Symbol() { Value = symbolPtr[0] };
        }

        public void CreateSupremum(out Symbol symbol)
        {
            clingo_symbol[] symbolPtr = new clingo_symbol[1];
            clingo_symbol_create_supremum(symbolPtr);
            symbol = new Symbol() { Value = symbolPtr[0] };
        }

        public void CreateInfimum(out Symbol symbol)
        {
            clingo_symbol[] symbolPtr = new clingo_symbol[1];
            clingo_symbol_create_infimum(symbolPtr);
            symbol = new Symbol() { Value = symbolPtr[0] };
        }

        public bool CreateString(string value, out Symbol symbol)
        {
            clingo_symbol[] symbolPtr = new clingo_symbol[1];
            var result = clingo_symbol_create_string(value, symbolPtr);
            symbol = new Symbol() { Value = symbolPtr[0] };
            return result != 0;
        }

        public bool CreateId(string name, bool positive, out Symbol symbol)
        {
            clingo_symbol[] symbolPtr = new clingo_symbol[1];
            var result = clingo_symbol_create_id(name, positive, symbolPtr);
            symbol = new Symbol() { Value = symbolPtr[0] };
            return result != 0;
        }

        public bool CreateFunction(string name, Symbol[] arguments, bool positive, out Symbol symbol)
        {
            clingo_symbol[] clingoArguments = m_mapper.Map<Symbol[], clingo_symbol[]>(arguments);
            UIntPtr argumentsSize = new UIntPtr(Convert.ToUInt32(arguments != null ? arguments.Length : 0));

            clingo_symbol[] symbolPtr = new clingo_symbol[1];

            var result = clingo_symbol_create_function(name, clingoArguments, argumentsSize, positive, symbolPtr);
            symbol = new Symbol() { Value = symbolPtr[0] };

            return result != 0;
        }

        #endregion

        #region Symbol Inspection

        public bool GetNumber(Symbol symbol, out int number)
        {
            int[] numberPtr = new int[1];

            var result = clingo_symbol_number(symbol.Value, numberPtr);
            number = numberPtr[0];

            return result != 0;
        }

        public bool GetName(Symbol symbol, out string name)
        {
            string[] namePtr = new string[1];

            var result = clingo_symbol_name(symbol.Value, namePtr);
            name = namePtr[0];

            return result != 0;
        }

        public bool GetString(Symbol symbol, out string value)
        {
            string[] stringPtr = new string[1];

            var result = clingo_symbol_string(symbol.Value, stringPtr);
            value = stringPtr[0];

            return result != 0;
        }

        public bool IsPositive(Symbol symbol, out bool positive)
        {
            bool[] positivePtr = new bool[1];

            var result = clingo_symbol_is_positive(symbol.Value, positivePtr);
            positive = positivePtr[0];

            return result != 0;
        }

        public bool IsNegative(Symbol symbol, out bool negative)
        {
            bool[] negativePtr = new bool[1];

            var result = clingo_symbol_is_negative(symbol.Value, negativePtr);
            negative = negativePtr[0];

            return result != 0;
        }

        public bool GetArguments(Symbol symbol, out Symbol[] arguments)
        {
            IntPtr argumentsPtr = new IntPtr();
            UIntPtr[] argumentsSizePtr = new UIntPtr[1];

            var result = clingo_symbol_arguments(symbol.Value, argumentsPtr, argumentsSizePtr);

            // Copies all symbols in a managed array
            int argumentsSize = Convert.ToInt32(argumentsSizePtr[0].ToUInt32());
            long[] clingoArguments = new long[argumentsSize];
            Marshal.Copy(argumentsPtr, clingoArguments, 0, argumentsSize);

            // Converts all clingo_symbol in a Symbol object
            arguments = new Symbol[argumentsSize];
            for (int i = 0; i < argumentsSize; i++)
            {
                arguments[i] = new Symbol() { Value = Convert.ToUInt64(clingoArguments[i]) };
            }

            return result != 0;
        }

        public SymbolType GetType(Symbol symbol)
        {
            return (SymbolType)clingo_symbol_type(symbol.Value);
        }

        public bool ToString(Symbol symbol, out string value)
        {
            UIntPtr[] stringSizePtr = new UIntPtr[1];

            var success = clingo_symbol_to_string_size(symbol.Value, stringSizePtr);

            if (success != 0)
            {
                char[] stringPtr = new char[stringSizePtr[0].ToUInt32()];

                success = clingo_symbol_to_string(symbol.Value, stringPtr, stringSizePtr[0]);

                value = new string(stringPtr);
                value = value.Replace("\0", string.Empty).Trim();

                return success != 0;
            }

            value = null;
            return false;
        }

        #endregion

        #region Symbol Comparasion

        public bool IsEqualTo(Symbol symbolA, Symbol symbolB)
        {
            return clingo_symbol_is_equal_to(symbolA.Value, symbolB.Value);
        }

        public bool IsLessThan(Symbol symbolA, Symbol symbolB)
        {
            return clingo_symbol_is_less_than(symbolA.Value, symbolB.Value);
        }

        public UIntPtr GetHash(Symbol symbol)
        {
            return clingo_symbol_hash(symbol.Value);
        }

        #endregion

        #endregion
    }
}
