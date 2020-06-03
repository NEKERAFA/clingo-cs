using ClingoSharp.NativeWrapper.Enums;
using System;

namespace ClingoSharp.NativeWrapper.Interfaces.Modules
{
    /// <summary>
    /// Working with (evaluated) ground terms and related functions.
    /// </summary>
    public interface ISymbol : IClingoModule
    {
        #region Signature Functions

        /// <summary>
        /// Create a new signature.
        /// </summary>
        /// <param name="name">name of the signature</param>
        /// <param name="arity">arity of the signature</param>
        /// <param name="positive">false if the signature has a classical negation sign</param>
        /// <param name="signature">the resulting signature</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool CreateSignature(string name, uint arity, bool positive, out ulong signature);

        /// <summary>
        /// Get the name of a signature.
        /// </summary>
        /// <param name="signature">the target signature</param>
        /// <returns>the name of the signature</returns>
        string GetSignatureName(ulong signature);

        /// <summary>
        /// Get the arity of a signature.
        /// </summary>
        /// <param name="signature">the target signature</param>
        /// <returns>the arity of the signature</returns>
        uint GetSignatureArity(ulong signature);

        /// <summary>
        /// Whether the signature is positive (is not classically negated).
        /// </summary>
        /// <param name="signature">the target signature</param>
        /// <returns>whether the signature has no sign</returns>
        bool IsSignaturePositive(ulong signature);

        /// <summary>
        /// Whether the signature is negative (is classically negated).
        /// </summary>
        /// <param name="signature">the target signature</param>
        /// <returns>whether the signature has a sign</returns>
        bool IsSignatureNegative(ulong signature);

        /// <summary>
        /// Check if two signatures are equal.
        /// </summary>
        /// <param name="signatureA">first signature</param>
        /// <param name="signatureB">second signature</param>
        /// <returns>whether a == b</returns>
        bool IsSignatureEqualTo(ulong signatureA, ulong signatureB);

        /// <summary>
        /// Check if a signature is less than another signature.
        /// </summary>
        /// Signatures are compared first by sign (unsigned < signed), then by arity, then by name.
        /// <param name="signatureA">first signature</param>
        /// <param name="signatureB">second signature</param>
        /// <returns>whether a < b</returns>
        bool IsSgnatureLessThan(ulong signatureA, ulong signatureB);

        /// <summary>
        /// Calculate a hash code of a signature.
        /// </summary>
        /// <param name="signature">the target signature</param>
        /// <returns>the hash code of the signature</returns>
        UIntPtr GetSignatureHash(ulong signature);

        #endregion

        #region Symbol Construction

        /// <summary>
        /// Construct a symbol representing a number.
        /// </summary>
        /// <param name="number">the number</param>
        /// <param name="symbol">the resulting symbol</param>
        void CreateNumber(int number, out ulong symbol);

        /// <summary>
        /// Construct a symbol representing <c>#sup</c>.
        /// </summary>
        /// <param name="symbol">the resulting symbol</param>
        void CreateSupremum(out ulong symbol);

        /// <summary>
        /// Construct a symbol representing <c>#inf</c>.
        /// </summary>
        /// <param name="symbol">the resulting symbol</param>
        void CreateInfimum(out ulong symbol);

        /// <summary>
        /// Construct a symbol representing a string.
        /// </summary>
        /// <param name="value">the string</param>
        /// <param name="symbol">the resulting symbol</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool CreateString(string value, out ulong symbol);

        /// <summary>
        /// Construct a symbol representing an id.
        /// </summary>
        /// <remarks>This is just a shortcut for <see cref="CreateFunction(string, ulong[], bool, out ulong)"/> with empty arguments.</remarks>
        /// <param name="name">the name</param>
        /// <param name="positive">whether the symbol has a classical negation sign</param>
        /// <param name="symbol">the resulting symbol</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool CreateId(string name, bool positive, out ulong symbol);

        /// <summary>
        /// Construct a symbol representing a function or tuple.
        /// </summary>
        /// <param name="name">the name of the function</param>
        /// <param name="arguments">the arguments of the function</param>
        /// <param name="positive">whether the symbol has a classical negation sign</param>
        /// <param name="symbol">the resulting symbol</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool CreateFunction(string name, ulong[] arguments, bool positive, out ulong symbol);

        #endregion

        #region Symbol Inspection

        /// <summary>
        /// Get the number of a symbol.
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="number">the resulting number</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetNumber(ulong symbol, out int number);

        /// <summary>
        /// Get the name of a symbol.
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="name">the resulting name</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetName(ulong symbol, out string name);

        /// <summary>
        /// Get the string of a symbol.
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="value">the resulting string</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetString(ulong symbol, out string value);

        /// <summary>
        /// Check if a function is positive (does not have a sign).
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="positive">the result</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool IsPositive(ulong symbol, out bool positive);

        /// <summary>
        /// Check if a function is negative (has a sign).
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="negative">the result</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool IsNegative(ulong symbol, out bool negative);

        /// <summary>
        /// Get the arguments of a symbol.
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="arguments">the resulting arguments</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetArguments(ulong symbol, out ulong[] arguments);

        /// <summary>
        /// Get the type of a symbol.
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <returns>the type of the symbol</returns>
        SymbolType GetType(ulong symbol);

        /// <summary>
        /// Get the string representation of a symbol.
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="value">the resulting string</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool ToString(ulong symbol, out string value);

        #endregion

        #region Symbol Comparasion

        /// <summary>
        /// Check if two symbols are equal.
        /// </summary>
        /// <param name="symbolA">first symbol</param>
        /// <param name="symbolB">second symbol</param>
        /// <returns>whether a == b</returns>
        bool IsEqualTo(ulong symbolA, ulong symbolB);

        /// <summary>
        /// <para>Check if a symbol is less than another symbol.</para>
        /// <para>Symbols are first compared by type. If the types are equal, the values are compared (where strings are compared using strcmp). Functions are first compared by signature and then lexicographically by arguments.</para>
        /// </summary>
        /// <param name="symbolA">first symbol</param>
        /// <param name="symbolB">second symbol</param>
        /// <returns>whether a < b</returns>
        bool IsLessThan(ulong symbolA, ulong symbolB);

        /// <summary>
        /// Calculate a hash code of a symbol.
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <returns>the hash code of the symbol</returns>
        UIntPtr GetHash(ulong symbol);

        #endregion
    }
}
