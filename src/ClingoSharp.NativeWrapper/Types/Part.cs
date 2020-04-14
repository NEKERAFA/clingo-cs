using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Types
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct clingo_part
    {
        public string name;
        public ulong[] params_list;
        public UIntPtr size;
    }
}
