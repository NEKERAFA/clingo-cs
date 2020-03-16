using ClingoSharp.CoreServices.Enums;
using System;

namespace ClingoSharp.CoreServices.Callbacks
{
    public delegate void LoggerCallback(WarningCode code, string message, IntPtr data);
}
