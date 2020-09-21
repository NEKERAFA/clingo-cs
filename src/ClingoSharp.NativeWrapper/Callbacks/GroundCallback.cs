using ClingoSharp.NativeWrapper.Types;
using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GroundCallback(Location[] location, string name, ulong[] arguments, UIntPtr arguments_size, IntPtr data, [MarshalAs(UnmanagedType.FunctionPtr)] SymbolCallback symbol_callback, IntPtr symbol_callback_data);
}
