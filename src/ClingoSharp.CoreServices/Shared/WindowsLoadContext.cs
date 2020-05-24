﻿using ClingoSharp.CoreServices.Interfaces;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ClingoSharp.CoreServices.Shared
{
    internal sealed class WindowsLoadContext : ILibraryLoadContext
    {
        private static IntPtr clingoLibPtr;

        #region Win32 Dynamic Loader Library

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string LibFileName);

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeLibrary(IntPtr module);

        #endregion

        #region Library Load Context

        public void LoadClingoLibrary()
        {
            if (clingoLibPtr == IntPtr.Zero)
            {
                string currentPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath);
                var nativeFile = string.Format("{0}{1}runtimes{1}win-x64{1}native{1}clingo.dll", currentPath, Path.DirectorySeparatorChar);
                clingoLibPtr = LoadLibrary(nativeFile);
            }
        }

        public void FreeClingoLibrary()
        {
            if (clingoLibPtr != IntPtr.Zero)
            {
                FreeLibrary(clingoLibPtr);
                clingoLibPtr = IntPtr.Zero;
            }
        }

        #endregion
    }
}