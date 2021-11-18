using System;
using System.Runtime.InteropServices;

namespace ClingoSharp
{
    public static class Utils
    {
        /// <summary>
        /// Reads a processor native-sized unsigned integer from unmanaged memory
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        public static UIntPtr ReadUIntPtr(IntPtr ptr)
        {
            /// This is a workarround to read UIntPtr inside of a IntPtr
            /// https://stackoverflow.com/a/3762219
            unchecked
            {
                return new UIntPtr(Environment.Is64BitProcess ? (ulong)(long)Marshal.ReadIntPtr(ptr) : (uint)(int)Marshal.ReadIntPtr(ptr));
            }
        }
    }
}