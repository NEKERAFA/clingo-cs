using ClingoSharp.Enums;
using System.Collections.Generic;

namespace ClingoSharp
{
    /// <summary>
    /// Enumeration of the different types of messages.
    /// </summary>
    public class MessageCode : Enumeration
    {
        #region Class attributes

        protected new static string[] m_names = new string[] 
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

        public static MessageCode OperationUndefined => new MessageCode(0);
        public static MessageCode RuntimeError => new MessageCode(1);
        public static MessageCode AtomUndefined => new MessageCode(2);
        public static MessageCode FileIncluded => new MessageCode(3);
        public static MessageCode VariableUnbounded => new MessageCode(4);
        public static MessageCode GlobalVariable => new MessageCode(5);
        public static MessageCode Other => new MessageCode(6);

        #endregion

        #region Constructors

        private MessageCode(int value) : base(value) {}

        #endregion

        #region Class methods

        /// <summary>
        /// Gets a iterator of the constants in the enumeration
        /// </summary>
        /// <returns>A <see cref="Enumerable"/> iterator with the constants in the enumeration</returns>
        public new static IEnumerable<Enumeration> GetValues()
        {
            return new MessageCode[] { OperationUndefined, RuntimeError, AtomUndefined, FileIncluded, VariableUnbounded, GlobalVariable, Other };
        }

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

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"CligoSharp.MessageCode<{Name}>";
        }

        #endregion
    }
}
