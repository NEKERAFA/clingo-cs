using ClingoSharp.CoreServices.Interfaces;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ClingoSharp.CoreServices.Shared
{
    internal sealed class PoxisLoadContext : ILibraryLoadContext
    {
        private const int RTLD_NOW = 2;

        private static IntPtr clingoLibPtr;

        #region POXIS dynamic library fuctions

#pragma warning disable IDE1006

        [DllImport("libdl",  CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern IntPtr dlopen(string fileName, int flags);

        [DllImport("libdl", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr dlerror();

        [DllImport("libdl", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        private static extern int dlclose(IntPtr handle);

#pragma warning restore IDE1006

        #endregion

        #region Library Load Context

        public void LoadClingoLibrary(string currentPath)
        {
            if (clingoLibPtr == IntPtr.Zero)
            {
                bool isOsx = Environment.OSVersion.Platform == PlatformID.MacOSX;
                string ext = isOsx ? "so" : "dylib";
                string arch = isOsx ? "osx" : "linux";

                var nativeFile = string.Format("{0}{1}runtimes{1}{2}-64{1}native{1}clingo.{3}", currentPath, Path.DirectorySeparatorChar, arch, ext);

                clingoLibPtr = dlopen(nativeFile, RTLD_NOW);
                if (clingoLibPtr == IntPtr.Zero)
                {
                    string errMsg = Marshal.PtrToStringAnsi(dlerror());
                    throw new Exception(string.Format("dlopen failed: {0} : {1}", nativeFile, errMsg));
                }
            }
        }

        public void FreeClingoLibrary()
        {
            if (clingoLibPtr != IntPtr.Zero)
            {
                if (dlclose(clingoLibPtr) != 0)
                {
                    string errMsg = Marshal.PtrToStringAnsi(dlerror());
                    throw new Exception(string.Format("dlclose failed: {0}", errMsg));
                }

                clingoLibPtr = IntPtr.Zero;
            }
        }

        #endregion
    }
}
