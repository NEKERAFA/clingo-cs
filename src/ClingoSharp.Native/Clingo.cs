using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.Native
{
    using clingo_literal_t = Int32;

    using clingo_atom_t = Int32;

    using clingo_id_t = UInt32;

    using clingo_weight_t = Int32;

    using clingo_error_t = Int32;

    using clingo_warning_t = Int32;

    using clingo_truth_value_t = Int32;

    using clingo_signature_t = UInt64;

    /// <summary>
    /// A Literal with an associated weight.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct clingo_weighted_literal_t
    {
        public clingo_literal_t literal;
        public clingo_weight_t weight;
    }

    /// <summary>
    /// Represents a source code location marking its beginnig and end.
    /// <para>Not all locations refer to physical files.
    /// By convention, such locations use a name put in angular brackets as filename.
    /// The string members of a location object are internalized and valid for the duration of the process.
    /// </para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct clingo_location_t
    {
        /// <summary>the file where the location begins</summary>
        public string begin_file;
        /// <summary>the file where the location ends</summary>
        public string end_file;
        /// <summary>the line where the location begins</summary>
        public UIntPtr begin_line;
        /// <summary>the line where the location ends</summary>
        public UIntPtr end_line;
        /// <summary>the column where the location begins</summary>
        public UIntPtr begin_column;
        /// <summary>the column where the location ends</summary>
        public UIntPtr end_column;
    }

    /// <summary>
    /// Enumeration of error codes.
    /// <para>
    /// Errors can only be recovered from if explicitly mentioned; most
    /// functions do not provide strong exception guarantees. This means that in
    /// case of errors associated objects cannot be used further. If such an
    /// object has a free function, this function can and should still be called.
    /// </para>
    /// </summary>
    public enum clingo_error_e
    {
        /// <summary>successful API calls</summary>
        clingo_error_success = 0,
        /// <summary>errors only detectable at runtime like invalid input</summary>
        clingo_error_runtime = 1,
        /// <summary>wrong usage of the clingo API</summary>
        clingo_error_logic = 2,
        /// <summary>memory could not be allocated</summary>
        clingo_error_bad_alloc = 3,
        /// <summary>errors unrelated to clingo</summary>
        clingo_error_unknown = 4
    }

    /// <summary>
    /// Enumeration of warning codes.
    /// </summary>
    public enum clingo_warning_e
    {
        /// <summary>undefined arithmetic operation or weight of aggregate</summary>
        clingo_warning_operation_undefined = 0,
        /// <summary>to report multiple errors; a corresponding runtime error is raised later</summary>
        clingo_warning_runtime_error = 1,
        /// <summary>undefined atom in program</summary>
        clingo_warning_atom_undefined = 2,
        /// <summary>same file included multiple times</summary>
        clingo_warning_file_included = 3,
        /// <summary>CSP variable with unbounded domain</summary>
        clingo_warning_variable_unbounded = 4,
        /// <summary>global variable in tuple of aggregate element</summary>
        clingo_warning_global_variable = 5,
        /// <summary>other kinds of warning</summary>
        clingo_warning_other = 6
    }

    /// <summary>
    /// Represents three-valued truth values.
    /// </summary>
    public enum clingo_truth_value_e
    {
        /// <summary>no truth value</summary>
        clingo_truth_value_free = 0,
        /// <summary>true</summary>
        clingo_truth_value_true = 1,
        /// <summary>false</summary>
        clingo_truth_value_false = 2
    }

    public static class Clingo
    {
        private const string libName = "clingo";

        /// <summary>
        /// Major version number.
        /// </summary>
        public const int CLINGO_VERSION_MAJOR = 5;

        /// <summary>
        /// Minor version number.
        /// </summary>
        public const int CLINGO_VERSION_MINOR = 5;

        /// <summary>
        /// Revision number.
        /// </summary>
        public const int CLINGO_VERSION_REVISION = 0;

        /// <summary>
        /// String representation of version.
        /// </summary>
        public const string CLINGO_VERSION = "5.5.0";

        /// <summary>
        /// Convert error code into string.
        /// </summary>
        /// <param name="code">Corresponding type of <c>clingo_error_e</c>.</param>
        /// <returns>String representation of error.</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string clingo_error_string(clingo_error_t code);

        /// <summary>
        /// Get the last error code set by a clingo API call.
        /// <para>Each thread has its own local error code.</para>
        /// </summary>
        /// <returns>error code</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern clingo_error_t clingo_error_code();

        /// <summary>
        /// Get the last error code set by a clingo API call.
        /// <para>Each thread has its own local error message.</para>
        /// </summary>
        /// <returns>error message or NULL</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string clingo_error_message();

        /// <summary>
        /// Set a custom error code and message in the active thread.
        /// </summary>
        /// <param name="code">code the error code</param>
        /// <param name="message">message the error message</param>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void clingo_set_error(clingo_error_t code, string message);

        /// <summary>
        /// Convert warning code into string.
        /// </summary>
        /// <param name="code">code warning</param>
        /// <returns>String representation of warning.</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string clingo_warning_string(clingo_warning_t code);

        /// <summary>
        /// Callback to intercept warning messages.
        /// </summary>
        /// <param name="code">associated warning code</param>
        /// <param name="message">warning message</param>
        /// <param name="data">user data for callback</param>
        public delegate void clingo_logger_t(clingo_warning_t code, string message, IntPtr data);

        /// <summary>
        /// Obtain the clingo version.
        /// </summary>
        /// <param name="major">major version number</param>
        /// <param name="minor">minor version number</param>
        /// <param name="revision">revision number</param>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void clingo_version([Out] IntPtr major, [Out] IntPtr minor, [Out] IntPtr revision);

        /// <summary>
        /// Create a new signature
        /// </summary>
        /// <param name="name">name of the signature</param>
        /// <param name="arity">arity of the signature</param>
        /// <param name="positive">false if the signature has a classical negation sign</param>
        /// <param name="signature">the resulting signature</param>
        /// <returns>whether the call was successful; might set one of the following error codes:
        /// <list>
        ///     <item cref="clingo_error_e.clingo_error_bad_alloc">clingo_error_bad_alloc</item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_signature_create(string name, int arity, [MarshalAs(UnmanagedType.Bool)] bool positive, [Out] IntPtr signature);

        /// <summary>
        /// Get the name of a signature.
        /// <para>The string is internalized and valid fro the durationb of the process.</para>
        /// </summary>
        /// 
        /// <param name="signature">the target signature</param>
        /// <returns>the name of the signature</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string clingo_signature_name(clingo_signature_t signature);
    }
}
