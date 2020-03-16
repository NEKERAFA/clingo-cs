using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.Interfaces.Modules;
using ClingoSharp.NativeWrapper.Enums;
using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.NativeWrapper
{
    /// <summary>
    /// Data types and functions used throughout all modules and version information
    /// </summary>
    public class ClingoModuleImpl : IClingoModule
    {
        #region Clingo C API Functions

        /// <summary>
        /// Obtain the clingo version.
        /// </summary>
        /// <param name="major">major version number</param>
        /// <param name="minor">minor version number</param>
        /// <param name="revision">revision number</param>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clingo_version([Out] IntPtr major, [Out] IntPtr minor, [Out] IntPtr revision);

        /// <summary>
        /// Get the last error code set by a clingo API call.
        /// 
        /// Note: Each thread has its own local error code.
        /// </summary>
        /// <returns>error code</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern clingo_error clingo_error_code();

        /// <summary>
        /// Get the last error message set if an API call fails.
        /// 
        /// Note: Each thread has its own local error code.
        /// </summary>
        /// <returns>error message or null</returns>
        [DllImport(Constants.ClingoLib, CallingConvention = CallingConvention.Cdecl)]
        private static extern string clingo_error_message();

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
            return (ErrorCode)clingo_error_code();
        }

        public string GetErrorMessage()
        {
            return clingo_error_message();
        }

        #endregion
    }
}
