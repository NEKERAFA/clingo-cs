using ClingoSharp.NativeWrapper.Enums;
using System;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    internal delegate void clingo_logger(clingo_warning code, string mesage, IntPtr data);
}