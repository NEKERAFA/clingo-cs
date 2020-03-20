using System;
using System.Collections.Generic;
using System.Linq;

namespace ClingoSharp.Enums
{
    /// <summary>
    /// Represents a Enumeration class
    /// </summary>
    public abstract class Enumeration : IEnumeration, IComparable<Enumeration>, IEquatable<Enumeration>
    {
        #region Class attributes

        protected static string[] m_names;

        #endregion

        #region Properties

        /// <summary>
        /// The value of the enumeration
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// The string value of the enumeration
        /// </summary>
        public string Name => m_names[Value];

        #endregion

        #region Constructors

        protected Enumeration(int value)
        {
            Value = value;
        }

        #endregion

        #region Enumeration methods

        /// <summary>
        /// Gets a iterator of the names of the constants in the enumeration 
        /// </summary>
        /// <returns>A string iterator with the names of the constants in the enumeration</returns>
        public static IEnumerable<string> GetNames()
        {
            return m_names;
        }

        /// <summary>
        /// Gets a iterator of the constants in the enumeration
        /// </summary>
        /// <returns>A <see cref="Enumerable"/> iterator with the constants in the enumeration</returns>
        public static IEnumerable<Enumeration> GetValues()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the <see cref="Enumerable"/> value that has the specified value
        /// </summary>
        /// <param name="value">The value of a particular enumerated constant in terms</param>
        /// <returns>The <see cref="Enumerable"/> containing the value of the enumerated constant</returns>
        public static Enumeration GetValue(int value)
        {
            return GetValues().FirstOrDefault(enumeration => enumeration.Value == value);
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        /// <returns>An <see cref="Enumeration"/> object whose value is represented by <paramref name="name"/></returns>
        public static Enumeration Parse(string name, bool ignoreCase)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                if (name == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            if (!TryParse(name, ignoreCase, out Enumeration enumeration))
            {
                throw new ArgumentException();
            }

            return enumeration;
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <returns>An <see cref="Enumeration"/> object whose value is represented by <paramref name="name"/></returns>
        public static Enumeration Parse(string name)
        {
            return Parse(name, false);
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="name"/></typeparam>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        /// <returns>An <see cref="Enumeration"/> object whose value is represented by <paramref name="name"/></returns>
        public static TEnum Parse<TEnum>(string name, bool ignoreCase) where TEnum : Enumeration
        {
            return (TEnum)Parse(name, ignoreCase);
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="name"/></typeparam>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <returns>An <see cref="Enumeration"/> object whose value is represented by <paramref name="name"/></returns>
        public static TEnum Parse<TEnum>(string name) where TEnum : Enumeration
        {
            return (TEnum)Parse(name, false);
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        /// <param name="result">When this method returns true, an object containing an enumeration constant representing the parsed value.</param>
        /// <returns><c>true</c> if the conversion succeeded; <c>false</c> otherwise.</returns>
        public static bool TryParse(string name, bool ignoreCase, out Enumeration result)
        {
            result = GetValues().FirstOrDefault(symbolType => symbolType.Name.Equals(name, ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture));
            return result == default;
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <param name="result">When this method returns true, an object containing an enumeration constant representing the parsed value.</param>
        /// <returns><c>true</c> if the conversion succeeded; <c>false</c> otherwise.</returns>
        public static bool TryParse(string name, out Enumeration result)
        {
            return TryParse(name, false, out result);
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="name"/></typeparam>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        /// <param name="result">When this method returns true, an object containing an enumeration constant representing the parsed value.</param>
        /// <returns></returns>
        public static bool TryParse<TEnum>(string name, bool ignoreCase, out TEnum result) where TEnum : Enumeration
        {
            if (TryParse(name, ignoreCase, out Enumeration enumeration))
            {
                result = (TEnum)enumeration;
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="name"/></typeparam>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <param name="result">When this method returns true, an object containing an enumeration constant representing the parsed value.</param>
        /// <returns></returns>
        public static bool TryParse<TEnum>(string name, out TEnum result) where TEnum : Enumeration
        {
            return TryParse(name, false, out result);
        }

        #endregion

        #region Equatable interface methods

        /// <inheritdoc/>
        public abstract bool Equals(Enumeration other);

        #endregion

        #region Comparable interface methods

        /// <inheritdoc/>
        public abstract int CompareTo(Enumeration other);

        #endregion

        #region object methods

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration))
            {
                return false;
            }

            var other = obj as Enumeration;
            return Equals(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ Name.GetHashCode();
        }

        #endregion
    }
}
