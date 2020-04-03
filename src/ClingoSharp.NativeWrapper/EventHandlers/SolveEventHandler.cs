using ClingoSharp.NativeWrapper.Enums;
using System;

namespace ClingoSharp.NativeWrapper.EventHandlers
{
    internal delegate int clingo_solve_event_callback(clingo_solve_event_type type, IntPtr event_data, IntPtr data, out bool[] goon);
}
