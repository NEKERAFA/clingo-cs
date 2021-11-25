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
            int[] major_c = new int[1];
            int[] minor_c = new int[1];
            int[] revision_c = new int[1];

            clingo_version(major_c, minor_c, revision_c);

            return (major_c[0], minor_c[0], revision_c[0]);
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

                throw clingo_error_code() switch
                {
                    clingo_error_t.clingo_error_runtime => new RuntimeException(message),
                    clingo_error_t.clingo_error_logic => new InvalidOperationException(message),
                    clingo_error_t.clingo_error_bad_alloc => new OutOfMemoryException(message),
                    clingo_error_t.clingo_error_unknown => new UnknownException(message),
                    clingo_error_t.clingo_error_success => new Exception(message),
                    _ => new Exception(message),
                };
            }
        }

        internal static void HandleWarning(clingo_warning_t code, string message)
        {
            throw code switch
            {
                clingo_warning_t.clingo_warning_operation_undefined => new InvalidOperationException(message),
                clingo_warning_t.clingo_warning_runtime_error => new RuntimeException(message),
                clingo_warning_t.clingo_warning_atom_undefined => new AtomUndefinedException(message),
                clingo_warning_t.clingo_warning_file_included => new CircularFileIncludedException(message),
                clingo_warning_t.clingo_warning_variable_unbounded => new VariableUnboundedException(message),
                clingo_warning_t.clingo_warning_global_variable => new GlobalVariableException(message),
                clingo_warning_t.clingo_warning_other => new UnknownException(message),
                _ => new Exception(message),
            };
        }
    }
}