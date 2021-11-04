using System;
using System.Runtime.InteropServices;

namespace Clingo
{
    using clingo_literal_t = Int32;

    using clingo_atom_t = Int32;

    using clingo_id_t = UInt32;

    using clingo_weight_t = Int32;

    using clingo_error_t = Int32;

    using clingo_warning_t = Int32;

    using clingo_truth_value_t = Int32;

    using clingo_signature_t = UInt64;

    [StructLayout(LayoutKind.Sequential)]
    public struct clingo_weighted_literal_t
    {
        public clingo_literal_t literal;
        public clingo_weight_t weight;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct clingo_location_t
    {
        public string begin_file;
        public string end_file;
        public UIntPtr begin_line;
        public UIntPtr end_line;
        public UIntPtr begin_column;
        public UIntPtr end_column;
    }

    enum clingo_error_e
    {
        clingo_error_success = 0,
        clingo_error_runtime = 1,
        clingo_error_logic = 2,
        clingo_error_bad_alloc = 3,
        clingo_error_unknown = 4
    }

    enum clingo_warning_e
    {
        clingo_warning_operation_undefined = 0,
        clingo_warning_runtime_error = 1,
        clingo_warning_atom_undefined = 2,
        clingo_warning_file_included = 3,
        clingo_warning_variable_unbounded = 4,
        clingo_warning_global_variable = 5,
        clingo_warning_other = 6
    }

    enum clingo_truth_value_e
    {
        clingo_truth_value_free = 0,
        clingo_truth_value_true = 1,
        clingo_truth_value_false = 2
    }

    public static class Clingo
    {
        private const string libName = "clingo";

        public const int CLINGO_VERSION_MAJOR = 5;

        public const int CLINGO_VERSION_MINOR = 5;

        public const int CLINGO_VERSION_REVISION = 0;

        public const string CLINGO_VERSION = "5.5.0";

        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string clingo_error_string(clingo_error_t code);

        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern clingo_error_t clingo_error_code();

        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string clingo_error_message();

        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void clingo_error_message(clingo_error_t code, string message);

        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string clingo_warning_string(clingo_warning_t code);

        public delegate void clingo_logger_t(clingo_warning_t code, string message, IntPtr data);

        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void clingo_version([Out] int major, [Out] int minor, [Out] int revision);
    }
}
