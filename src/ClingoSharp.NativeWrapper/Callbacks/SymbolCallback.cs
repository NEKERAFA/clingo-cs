using ClingoSharp.NativeWrapper.Enums;
using System;
using clingo_symbol = System.UInt64;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    internal delegate int clingo_symbol_callback(clingo_symbol[] symbols, UIntPtr symbols_size, IntPtr data);
}
