using System;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    public delegate int SymbolCallback(ulong[] symbols, UIntPtr symbols_size, IntPtr data);
}