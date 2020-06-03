using ClingoSharp.NativeWrapper.Enums;
using System;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    public delegate void LoggerCallback(WarningCode code, string mesage, IntPtr data);
}