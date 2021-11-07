using System;
using System.Runtime.InteropServices;
using ClingoSharp.Native;
using ClingoSharp.Enums;

namespace ClingoSharp
{
    public static class Core
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

            Clingo.clingo_version(majorRef, minorRef, revisionRef);

            int major = Marshal.ReadInt32(majorRef);
            int minor = Marshal.ReadInt32(minorRef);
            int revision = Marshal.ReadInt32(revisionRef);

            Marshal.FreeHGlobal(majorRef);
            Marshal.FreeHGlobal(minorRef);
            Marshal.FreeHGlobal(revisionRef);

            return (major, minor, revision);
        }

        public sealed class MessageCode : Enumeration
        {
            #region Class attributes

            private static readonly string[] MessageCodeNames = new string[]
            {
                "OperationUndefined",
                "RuntimeError",
                "AtomUndefined",
                "FileIncluded",
                "VariableUnbounded",
                "GlobalVariable",
                "Other"
            };

            #endregion

            #region Class Properties

            /// <summary>
            /// Inform about an undefined arithmetic operation or unsupported weight of an aggregate.
            /// </summary>
            public static MessageCode OperationUndefined => new MessageCode((int)clingo_warning_e.clingo_warning_operation_undefined);

            /// <summary>
            /// To report multiple errors; a corresponding runtime error is raised later.
            /// </summary>
            public static MessageCode RuntimeError => new MessageCode((int)clingo_warning_e.clingo_warning_runtime_error);

            /// <summary>
            /// Informs about an undefined atom in program.
            /// </summary>
            public static MessageCode AtomUndefined => new MessageCode((int)clingo_warning_e.clingo_warning_atom_undefined);

            /// <summary>
            /// Indicates that the same file was included multiple times.
            /// </summary>
            public static MessageCode FileIncluded => new MessageCode((int)clingo_warning_e.clingo_warning_file_included);

            /// <summary>
            /// Informs about a CSP variable with an unbounded domain.
            /// </summary>
            public static MessageCode VariableUnbounded => new MessageCode((int)clingo_warning_e.clingo_warning_variable_unbounded);

            /// <summary>
            /// Informs about a global variable in a tuple of an aggregate element.
            /// </summary>
            public static MessageCode GlobalVariable => new MessageCode((int)clingo_warning_e.clingo_warning_global_variable);

            /// <summary>
            /// Reports other kinds of messages.
            /// </summary>
            public static MessageCode Other => new MessageCode((int)clingo_warning_e.clingo_warning_other);

            #endregion

            #region Constructors

            private MessageCode(int value) : base(value, MessageCodeNames[value]) { }

            #endregion

            #region Instance methods

            /// <inheritdoc/>
            public override int CompareTo(Enumeration other)
            {
                if ((other == null) || !(other is MessageCode))
                {
                    return 1;
                }

                return Value.CompareTo(other.Value);
            }

            /// <inheritdoc/>
            public override bool Equals(Enumeration other)
            {
                if ((other == null) || !(other is MessageCode))
                {
                    return false;
                }

                return Value.Equals(other.Value);
            }

            #endregion
        }
    }
}