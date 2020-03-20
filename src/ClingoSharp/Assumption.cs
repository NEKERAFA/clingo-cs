using System;

namespace ClingoSharp
{
    /// <summary>
    /// Represents a union of a literal or a (atom, bool) tuple.
    /// </summary>
    public class Assumption
    {
        #region Enumerables

        private enum TypeValue { Literal }

        #endregion

        #region Attributes

        private TypeValue m_currentValue;
        private int m_intValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an assumption of a program literal
        /// </summary>
        /// <param name="value">the literal value</param>
        public Assumption(int value)
        {
            SetValue(value);
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Sets the assumptions as a program literal.
        /// </summary>
        /// <param name="value">the literal value</param>
        public void SetValue(int value)
        {
            m_intValue = value;
            m_currentValue = TypeValue.Literal;
        }

        /// <summary>
        /// Gets the literal value
        /// </summary>
        /// <returns>the literal value</returns>
        public int GetLiteralValue()
        {
            if (!IsLiteral())
            {
                throw new FieldAccessException("Assumption is not a Literal value");
            }

            return m_intValue;
        }

        /// <summary>
        /// Checks if the assumption has a literal value
        /// </summary>
        /// <returns>true if the assumption is a literal, false otherwise</returns>
        public bool IsLiteral()
        {
            return m_currentValue == TypeValue.Literal;
        }

        #endregion
    }
}
