using ClingoSharp.CoreServices.Enums;
using System;

namespace ClingoSharp.CoreServices.Callbacks
{
    /// <summary>
    /// Callback to intercept warning messages.
    /// </summary>
    /// <param name="code">associated warning code</param>
    /// <param name="message">warning message</param>
    /// <param name="data">user data for callback</param>
    public delegate void LoggerCallback(WarningCode code, string message, IntPtr data);
}
