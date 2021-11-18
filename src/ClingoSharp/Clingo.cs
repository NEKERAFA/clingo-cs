using System;
using System.Runtime.InteropServices;
using ClingoSharp.Exceptions;
using static ClingoSharp.Native.Clingo_c;

namespace ClingoSharp
{
    /// <summary>
    /// Core functionality used throught the clingo package.
    /// </summary>
    public sealed class Clingo
    {
        /// <summary>
        /// Clingo's version as a tuple <c>(major, minor, revision)</c>
        /// </summary>
        /// <returns></returns>
        public static (int, int, int) Version()
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

            return (major, minor, revision);
        }

        /// <summary>
        /// Helper to simplify calling clingo functions and checks if there is some error throw in the execution
        /// </summary>
        /// <param name="success">The result of the clingo function</param>
        /// <exception cref="RuntimeException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="UnknownException"></exception>
        /// <exception cref="Exception"></exception>
        internal static void HandleError(bool success)
        {
            if (!success)
            {
                string message = clingo_error_message();
                if (message == null) { message = ""; }

                switch (clingo_error_code())
                {
                    case (int)clingo_error_e.clingo_error_runtime:
                        throw new RuntimeException(message);
                    case (int)clingo_error_e.clingo_error_logic:
                        throw new InvalidOperationException(message);
                    case (int)clingo_error_e.clingo_error_bad_alloc:
                        throw new OutOfMemoryException(message);
                    case (int)clingo_error_e.clingo_error_unknown:
                        throw new UnknownException(message);
                    case (int)clingo_error_e.clingo_error_success:
                        throw new Exception(message);
                    default:
                        throw new Exception(message);
                }
            }
        }

        internal static void HandleWarning(clingo_warning_t code, string message)
        {
            switch (code)
            {
                case (int)clingo_warning_e.clingo_warning_operation_undefined:
                    throw new InvalidOperationException(message);
                case (int)clingo_warning_e.clingo_warning_runtime_error:
                    throw new RuntimeException(message);
                case (int)clingo_warning_e.clingo_warning_atom_undefined:
                    throw new AtomUndefinedException(message);
                case (int)clingo_warning_e.clingo_warning_file_included:
                    throw new CircularFileIncludedException(message);
                case (int)clingo_warning_e.clingo_warning_variable_unbounded:
                    throw new VariableUnboundedException(message);
                case (int)clingo_warning_e.clingo_warning_global_variable:
                    throw new GlobalVariableException(message);
                case (int)clingo_warning_e.clingo_warning_other:
                    throw new UnknownException(message);
                default:
                    throw new Exception(message);
            }
        }
    }
}