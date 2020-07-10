using ClingoSharp.NativeWrapper.Enums;
using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LoggerCallback(WarningCode code, string mesage, IntPtr data);
}