using System;

namespace ClingoSharp
{
    /// <summary>
    /// Represents a union of a literal or a (atom, bool) tuple.
    /// </summary>
    public class Assumption
    {
        /// <summary>
        /// Represents the assumption type
        /// </summary>
        public enum Type { Literal = 1, Tuple = 2 }

        private Type _currentValue;
        private int _intValue;
        private Tuple<Symbol, bool> _tupleValue;

        /// <summary>
        /// Creates an assumption of a program literal
        /// </summary>
        /// <param name="value">the literal value</param>
        public Assumption(int value)
        {
            SetValue(value);
        }

        /// <summary>
        /// Creates an assumption of an (atom, bool) tuple.
        /// </summary>
        /// <param name="value">the tuple value</param>
        public Assumption(Tuple<Symbol, bool> value)
        {
            SetValue(value);
        }

        /// <summary>
        /// Sets the assumptions as a program literal.
        /// </summary>
        /// <param name="value">the literal value</param>
        public void SetValue(int value)
        {
            _intValue = value;
            _currentValue = Type.Literal;
        }

        /// <summary>
        /// Sets the assumptions as an (atom, bool) tuple.
        /// </summary>
        /// <param name="value">the tuple value</param>
        public void SetValue(Tuple<Symbol, bool> value)
        {
            _tupleValue = value;
            _currentValue = Type.Tuple;
        }

        /// <summary>
        /// Gets the literal value
        /// </summary>
        /// <returns>the literal value</returns>
        public int GetLiteralValue()
        {
            if (!IsLiteral())
            {
                throw new FieldAccessException("Assumption have not a Literal value");
            }

            return _intValue;
        }

        /// <summary>
        /// Gets the tuple value
        /// </summary>
        /// <returns>the tuple value</returns>
        public Tuple<Symbol, bool> GetTupleValue()
        {
            if (!IsTuple())
            {
                throw new FieldAccessException("Assumption have not a Tuple value");
            }

            return _tupleValue;
        }

        /// <summary>
        /// Checks if the assumption has a literal value
        /// </summary>
        /// <returns>true if the assumption is a literal, false otherwise</returns>
        public bool IsLiteral()
        {
            return _currentValue == Type.Literal;
        }

        /// <summary>
        /// Checks if the assumption has a tuple value
        /// </summary>
        /// <returns>true if the assumption is a tuple, false otherwise</returns>
        public bool IsTuple()
        {
            return _currentValue == Type.Tuple;
        }
    }
}
