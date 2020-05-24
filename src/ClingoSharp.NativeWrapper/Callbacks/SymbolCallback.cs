using System;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    internal delegate int clingo_symbol_callback(ulong[] symbols, UIntPtr symbols_size, IntPtr data);
}