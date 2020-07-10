using ClingoSharp.NativeWrapper.Enums;
using ClingoSharp.NativeWrapper.Helpers;
using ClingoSharp.NativeWrapper.Interfaces.Modules;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Managers
{
    public class SymbolModuleImpl : ISymbol
    {
        #region Clingo C API Functions

        #region Signature Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_signature_create(string name, uint arity, bool positive, [Out] ulong[] signature);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern string clingo_signature_name(ulong signature);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern uint clingo_signature_arity(ulong signature);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool clingo_signature_is_positive(ulong signature);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool clingo_signature_is_negative(ulong signature);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool clingo_signature_is_equal_to(ulong a, ulong b);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool clingo_signature_is_less_than(ulong a, ulong b);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern UIntPtr clingo_signature_hash(ulong signature);

        #endregion

        #region Symbol Construction

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clingo_symbol_create_number(int number, [Out] ulong[] symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clingo_symbol_create_supremum([Out] ulong[] symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clingo_symbol_create_infimum([Out] ulong[] symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_create_string(string value, [Out] ulong[] symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_create_id(string name, bool positive, [Out] ulong[] symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_create_function(string name, ulong[] arguments, UIntPtr arguments_size, bool positive, [Out] ulong[] symbol);

        #endregion

        #region Symbol Inspection

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_number(ulong symbol, [Out] int[] number);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_name(ulong symbol, [Out] string[] name);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_string(ulong symbol, [Out] string[] value);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_is_positive(ulong symbol, [Out] bool[] positive);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_is_negative(ulong symbol, [Out] bool[] negative);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_arguments(ulong symbol, [Out] IntPtr arguments, [Out] UIntPtr[] arguments_size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern Enums.SymbolType clingo_symbol_type(ulong symbol);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_to_string_size(ulong symbol, [Out] UIntPtr[] size);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern int clingo_symbol_to_string(ulong symbol, [Out] byte[] value, UIntPtr size);

        #endregion

        #region Symbol Comparasion

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool clingo_symbol_is_equal_to(ulong a, ulong b);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool clingo_symbol_is_less_than(ulong a, ulong b);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern UIntPtr clingo_symbol_hash(ulong symbol);

        #endregion

        #endregion

        #region Module implementation

        #region Signature Functions

        public bool CreateSignature(string name, uint arity, bool positive, out ulong signature)
        {
            ulong[] signaturePtr = new ulong[1];
            var result = clingo_signature_create(name, arity, positive, signaturePtr);
            signature = signaturePtr[0];
            return result != 0;
        }

        public string GetSignatureName(ulong signature)
        {
            return clingo_signature_name(signature);
        }

        public uint GetSignatureArity(ulong signature)
        {
            return clingo_signature_arity(signature);
        }

        public bool IsSignaturePositive(ulong signature)
        {
            return clingo_signature_is_positive(signature);
        }

        public bool IsSignatureNegative(ulong signature)
        {
            return clingo_signature_is_negative(signature);
        }

        public bool IsSignatureEqualTo(ulong signatureA, ulong signatureB)
        {
            return clingo_signature_is_equal_to(signatureA, signatureB);
        }

        public bool IsSgnatureLessThan(ulong signatureA, ulong signatureB)
        {
            return clingo_signature_is_less_than(signatureA, signatureB);
        }

        public UIntPtr GetSignatureHash(ulong signature)
        {
            return clingo_signature_hash(signature);
        }

        #endregion

        #region Symbol Construction

        public void CreateNumber(int number, out ulong symbol)
        {
            ulong[] symbolPtr = new ulong[1];
            clingo_symbol_create_number(number, symbolPtr);
            symbol = symbolPtr[0];
        }

        public void CreateSupremum(out ulong symbol)
        {
            ulong[] symbolPtr = new ulong[1];
            clingo_symbol_create_supremum(symbolPtr);
            symbol = symbolPtr[0];
        }

        public void CreateInfimum(out ulong symbol)
        {
            ulong[] symbolPtr = new ulong[1];
            clingo_symbol_create_infimum(symbolPtr);
            symbol = symbolPtr[0];
        }

        public bool CreateString(string value, out ulong symbol)
        {
            ulong[] symbolPtr = new ulong[1];
            var result = clingo_symbol_create_string(value, symbolPtr);
            symbol = symbolPtr[0];
            return result != 0;
        }

        public bool CreateId(string name, bool positive, out ulong symbol)
        {
            ulong[] symbolPtr = new ulong[1];
            var result = clingo_symbol_create_id(name, positive, symbolPtr);
            symbol = symbolPtr[0];
            return result != 0;
        }

        public bool CreateFunction(string name, ulong[] arguments, bool positive, out ulong symbol)
        {
            ulong[] clingoArguments = arguments.Select(arg => (ulong)arg).ToArray();
            UIntPtr argumentsSize = new UIntPtr(Convert.ToUInt32(arguments != null ? arguments.Length : 0));

            ulong[] symbolPtr = new ulong[1];

            var result = clingo_symbol_create_function(name, clingoArguments, argumentsSize, positive, symbolPtr);
            symbol = symbolPtr[0];

            return result != 0;
        }

        #endregion

        #region Symbol Inspection

        public bool GetNumber(ulong symbol, out int number)
        {
            int[] numberPtr = new int[1];

            var result = clingo_symbol_number(symbol, numberPtr);
            number = numberPtr[0];

            return result != 0;
        }

        public bool GetName(ulong symbol, out string name)
        {
            string[] namePtr = new string[1];

            var result = clingo_symbol_name(symbol, namePtr);
            name = namePtr[0];

            return result != 0;
        }

        public bool GetString(ulong symbol, out string value)
        {
            string[] stringPtr = new string[1];

            var result = clingo_symbol_string(symbol, stringPtr);
            value = stringPtr[0];

            return result != 0;
        }

        public bool IsPositive(ulong symbol, out bool positive)
        {
            bool[] positivePtr = new bool[1];

            var result = clingo_symbol_is_positive(symbol, positivePtr);
            positive = positivePtr[0];

            return result != 0;
        }

        public bool IsNegative(ulong symbol, out bool negative)
        {
            bool[] negativePtr = new bool[1];

            var result = clingo_symbol_is_negative(symbol, negativePtr);
            negative = negativePtr[0];

            return result != 0;
        }

        public bool GetArguments(ulong symbol, out ulong[] arguments)
        {
            IntPtr argumentsPtr = new IntPtr();
            UIntPtr[] argumentsSizePtr = new UIntPtr[1];

            var result = clingo_symbol_arguments(symbol, argumentsPtr, argumentsSizePtr);

            // Copies all symbols in a managed array
            int argumentsSize = Convert.ToInt32(argumentsSizePtr[0].ToUInt32());
            arguments = new ulong[argumentsSize];
            MarshalHelper.Copy(argumentsPtr, arguments, 0, argumentsSize);

            return result != 0;
        }

        public SymbolType GetType(ulong symbol)
        {
            return clingo_symbol_type(symbol);
        }

        public bool ToString(ulong symbol, out string value)
        {
            UIntPtr[] stringSizePtr = new UIntPtr[1];

            var success = clingo_symbol_to_string_size(symbol, stringSizePtr);

            if (success != 0)
            {
                byte[] stringPtr = new byte[stringSizePtr[0].ToUInt32()];

                success = clingo_symbol_to_string(symbol, stringPtr, stringSizePtr[0]);

                value = System.Text.Encoding.UTF8.GetString(stringPtr);
                value = value.Replace("\0", string.Empty).Trim();

                return success != 0;
            }

            value = null;
            return false;
        }

        #endregion

        #region Symbol Comparasion

        public bool IsEqualTo(ulong symbolA, ulong symbolB)
        {
            return clingo_symbol_is_equal_to(symbolA, symbolB);
        }

        public bool IsLessThan(ulong symbolA, ulong symbolB)
        {
            return clingo_symbol_is_less_than(symbolA, symbolB);
        }

        public UIntPtr GetHash(ulong symbol)
        {
            return clingo_symbol_hash(symbol);
        }

        #endregion

        #endregion
    }
}
