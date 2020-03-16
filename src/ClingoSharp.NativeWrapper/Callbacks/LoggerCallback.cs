using ClingoSharp.NativeWrapper.Enums;
using System;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    /// <summary>
    /// Callback to intercept warning messages.
    /// </summary>
    /// <param name="code">associated warning code</param>
    /// <param name="mesage">warning message</param>
    /// <param name="data">user data for callback</param>
    internal delegate void clingo_logger(clingo_warning code, string mesage, IntPtr data);
}
