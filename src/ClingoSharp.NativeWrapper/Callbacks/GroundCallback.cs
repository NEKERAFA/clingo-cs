using ClingoSharp.NativeWrapper.Types;
using ClingoSharp.NativeWrapper.Enums;
using System;
using clingo_symbol = System.UInt64;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    internal delegate int clingo_ground_callback(clingo_location[] location, string name, clingo_symbol[] arguments, UIntPtr arguments_size, IntPtr data, clingo_symbol_callback symbol_callback, IntPtr symbol_callback_data);
}
