﻿using ClingoSharp.NativeWrapper.Enums;
using ClingoSharp.NativeWrapper.Interfaces.Modules;
using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper.Managers
{
    /// <summary>
    /// Data types and functions used throughout all modules and version information
    /// </summary>
    public class ClingoModule : IClingo
    {
        #region Clingo C API Functions

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clingo_version([Out] IntPtr major, [Out] IntPtr minor, [Out] IntPtr revision);

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern ErrorCode clingo_error_code();

        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr clingo_error_message();

        #endregion

        #region Module implementation

        public string GetVersion()
        {
            IntPtr majorRef = Marshal.AllocHGlobal(sizeof(int));
            IntPtr minorRef = Marshal.AllocHGlobal(sizeof(int));
            IntPtr revisionRef = Marshal.AllocHGlobal(sizeof(int));

            clingo_version(majorRef, minorRef, revisionRef);

            int major = Marshal.ReadInt32(majorRef);
            int minor = Marshal.ReadInt32(minorRef);
            int revision = Marshal.ReadInt32(revisionRef);

            Marshal.FreeHGlobal(majorRef);
            Marshal.FreeHGlobal(minorRef);
            Marshal.FreeHGlobal(revisionRef);

            return $"{major}.{minor}.{revision}";
        }

        public ErrorCode GetErrorCode()
        {
            return clingo_error_code();
        }

        public string GetErrorMessage()
        {
            IntPtr stringPtr = clingo_error_message();
            if (stringPtr == IntPtr.Zero) return null;
            return Marshal.PtrToStringAnsi(stringPtr);
        }

        #endregion
    }
}
