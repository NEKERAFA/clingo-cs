using System;
using System.Runtime.InteropServices;
using static Clingo_c.Clingo_c;

namespace Clingo_cs
{
    public static class Utils
    {
        /// <summary>
        /// Reads a size_t from unmanaged memory
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        public static size_t Read(IntPtr ptr)
        {
            if (Environment.Is64BitProcess)
            {
                return (ulong)Marshal.ReadInt64(ptr);
            }

            return (uint)Marshal.ReadInt32(ptr);
        }
    }
}
