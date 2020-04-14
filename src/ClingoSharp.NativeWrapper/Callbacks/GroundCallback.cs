using ClingoSharp.NativeWrapper.Types;
using System;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    internal delegate int clingo_ground_callback(clingo_location[] location, string name, ulong[] arguments, UIntPtr arguments_size, IntPtr data, clingo_symbol_callback symbol_callback, IntPtr symbol_callback_data);
}
