using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClingoSharp.Enums
{
    /// <summary>
    /// Represents a Enumeration class
    /// </summary>
    public abstract class Enumeration : IEnumeration, IComparable, IComparable<Enumeration>, IEquatable<Enumeration>
    {
        #region Properties

        /// <summary>
        /// The value of the enumeration
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// The string value of the enumeration
        /// </summary>
        public string Name { get; private set; }

        #endregion

        #region Constructors

        protected Enumeration(int value, string name)
        {
            Value = value;
            Name = name;
        }

        #endregion

        #region Enumeration methods

        /// <summary>
        /// Gets a iterator of the constants in the enumeration
        /// </summary>
        /// <param name="typeEnum">An <see cref="IEnumeration"/> value</param>
        /// <returns>A <see cref="IEnumeration"/> iterator with the constants in the enumeration</returns>
        /// <exception cref="ArgumentException"><paramref name="typeEnum"/> is not a <see cref="IEnumeration"/> subclass</exception>
        public static IEnumerable<IEnumeration> GetValues(Type typeEnum)
        {
            if (typeEnum.IsSubclassOf(typeof(Enumeration)))
            {
                var fields = typeEnum.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
                var values = fields.Select(f => f.GetValue(null)).Cast<IEnumeration>();
                return values;
            }

            throw new ArgumentException("is not an <IEnumeration> type", "typeEnum");
        }

        /// <summary>
        /// Gets a iterator of the constants in the enumeration
        /// </summary>
        /// <typeparam name="TEnum">An <see cref="IEnumeration"/> value</typeparam>
        /// <returns>A <see cref="TEnum"/> iterator with the constants in the enumeration</returns>
        public static IEnumerable<TEnum> GetValues<TEnum>() where TEnum : IEnumeration
        {
            return GetValues(typeof(TEnum)).Cast<TEnum>();
        }

        /// <summary>
        /// Gets a iterator of the names of the constants in the enumeration 
        /// </summary>
        /// <param name="typeEnum">An <see cref="IEnumeration"/> value</param>
        /// <returns>A string iterator with the names of the constants in the enumeration</returns>
        /// <exception cref="ArgumentException"><paramref name="typeEnum"/> is not a <see cref="IEnumeration"/> subclass</exception>
        public static IEnumerable<string> GetNames(Type typeEnum)
        {
            return GetValues(typeEnum).Select(v => v.Name);
        }

        /// <summary>
        /// Gets a iterator of the names of the constants in the enumeration 
        /// </summary>
        /// <typeparam name="TEnum">An <see cref="IEnumeration"/> value</typeparam>
        /// <returns>A string iterator with the names of the constants in the enumeration</returns>
        public static IEnumerable<string> GetNames<TEnum>() where TEnum : IEnumeration
        {
            return GetNames(typeof(TEnum));
        }

        /// <summary>
        /// Gets the <see cref="IEnumeration"/> value that has the specified value
        /// </summary>
        /// <param name="typeEnum">An <see cref="IEnumeration"/> value</param>
        /// <param name="value">The value of a particular enumerated constant in terms</param>
        /// <returns>The <see cref="IEnumeration"/> containing the value of the enumerated constant</returns>
        /// <exception cref="ArgumentException"><paramref name="typeEnum"/> is not a <see cref="IEnumeration"/> subclass</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="typeEnum"/> not contains a instance with the <paramref name="value"/> value</exception>
        public static IEnumeration GetValue(Type typeEnum, int value)
        {
            IEnumeration enumValue = GetValues(typeEnum).FirstOrDefault(enumeration => enumeration.Value == value);
            if (enumValue == null)
                throw new ArgumentOutOfRangeException("value");

            return enumValue;
        }

        /// <summary>
        /// Gets the <see cref="IEnumeration"/> value that has the specified value
        /// </summary>
        /// <typeparam name="TEnum">An <see cref="IEnumeration"/> value</typeparam>
        /// <param name="value">The value of a particular enumerated constant in terms</param>
        /// <returns>The <see cref="IEnumeration"/> containing the value of the enumerated constant</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="typeEnum"/> not contains a instance with the <paramref name="value"/> value</exception>
        public static TEnum GetValue<TEnum>(int value) where TEnum : IEnumeration
        {
            return (TEnum) GetValue(typeof(TEnum), value);
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <param name="typeEnum">An <see cref="IEnumeration"/> value</param>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        /// <param name="result">When this method returns true, an object containing an enumeration constant representing the parsed value.</param>
        /// <returns><c>true</c> if the conversion succeeded; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException"><paramref name="typeEnum"/> is not a <see cref="IEnumeration"/> subclass</exception>
        public static bool TryParse(Type typeEnum, string name, bool ignoreCase, out IEnumeration result)
        {
            result = GetValues(typeEnum).FirstOrDefault(symbolType => symbolType.Name.Equals(name, ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture));
            return result != default;
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <param name="typeEnum">An <see cref="IEnumeration"/> value</param>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <param name="result">When this method returns true, an object containing an enumeration constant representing the parsed value.</param>
        /// <returns><c>true</c> if the conversion succeeded; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException"><paramref name="typeEnum"/> is not a <see cref="IEnumeration"/> subclass</exception>
        public static bool TryParse(Type typeEnum, string name, out IEnumeration result)
        {
            return TryParse(typeEnum, name, false, out result);
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <typeparam name="TEnum">An <see cref="IEnumeration"/> value</typeparam>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        /// <param name="enumeration">When this method returns true, an object containing an enumeration constant representing the parsed value.</param>
        /// <returns><c>true</c> if the conversion succeeded; <c>false</c> otherwise.</returns>
        public static bool TryParse<TEnum>(string name, bool ignoreCase, out TEnum enumeration)
        {
            bool parsed = TryParse(typeof(TEnum), name, ignoreCase, out IEnumeration result);
            enumeration = (TEnum)result;
            return parsed;
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <typeparam name="TEnum">An <see cref="IEnumeration"/> value</typeparam>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <param name="enumeration">When this method returns true, an object containing an enumeration constant representing the parsed value.</param>
        /// <returns><c>true</c> if the conversion succeeded; <c>false</c> otherwise.</returns>
        public static bool TryParse<TEnum>(string name, out TEnum enumeration)
        {
            return TryParse(name, false, out enumeration);
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <param name="typeEnum">An <see cref="IEnumeration"/> value</param>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c></exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty</exception>
        /// <exception cref="ArgumentException"><paramref name="typeEnum"/> is not a <see cref="IEnumeration"/> subclass</exception>
        /// <exception cref="ArgumentException"><paramref name="typeEnum"/> not contains a instance with the name <paramref name="name"/> value</exception>
        public static IEnumeration Parse(Type typeEnum, string name, bool ignoreCase = false)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                if (name == null)
                    throw new ArgumentNullException("name");

                throw new ArgumentException("cannot be empty", "name");
            }

            if (!TryParse(typeEnum, name, ignoreCase, out IEnumeration result))
                throw new ArgumentException($"<{typeEnum.Name}> not contains a instance with the name {name}");

            return result;
        }

        /// <summary>
        /// Converts the string representation of the name to an equivalent enumerated object
        /// </summary>
        /// <typeparam name="TEnum">An <see cref="IEnumeration"/> value</typeparam>
        /// <param name="name">A string containing the name or value to convert</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c></exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty</exception>
        /// <exception cref="ArgumentException"><typeparamref name="TEnum"/> not contains a instance with the name <paramref name="name"/> value</exception>
        public static TEnum Parse<TEnum>(string name, bool ignoreCase = false) where TEnum : IEnumeration
        {
            return (TEnum)Parse(typeof(TEnum), name, ignoreCase);
        }

        #endregion

        #region Equatable interface methods

        /// <inheritdoc/>
        public abstract bool Equals(Enumeration other);

        #endregion

        #region Comparable interface methods

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        /// <exception cref="ArgumentException"><c>obj</c> is not the same type as this instance.</exception>
        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is Enumeration))
            {
                throw new ArgumentException($"{obj.GetType().Name} object is null or not {this.GetType().Name} type");
            }

            return CompareTo(obj as Enumeration);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        /// <exception cref="ArgumentException"><c>obj</c> is not the same type as this instance.</exception>
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

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{GetType().FullName}.{Name}";
        }

        #endregion
    }
}
