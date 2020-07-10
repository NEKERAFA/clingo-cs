using ClingoSharp.NativeWrapper.Enums;
using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SolveEventCallback(SolveEventType type, IntPtr event_data, IntPtr data, ref int goon);
}
