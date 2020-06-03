using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Location
    {
        public string begin_file;
        public string end_file;
        public UIntPtr begin_line;
        public UIntPtr end_line;
        public UIntPtr begin_column;
        public UIntPtr end_column;
    }
}
