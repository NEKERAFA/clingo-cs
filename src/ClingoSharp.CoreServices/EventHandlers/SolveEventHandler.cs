using ClingoSharp.CoreServices.Enums;
using System;

namespace ClingoSharp.CoreServices.EventHandlers
{
    public delegate bool SolveEventHandler(SolveEventType type, IntPtr eventData, out bool goon);
}
