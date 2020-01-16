using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ClingoSharp.Api
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CClingo
    {
        #region Clingo constants

        internal const string ClingoLib = "clingo.dll";

        #endregion

        #region Clingo library loader

        static IntPtr _clingoLibPtr = IntPtr.Zero;

        internal static void LoadClingoLibrary()
        {
            if (_clingoLibPtr == IntPtr.Zero)
            {
                // Gets assembly path file
                var assemblyPath = new Uri(typeof(CClingo).Assembly.CodeBase).LocalPath;
                // Gets assembly folder
                var assemblyFolder = Path.GetDirectoryName(assemblyPath);
                // Gets arch process
                var arch = Environment.Is64BitProcess ? "lib" : "lib32";

                // Loads clingo library
                _clingoLibPtr = LoadLibrary(Path.Combine(assemblyFolder, arch, ClingoLib));
                if (_clingoLibPtr == IntPtr.Zero)
                {
                    throw new System.ComponentModel.Win32Exception();
                }
            }
        }

        /// <summary>
        /// Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
        /// </summary>
        /// <param name="LibFileName">The name of the module.</param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the module.
        /// If the function fails, the return value is NULL.To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string LibFileName);

        #endregion

        #region Clingo utils

        public class CClingoError : Exception
        {
            public CClingoError(string clingoError) : base(clingoError)
            {
            }
        }

        #endregion

        static CClingo()
        {
            LoadClingoLibrary();
        }

        #region Basic Data Types and Functions

        /// <summary>
        /// Represents a source code location marking its beginnig and end.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class Location
        {
            string BeginFile;
            string EndFile;
            UIntPtr BeginLine;
            UIntPtr EndLine;
            UIntPtr BeginColumn;
            UIntPtr EndColumn;
        }

        private enum Error
        {
            Success = 0,
            RuntimeError = 1,
            LogicError = 2,
            BadAlloc = 3,
            Unknown = 4
        }

        public enum Warning
        {
            OperationUndefined = 0,
            RuntimeError = 1,
            AtomUndefined = 2,
            FileIncluded = 3,
            VariableUnbounded = 4,
            GlobalVariable = 5,
            Other = 6
        }

        /// <summary>
        /// Callback to intercept warning messages.
        /// </summary>
        /// <param name="code">associated warning code</param>
        /// <param name="message">warning message</param>
        /// <param name="data">user data for callback</param>
        public delegate void LoggerCallback(Warning code, string message, IntPtr data);

        [DllImport(ClingoLib, EntryPoint = "clingo_error_code", CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetLastError();

        /// <summary>
        /// Get the last error message set if an API call fails.
        /// </summary>
        /// <returns>error message or NULL</returns>
        [DllImport(ClingoLib, EntryPoint = "clingo_error_message", CallingConvention = CallingConvention.Cdecl)]
        private static extern string GetLastErrorMessage();

        public static void CallProcedureModule(Func<int> procedure)
        {
            if (procedure() == 0)
            {
                Error code = (Error)GetLastError();
                string message = GetLastErrorMessage();

                throw new CClingoError(string.Format("Clingo error: {0} ({1})", code.ToString(), message));
            }
        }

        public static void CallProcedureModule(string name, Func<int> procedure)
        {
            int result = procedure();
            Console.WriteLine(string.Format("{0}: {1}", name, result));
            if (result == 0)
            {
                Error code = (Error)GetLastError();
                string message = GetLastErrorMessage();

                throw new CClingoError(string.Format("Clingo error: {0} ({1})", code.ToString(), message));
            }
        }

        /// <summary>
        /// Obtain the clingo version.
        /// </summary>
        /// <param name="major">major version number</param>
        /// <param name="minor">minor version number</param>
        /// <param name="revision">revision number</param>
        [DllImport(ClingoLib, EntryPoint = "clingo_version", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetVersion([Out] IntPtr major, [Out] IntPtr minor, [Out] IntPtr revision);

        #endregion
    }
}
