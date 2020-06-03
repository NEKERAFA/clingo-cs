using ClingoSharp.NativeWrapper.Types;
using System;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    public delegate int GroundCallback(Location[] location, string name, ulong[] arguments, UIntPtr arguments_size, IntPtr data, SymbolCallback symbol_callback, IntPtr symbol_callback_data);
}
