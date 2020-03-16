using ClingoSharp.NativeWrapper.Enums;
using System;
using clingo_symbol = System.UInt64;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    /// <summary>
    /// Callback function to inject symbols
    /// </summary>
    /// <param name="symbols">array of symbols</param>
    /// <param name="symbols_size">size of the symbol array</param>
    /// <param name="data">user data of the callback</param>
    /// <returns>whether the call was successful; might set one of the following error codes: <see cref="clingo_error.clingo_error_bad_alloc"/></returns>
    internal delegate int clingo_symbol_callback(clingo_symbol[] symbols, UIntPtr symbols_size, IntPtr data);
}
