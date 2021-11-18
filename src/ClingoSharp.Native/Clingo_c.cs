using System;
using System.Runtime.InteropServices;

namespace ClingoSharp.Native
{
    public static class Clingo_c
    {
        private const string libName = "clingo";

        #region Basic types and error/warning handling

        #region Basic Types

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
        public const int CLINGO_VERSION_REVISION = 1;

        /// <summary>
        /// String representation of version.
        /// </summary>
        public const string CLINGO_VERSION = "5.5.1";

        /// <summary>
        /// Signed integer type used for aspif and solver literals.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public readonly struct clingo_literal_t
        {
            private readonly int value;

            public clingo_literal_t(int value)
            {
                this.value = value;
            }

            public static implicit operator int(clingo_literal_t type) => type.value;
            public static implicit operator clingo_literal_t(int value) => new clingo_literal_t(value);
        }

        /// <summary>
        /// Unsigned integer type used for aspif atoms.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public readonly struct clingo_atom_t
        {
            private readonly uint value;

            public clingo_atom_t(uint value)
            {
                this.value = value;
            }

            public static implicit operator uint(clingo_atom_t type) => type.value;
            public static implicit operator clingo_atom_t(uint value) => new clingo_atom_t(value);
        }

        /// <summary>
        /// Unsigned integer type used in various places.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public readonly struct clingo_id_t
        {
            private readonly uint value;

            public clingo_id_t(uint value)
            {
                this.value = value;
            }

            public static implicit operator uint(clingo_id_t type) => type.value;
            public static implicit operator clingo_id_t(uint value) => new clingo_id_t(value);
        }

        /// <summary>
        /// Signed integer type for weights in sum aggregates and minimize constraints.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public readonly struct clingo_weight_t
        {
            private readonly int value;

            public clingo_weight_t(int value)
            {
                this.value = value;
            }

            public static implicit operator int(clingo_weight_t type) => type.value;
            public static implicit operator clingo_weight_t(int value) => new clingo_weight_t(value);
        }

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
        /// Enumeration of error codes.
        /// 
        /// <para>
        /// Note: Errors can only be recovered from if explicitly mentioned; most
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
        /// Corresponding type to <see cref="clingo_error_e" />.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public readonly struct clingo_error_t
        {
            private readonly int value;

            public clingo_error_t(int value)
            {
                this.value = value;
            }

            public static implicit operator int(clingo_error_t type) => type.value;
            public static implicit operator clingo_error_t(int value) => new clingo_error_t(value);
        }

        /// <summary>
        /// Convert error code into string.
        /// </summary>
        /// <param name="code">Corresponding type of <see cref="clingo_error_e" />.</param>
        /// <returns>string representation of error.</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string clingo_error_string(clingo_error_t code);

        /// <summary>
        /// Get the last error code set by a clingo API call.
        /// 
        /// <para>Note: Each thread has its own local error code.</para>
        /// </summary>
        /// <returns>error code</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        public static extern clingo_error_t clingo_error_code();

        /// <summary>
        /// Get the last error message set if an API call fails.
        /// 
        /// <para>Note: Each thread has its own local error message.</para>
        /// </summary>
        /// <returns>error message or <c>null</c></returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string clingo_error_message();

        /// <summary>
        /// Set a custom error code and message in the active thread.
        /// </summary>
        /// <param name="code">the error code</param>
        /// <param name="message">the error message</param>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void clingo_set_error(clingo_error_t code, string message);

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
        /// Corresponding to <see cref="Clingo.clingo_warning_e" />
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public readonly struct clingo_warning_t
        {
            private readonly int value;

            public clingo_warning_t(int value)
            {
                this.value = value;
            }

            public static implicit operator int(clingo_warning_t type) => type.value;
            public static implicit operator clingo_warning_t(int value) => new clingo_warning_t(value);
        }

        /// <summary>
        /// Convert warning code into string.
        /// </summary>
        /// <param name="code">code warning</param>
        /// <returns>String representation of warning.</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string clingo_warning_string(clingo_warning_t code);

        /// <summary>
        /// Callback to intercept warning messages.
        /// </summary>
        /// <param name="code">associated warning code</param>
        /// <param name="message">warning message</param>
        /// <param name="data">user data for callback</param>
        public delegate void clingo_logger_t(clingo_warning_t code, [MarshalAs(UnmanagedType.LPStr)] string message, IntPtr data);

        /// <summary>
        /// Obtain the clingo version.
        /// </summary>
        /// <param name="major">major version number</param>
        /// <param name="minor">minor version number</param>
        /// <param name="revision">revision number</param>
        /// <see />
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void clingo_version([Out] IntPtr major, [Out] IntPtr minor, [Out] IntPtr revision);

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

        /// <summary>
        /// Corresponding type to <see cref="Clingo.clingo_truth_value_e" />
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public readonly struct clingo_truth_value_t
        {
            private readonly int value;

            public clingo_truth_value_t(int value)
            {
                this.value = value;
            }

            public static implicit operator int(clingo_truth_value_t type) => type.value;
            public static implicit operator clingo_truth_value_t(int value) => new clingo_truth_value_t(value);
        }

        /// <summary>
        /// Represents a source code location marking its beginnig and end.
        /// 
        /// <para>Note: Not all locations refer to physical files.
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

        #endregion

        #endregion

        #region Signature and symbols

        #region Symbols

        /// <summary>
        /// Represents a predicate signature.
        /// 
        /// <para>
        /// Signatures have a name and an arity, and can be positive or negative (to represent classical negation).
        /// </para>
        /// </summary>

        [StructLayout(LayoutKind.Sequential)]
        public readonly struct clingo_signature_t
        {
            private readonly ulong value;

            public clingo_signature_t(ulong value)
            {
                this.value = value;
            }

            public static implicit operator ulong(clingo_signature_t type) => type.value;
            public static implicit operator clingo_signature_t(ulong value) => new clingo_signature_t(value);
        }

        #endregion

        #region Signature functions

        /// <summary>
        /// Create a new signature
        /// </summary>
        /// <param name="name">name of the signature</param>
        /// <param name="arity">arity of the signature</param>
        /// <param name="positive">false if the signature has a classical negation sign</param>
        /// <param name="signature">the resulting signature</param>
        /// <returns>whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item><see cref="clingo_error_e.clingo_error_bad_alloc" /></item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_signature_create(string name, int arity, [MarshalAs(UnmanagedType.Bool)] bool positive, [Out] IntPtr signature);

        /// <summary>
        /// Get the name of a signature.
        /// 
        /// <para>Note: The string is internalized and valid fro the durationb of the process.</para>
        /// </summary>
        /// <param name="signature">the target signature</param>
        /// <returns>the name of the signature</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string clingo_signature_name(clingo_signature_t signature);

        /// <summary>
        /// Get the arity of a signature.
        /// </summary>
        /// <param name="signature">the target signature</param>
        /// <returns>the arity of the signature</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int clingo_signature_arity(clingo_signature_t signature);

        /// <summary>
        /// Whether the signature is positive (is not classically negated).
        /// </summary>
        /// <param name="signature">the target signature</param>
        /// <returns>whether the signature has no sign</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_signature_is_positive(clingo_signature_t signature);

        /// <summary>
        /// Whether the signature is negative (is classically negated).
        /// </summary>
        /// <param name="signature">the target signature</param>
        /// <returns>whether the signature has a sign</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_signature_is_negative(clingo_signature_t signature);

        /// <summary>
        /// Check if two signatures are equal.
        /// </summary>
        /// <param name="a">first signature</param>
        /// <param name="b">second signature</param>
        /// <returns>whether a == b</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_signature_is_equal_to(clingo_signature_t a, clingo_signature_t b);

        /// <summary>
        /// Check if a signature is less than another signature.
        /// 
        /// <para>
        /// Signatures are compared first by sign (unsigned &lt; signed), then by arity,
        /// then by name.
        /// </para>
        /// </summary>
        /// <param name="a">a first signature</param>
        /// <param name="b">b second signature</param>
        /// <returns>whether a &lt; b</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_signature_is_less_than(clingo_signature_t a, clingo_signature_t b);

        /// <summary>
        /// Calculate a hash code of a signature.
        /// </summary>
        /// <param name="signature">the target signature</param>
        /// <returns>the hash code of the signature</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr clingo_signature_hash(clingo_signature_t signature);

        #endregion

        /// <summary>
        /// Enumeration of available symbol types.
        /// </summary>
        public enum clingo_symbol_type_e
        {
            /// <summary>the <c>#inf</c> symbol</summary>
            clingo_symbol_type_infimum = 0,
            /// <summary>a numeric symbol, e.g., `1`</summary>
            clingo_symbol_type_number = 1,
            /// <summary>a string symbol, e.g., `"a"`</summary>
            clingo_symbol_type_string = 4,
            /// <summary>a numeric symbol, e.g., `c`, `(1, "a")`, or `f(1,"a")`</summary>
            clingo_symbol_type_function = 5,
            /// <summary> the <c>#sup</c> symbol</summary>
            clingo_symbol_type_supremum = 7
        };

        /// <summary>
        /// Corresponding type to <see cref="Clingo.clingo_symbol_type_e" />
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public readonly struct clingo_symbol_type_t
        {
            private readonly int value;

            public clingo_symbol_type_t(int value)
            {
                this.value = value;
            }

            public static implicit operator int(clingo_symbol_type_t type) => type.value;
            public static implicit operator clingo_symbol_type_t(int value) => new clingo_symbol_type_t(value);
        };

        /// <summary>
        /// Represents a symbol.
        /// 
        /// <para>
        /// This includes numbers, strings, functions (including constants when
        /// arguments are empty and tuples when the name is empty), <c>#inf</c> and <c>#sup</c>.
        /// </para>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct clingo_symbol_t
        {
            internal ulong value;

            public clingo_symbol_t(ulong value)
            {
                this.value = value;
            }

            public static implicit operator ulong(clingo_symbol_t type) => type.value;
            public static implicit operator clingo_symbol_t(ulong value) => new clingo_symbol_t(value);
        };

        #region Symbol Construction Functions

        /// <summary>
        /// Construct a symbol representing a number.
        /// </summary>
        /// <param name="number">the number</param>
        /// <param name="symbol">the resulting symbol</param>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void clingo_symbol_create_number(int number, [Out] IntPtr symbol);

        /// <summary>
        /// Construct a symbol representing <c>#sup</c>.
        /// </summary>
        /// <param name="symbol">the resulting symbol</param>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void clingo_symbol_create_supremum([Out] IntPtr symbol);

        /// <summary>
        /// Construct a symbol representing <c>#inf</c>.
        /// </summary>
        /// <param name="symbol">the resulting symbol</param>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void clingo_symbol_create_infimum([Out] IntPtr symbol);

        /// <summary>
        /// Construct a symbol representing a number.
        /// </summary>
        /// <param name="str">the string</param>
        /// <param name="symbol">the resulting symbol</param>
        /// <returns>whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item><see cref="clingo_error_e.clingo_error_bad_alloc" /></item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_create_string(string str, [Out] IntPtr symbol);

        /// <summary>
        /// Construct a symbol representing a number.
        /// </summary>
        /// <param name="name">the name</param>
        /// <param name="positive">whether the symbol has a classical negation sign</param>
        /// <param name="symbol">the resulting symbol</param>
        /// <returns>whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item><see cref="clingo_error_e.clingo_error_bad_alloc" /></item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_create_id(string name, [MarshalAs(UnmanagedType.Bool)] bool positive, [Out] IntPtr symbol);

        /// <summary>
        /// Construct a symbol representing a function or tuple.
        /// <para>
        /// Note: To create tuples, the empty string has to be used as name.
        /// </para>
        /// </summary>
        /// <param name="name">the name of the function</param>
        /// <param name="arguments">the arguments of the function</param>
        /// <param name="arguments_size">the number of arguments</param>
        /// <param name="positive">the symbol has a classical negation sign</param>
        /// <param name="symbol">the resulting symbol</param>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_create_function(string name, IntPtr arguments, UIntPtr arguments_size, bool positive, [Out] IntPtr symbol);

        #endregion

        #region Symbol Inspection Functions

        /// <summary>
        /// Get the number of a symbol.
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="number">the resulting number</param>
        /// <returns>whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item>
        ///         <term><see cref="clingo_error_e.clingo_error_runtime" /></term>
        ///         <description>if symbol is not of type <see cref="clingo_symbol_type_e.clingo_symbol_type_number" /></description>
        ///     </item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_number(clingo_symbol_t symbol, [Out] IntPtr number);

        /// <summary>
        /// Get the name of a symbol.
        /// 
        /// <para>
        /// Note: The string is internalized and valid for the duration of the process.
        /// </para>
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="name">the resulting name</param>
        /// <returns>whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item>
        ///         <term><see cref="clingo_error_e.clingo_error_runtime" /></term>
        ///         <description>if symbol is not of type <see cref="clingo_symbol_type_e.clingo_symbol_type_function" /></description>
        ///     </item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_name(clingo_symbol_t symbol, [Out] IntPtr name);

        /// <summary>
        /// Get the string of a symbol.
        /// 
        /// <para>
        /// Note: The string is internalized and valid for the duration of the process.
        /// </para>
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="str">the resulting string</param>
        /// <returns>whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item>
        ///         <term><see cref="clingo_error_e.clingo_error_runtime" /></term>
        ///         <description>if symbol is not of type <see cref="clingo_symbol_type_e.clingo_symbol_type_string" /></description>
        ///     </item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_string(clingo_symbol_t symbol, [Out] IntPtr str);

        /// <summary>
        /// Check if a function is positive (does not have a sign).
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="positive">the result</param>
        /// <returns>whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item>
        ///         <term><see cref="clingo_error_e.clingo_error_runtime" /></term>
        ///         <description>if symbol is not of type <see cref="clingo_symbol_type_e.clingo_symbol_type_function" /></description>
        ///     </item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_is_positive(clingo_symbol_t symbol, [Out] IntPtr positive);

        /// <summary>
        /// Check if a function is negative (has a sign).
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="negative">the result</param>
        /// <returns>whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item>
        ///         <term><see cref="clingo_error_e.clingo_error_runtime" /></term>
        ///         <description>if symbol is not of type <see cref="clingo_symbol_type_e.clingo_symbol_type_function" /></description>
        ///     </item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_is_negative(clingo_symbol_t symbol, [Out] IntPtr negative);

        /// <summary>
        /// Get the arguments of a symbol.
        /// </summary>
        /// <param name="symbol">the resulting arguments</param>
        /// <param name="arguments">the resulting string</param>
        /// <param name="arguments_size">the number of arguments</param>
        /// <returns>whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item>
        ///         <term><see cref="clingo_error_e.clingo_error_runtime" /></term>
        ///         <description>if symbol is not of type <see cref="clingo_symbol_type_e.clingo_symbol_type_string" /></description>
        ///     </item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_arguments(clingo_symbol_t symbol, [Out] IntPtr arguments, [Out] IntPtr arguments_size);

        /// <summary>
        /// Get the type of a symbol.
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <returns>the type of the symbol
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        public static extern clingo_symbol_type_t clingo_symbol_type(clingo_symbol_t symbol);

        /// <summary>
        /// Get the size of the string representation of a symbol (including the terminating 0).
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="str">the resulting size</param>
        /// <returns>whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item><see cref="clingo_error_e.clingo_error_bad_alloc" /></item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_to_string_size(clingo_symbol_t symbol, [Out] IntPtr size);

        /// <summary>
        /// Get the string representation of a symbol.
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <param name="str">the resulting string</param>
        /// <param name="size">the size of the string</param>
        /// <returns>whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item><see cref="clingo_error_e.clingo_error_bad_alloc" /></item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_to_string(clingo_symbol_t symbol, [Out] IntPtr str, UIntPtr size);

        #endregion

        #region Symbol Comparison Functions

        /// <summary>
        /// Check if two symbols are equal.
        /// </summary>
        /// <param name="a">first symbol</param>
        /// <param name="b">second symbol</param>
        /// <returns>whether a == b</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_is_equal_to(clingo_symbol_t a, clingo_symbol_t b);

        /// <summary>
        /// Check if a symbol is less than another symbol.
        ///
        ///<para>
        /// Symbols are first compared by type.  If the types are equal, the values are
        /// compared (where strings are compared using strcmp).  Functions are first
        /// compared by signature and then lexicographically by arguments.
        ///</para>
        /// </summary>
        /// <param name="a">first symbol</param>
        /// <param name="b">second symbol</param>
        /// <returns>whether a &lt; b</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_symbol_is_less_than(clingo_symbol_t a, clingo_symbol_t b);

        /// <summary>
        /// Calculate a hash code of a symbol.
        /// </summary>
        /// <param name="symbol">the target symbol</param>
        /// <returns>the hash code of the symbol</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr clingo_symbol_hash(clingo_symbol_t symbol);

        #endregion

        /// <summary>
        /// Internalize a string.
        /// 
        /// <para>
        /// This functions takes a string as input and returns an equal unique string
        /// that is (at the moment) not freed until the program is closed.
        /// </para>
        /// </summary>
        /// <param name="str">the string to internalize</param>
        /// <param name="result"></param>
        /// <returns>
        /// whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item><see cref="clingo_error_e.clingo_error_bad_alloc" /></item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_add_string(string str, [Out] IntPtr result);

        /// <summary>
        /// Parse a term in string form.
        /// 
        /// <para>
        /// The result of this function is a symbol. The input term can contain
        /// unevaluated functions, which are evaluated during parsing.
        /// </para>
        /// </summary>
        /// <param name="str">the string to parse</param>
        /// <param name="logger">logger to report warnings during parsing</param>
        /// <param name="logger_data">user data for the logger</param>
        /// <param name="message_limit">maximum number of times to call the logger</param>
        /// <param name="symbol">the resulting symbol</param>
        /// <returns>
        /// whether the call was successful; might set one of the following error codes:
        /// <list type="bullet">
        ///     <item><see cref="clingo_error_e.clingo_error_bad_alloc" /></item>
        ///     <item>
        ///         <term><see cref="clingo_error_e.clingo_error_runtime" /></term>
        ///         <description>if parsing fails</description>
        ///     </item>
        /// </list>
        /// </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool clingo_parse_term(string str, clingo_logger_t logger, IntPtr logger_data, uint message_limit, [Out] IntPtr symbol);

        #endregion

        /// <summary>
        /// Enumeration of different external statements.
        /// </summary>
        public enum clingo_external_type_e
        {
            /// <summary>
            /// allow an external to be assigned freely
            /// </summary>
            clingo_external_type_free = 0,
            /// <summary>
            /// assign an external to true
            /// </summary>
            clingo_external_type_true = 1,
            /// <summary>
            /// assign an external to false
            /// </summary>
            clingo_external_type_false = 2,
            /// <summary>
            /// no longer treat an atom as external
            /// </summary>
            clingo_external_type_release = 3,
        };
    }
}
