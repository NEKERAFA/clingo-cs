using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Part
    {
        public string name;
        public ulong[] params_list;
        public UIntPtr size;
    }
}
