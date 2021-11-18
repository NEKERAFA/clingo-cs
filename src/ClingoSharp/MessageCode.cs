using System;
using ClingoSharp.Enums;
using static ClingoSharp.Native.Clingo_c;

namespace ClingoSharp
{
    /// <summary>
    /// Enumeration of messages codes
    /// </summary>
    public sealed class MessageCode : Enumeration, IEquatable<MessageCode>, IComparable, IComparable<MessageCode>
    {
        #region Class Properties

        /// <summary>
        /// Informs about an undefined atom in program.
        /// </summary>
        public static MessageCode AtomUndefined => new MessageCode(clingo_warning_e.clingo_warning_atom_undefined, "AtomUndefined");

        /// <summary>
        /// Indicates that the same file was included multiple times.
        /// </summary>
        public static MessageCode FileIncluded => new MessageCode(clingo_warning_e.clingo_warning_file_included, "FiledIncluded");

        /// <summary>
        /// Informs about a global variable in a tuple of an aggregate element.
        /// </summary>
        public static MessageCode GlobalVariable => new MessageCode(clingo_warning_e.clingo_warning_global_variable, "GlobalVariable");

        /// <summary>
        /// Inform about an undefined arithmetic operation or unsupported weight of an aggregate.
        /// </summary>
        public static MessageCode OperationUndefined => new MessageCode(clingo_warning_e.clingo_warning_operation_undefined, "OperationUndefined");

        /// <summary>
        /// Reports other kinds of messages.
        /// </summary>
        public static MessageCode Other => new MessageCode(clingo_warning_e.clingo_warning_other, "Other");

        /// <summary>
        /// To report multiple errors; a corresponding runtime error is raised later.
        /// </summary>
        public static MessageCode RuntimeError => new MessageCode(clingo_warning_e.clingo_warning_runtime_error, "RuntimeError");

        /// <summary>
        /// Informs about a CSP variable with an unbounded domain.
        /// </summary>
        public static MessageCode VariableUnbounded => new MessageCode(clingo_warning_e.clingo_warning_variable_unbounded, "VariableUnbounded");

        #endregion

        #region Constructors

        private MessageCode(clingo_warning_e value, string name) : base((int)value, name) { }

        #endregion

        #region Class methods

        public static bool operator <(MessageCode a, MessageCode b) => a.Value < b.Value;
        public static bool operator <=(MessageCode a, MessageCode b) => a.Value <= b.Value;
        public static bool operator >(MessageCode a, MessageCode b) => a.Value > b.Value;
        public static bool operator >=(MessageCode a, MessageCode b) => a.Value >= b.Value;
        public static bool operator ==(MessageCode a, MessageCode b) => a.Value == b.Value;
        public static bool operator !=(MessageCode a, MessageCode b) => a.Value != b.Value;

        #endregion

        #region Instance methods

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        /// <exception cref="ArgumentException"><c>obj</c> is not the same type as this instance.</exception>
        public override int CompareTo(Enumeration other)
        {
            if ((other == null) || !(other is MessageCode))
            {
                throw new ArgumentException($"{other.GetType().Name} object is null or not {this.GetType().Name} type");
            }

            return CompareTo(other as MessageCode);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        /// <exception cref="ArgumentException"><c>obj</c> is not the same type as this instance.</exception>
        public int CompareTo(MessageCode other) => Value.CompareTo(other.Value);

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(Enumeration other)
        {
            if ((other == null) || !(other is MessageCode))
            {
                return false;
            }

            return Equals(other as MessageCode);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public bool Equals(MessageCode other) => this.Value.Equals(other.Value);

        public override int GetHashCode() => base.GetHashCode();

        public override bool Equals(object obj) => base.Equals(obj);

        #endregion
    }
}