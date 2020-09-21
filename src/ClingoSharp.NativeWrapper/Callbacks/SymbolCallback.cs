using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SymbolCallback(ulong[] symbols, UIntPtr symbols_size, IntPtr data);
}