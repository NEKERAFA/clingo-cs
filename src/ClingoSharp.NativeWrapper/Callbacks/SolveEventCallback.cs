using ClingoSharp.NativeWrapper.Enums;
using System;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    public delegate int SolveEventCallback(SolveEventType type, IntPtr event_data, IntPtr data, out bool[] goon);
}
