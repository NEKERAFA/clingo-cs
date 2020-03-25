using System;
using System.Runtime.InteropServices;
using clingo_symbol = System.UInt64;

namespace ClingoSharp.NativeWrapper.Types
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct clingo_part
    {
        public string name;
        public clingo_symbol[] params_list;
        public UIntPtr size;
    }
}
