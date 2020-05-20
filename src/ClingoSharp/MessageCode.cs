using ClingoSharp.Enums;

namespace ClingoSharp
{
    /// <summary>
    /// Enumeration of the different types of messages.
    /// </summary>
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
        public static MessageCode OperationUndefined => new MessageCode(0);

        /// <summary>
        /// To report multiple errors; a corresponding runtime error is raised later.
        /// </summary>
        public static MessageCode RuntimeError => new MessageCode(1);

        /// <summary>
        /// Informs about an undefined atom in program.
        /// </summary>
        public static MessageCode AtomUndefined => new MessageCode(2);

        /// <summary>
        /// Indicates that the same file was included multiple times.
        /// </summary>
        public static MessageCode FileIncluded => new MessageCode(3);

        /// <summary>
        /// Informs about a CSP variable with an unbounded domain.
        /// </summary>
        public static MessageCode VariableUnbounded => new MessageCode(4);

        /// <summary>
        /// Informs about a global variable in a tuple of an aggregate element.
        /// </summary>
        public static MessageCode GlobalVariable => new MessageCode(5);

        /// <summary>
        /// Reports other kinds of messages.
        /// </summary>
        public static MessageCode Other => new MessageCode(6);

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
