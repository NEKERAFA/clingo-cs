using System;

namespace ClingoSharp
{
    /// <summary>
    /// Represents a union of two types
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public struct Union<T1, T2> : IEquatable<Union<T1, T2>>, IComparable, IComparable<Union<T1, T2>>, ICloneable, IFormattable
    {
        private enum ValueState { Undefined, Value1, Value2 }

        #region Attributes

        private T1 value1;
        private T2 value2;
        private ValueState state;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a union with <typeparamref name="T1"/> value.
        /// </summary>
        /// <param name="value">A <typeparamref name="T1"/> value</param>
        public Union(T1 value)
        {
            value1 = value;
            value2 = default;
            state = ValueState.Value1;
        }

        /// <summary>
        /// Creates a union with <typeparamref name="T2"/> value.
        /// </summary>
        /// <param name="value">A <typeparamref name="T2"/> value</param>
        public Union(T2 value)
        {
            value1 = default;
            value2 = value;
            state = ValueState.Value2;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the union as <typeparamref name="T1"/> value.
        /// </summary>
        /// <param name="value">A <typeparamref name="T1"/> value</param>
        public void Set(T1 value)
        {
            value1 = value;
            value2 = default;
            state = ValueState.Value1;
        }

        /// <summary>
        /// Sets the union as <typeparamref name="T2"/> value.
        /// </summary>
        /// <param name="value">A <typeparamref name="T2"/> value</param>
        public void Set(T2 value)
        {
            value1 = default;
            value2 = value;
            state = ValueState.Value2;
        }

        /// <summary>
        /// Checks if the union has the type setted.
        /// </summary>
        /// <param name="type">The type of the union</param>
        /// <returns>True if the union has the type setted, false otherwise</returns>
        public bool IsType(Type type)
        {
            if (type == typeof(T1))
            {
                return state == ValueState.Value1;
            }
            else if (type == typeof(T2))
            {
                return state == ValueState.Value2;
            }

            return false;
        }

        /// <summary>
        /// Checks if the union has the type setted.
        /// </summary>
        /// <typeparam name="T">The type of the union</typeparam>
        /// <returns>True if the union has the type setted, false otherwise</returns>
        public bool IsType<T>()
        {
            return IsType(typeof(T));
        }

        /// <summary>
        /// Tries to get a value.
        /// </summary>
        /// <param name="type">The type of the value</param>
        /// <param name="value">The result value</param>
        /// <returns>True if the union has setted the value with <paramref name="type"/> type, false otherwise</returns>
        public bool TryGet(Type type, out object value)
        {
            if (type == typeof(T1))
            {
                if (state == ValueState.Value1)
                {
                    value = value1;
                    return true;
                }
            }

            if (type == typeof(T2))
            {
                if (state == ValueState.Value2)
                {
                    value = value2;
                    return true;
                }
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Tries to get a value.
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="value">The result value</param>
        /// <returns>True if the union has setted the value with <paramref name="type"/> type, false otherwise</returns>
        public bool TryGet<T>(out T value)
        {
            var success = TryGet(typeof(T), out object val);
            value = (T)val;
            return success;
        }

        /// <summary>
        /// Gets a value.
        /// </summary>
        /// <param name="type">The type of the value</param>
        /// <returns>The result value</returns>
        /// <exception cref="FieldAccessException">The union is not the type setted</exception>
        public object Get(Type type)
        {
            if (TryGet(type, out object value))
            {
                return value;
            }
            else
            {
                throw new FieldAccessException($"{typeof(Union<T1, T2>).Name} is not a {type.Name} value");
            }
        }

        /// <summary>
        /// Gets a value.
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <returns>The result value</returns>
        /// <exception cref="FieldAccessException">The union have not a <typeparamref name="T"/> value</exception>
        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        #endregion

        #region Clonable implementation

        /// <summary>
        /// Converts a <typeparamref name="T1"/> value in a <see cref="Union{T1, T2}"/>
        /// </summary>
        /// <param name="value">The <typeparamref name="T1"/> value to convert in a <see cref="Union{T1, T2}"/></param>
        public static implicit operator Union<T1, T2>(T1 value)
        {
            return new Union<T1, T2>(value);
        }

        /// <summary>
        /// Converts a <typeparamref name="T2"/> value in a <see cref="Union{T1, T2}"/>
        /// </summary>
        /// <param name="value">The <typeparamref name="T2"/> value to convert in a <see cref="Union{T1, T2}"/></param>
        public static implicit operator Union<T1, T2>(T2 value)
        {
            return new Union<T1, T2>(value);
        }

        /// <summary>
        /// Gets the <typeparamref name="T1"/> value
        /// </summary>
        /// <param name="union">A <see cref="Union{T1, T2}"/> value</param>
        /// <exception cref="FieldAccessException">The union have not a <typeparamref name="T1"/> value</exception>
        public static implicit operator T1(Union<T1, T2> union)
        {
            if (union.state != ValueState.Value1)
            {
                throw new FieldAccessException($"{typeof(Union<T1, T2>).Name} is not a {typeof(T1).Name} value");
            }

            return union.value1;
        }

        /// <summary>
        /// Gets the <typeparamref name="T2"/> value
        /// </summary>
        /// <param name="union">A <see cref="Union{T1, T2}"/> value</param>
        /// <exception cref="FieldAccessException">The union have not a <typeparamref name="T2"/> value</exception>
        public static implicit operator T2(Union<T1, T2> union)
        {
            if (union.state != ValueState.Value2)
            {
                throw new FieldAccessException($"{typeof(Union<T1, T2>).Name} is not a {typeof(T2).Name} value");
            }

            return union.value2;
        }

        /// <inheritdoc/>
        public object Clone()
        {
            if (state == ValueState.Value1)
            {
                T1 value = value1;
                if (value is ICloneable)
                {
                    value = (T1)((ICloneable)value).Clone();
                }

                Union<T1, T2> newUnion = new Union<T1, T2>(value);
                return newUnion;
            }
            else if (state == ValueState.Value2)
            {
                T2 value = value2;
                if (value is ICloneable)
                {
                    value = (T2)((ICloneable)value).Clone();
                }

                Union<T1, T2> newUnion = new Union<T1, T2>(value);
                return newUnion;
            }

            throw new InvalidOperationException($"The {typeof(Union<T1, T2>).Name} is not initialized");
        }

        #endregion

        #region Equatable implementation

        /// <summary>
        /// Indicates whether the current <see cref="Union{T1, T2}"/> is equal to another <see cref="Union{T1, T2}"/>.
        /// </summary>
        /// <param name="union">The <see cref="Union{T1, T2}"/> value</param>
        /// <param name="other">An object to compare with</param>
        /// <returns><c>true</c> if the current <see cref="Union{T1, T2}"/> is equal to the other <see cref="Union{T1, T2}"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Union<T1, T2> union, object other)
        {
            return union.Equals(other);
        }

        /// <summary>
        /// Indicates whether the current object is not equal to another object of the same type.
        /// </summary>
        /// <param name="union">The <see cref="Union{T1, T2}"/> value</param>
        /// <param name="other">An object to compare with</param>
        /// <returns><c>false</c> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <c>true</c>.</returns>
        public static bool operator !=(Union<T1, T2> union, object other)
        {
            return !union.Equals(other);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Union<T1, T2>))
            {
                return false;
            }

            return Equals((Union<T1, T2>)obj);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Union{T1, T2}"/> is equal to another <see cref="Union{T1, T2}"/>.
        /// </summary>
        /// <param name="other">A <see cref="Union{T1, T2}"/> to compare with this object</param>
        /// <returns><c>true</c> if the current <see cref="Union{T1, T2}"/> is equal to the other <see cref="Union{T1, T2}"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(Union<T1, T2> other)
        {
            if ((state == other.state) && (state == ValueState.Value1))
            {
                return value1.Equals(other.value1);
            }
            else if ((state == other.state) && (state == ValueState.Value2))
            {
                return value2.Equals(other.value2);
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            if (state == ValueState.Value1)
            {
                return value1.GetHashCode();
            }

            if (state == ValueState.Value2)
            {
                return value2.GetHashCode();
            }

            return base.GetHashCode();
        }

        #endregion

        #region Comparable implementation

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">if the <see cref="Union{T1, T2}"/> is not comparable</exception>
        public int CompareTo(Union<T1, T2> other)
        {
            if ((state == other.state) && (state == ValueState.Value1))
            {
                if (value1 is IComparable)
                {
                    return ((IComparable)value1).CompareTo(other.value1);
                }

                throw new InvalidOperationException();
            }
            else if ((state == other.state) && (state == ValueState.Value2))
            {
                if (value2 is IComparable)
                {
                    return ((IComparable)value2).CompareTo(other.value2);
                }

                throw new InvalidOperationException();
            }

            throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">if the <see cref="Union{T1, T2}"/> is not comparable</exception>
        public int CompareTo(object obj)
        {
            if ((obj == null) || !(obj is Union<T1, T2>))
            {
                throw new ArgumentException($"{obj.GetType().Name} is not {typeof(Union<T1, T2>).Name}");
            }

            return CompareTo((Union<T1, T2>)obj);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="union">The <see cref="Union{T1, T2}"/> value</param>
        /// <param name="other">An object to compare with</param>
        /// <returns><c>true</c> if the current instance precedes the <paramref name="other"/> parameter; otherwise, <c>false</c></returns>
        /// <exception cref="InvalidOperationException">if the <see cref="Union{T1, T2}"/> is not comparable</exception>
        public static bool operator <(Union<T1, T2> union, object other)
        {
            return union.CompareTo(other) < 0;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="union">The <see cref="Union{T1, T2}"/> value</param>
        /// <param name="other">An object to compare with</param>
        /// <returns><c>true</c> if the current instance precedes or is equals to the <paramref name="other"/> parameter; otherwise, <c>false</c></returns>
        /// <exception cref="InvalidOperationException">if the <see cref="Union{T1, T2}"/> is not comparable</exception>
        public static bool operator <=(Union<T1, T2> union, object other)
        {
            return union.CompareTo(other) <= 0;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="union">The <see cref="Union{T1, T2}"/> value</param>
        /// <param name="other">An object to compare with</param>
        /// <returns><c>true</c> if the current instance follows the <paramref name="other"/> parameter; otherwise, <c>false</c></returns>
        /// <exception cref="InvalidOperationException">if the <see cref="Union{T1, T2}"/> is not comparable</exception>
        public static bool operator >(Union<T1, T2> union, object other)
        {
            return union.CompareTo(other) > 0;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="union">The <see cref="Union{T1, T2}"/> value</param>
        /// <param name="other">An object to compare with</param>
        /// <returns><c>true</c> if the current instance follows or is equals to the <paramref name="other"/> parameter; otherwise, <c>false</c></returns>
        /// <exception cref="InvalidOperationException">if the <see cref="Union{T1, T2}"/> is not comparable</exception>
        public static bool operator >=(Union<T1, T2> union, object other)
        {
            return union.CompareTo(other) >= 0;
        }

        #endregion

        #region Formattable implementation

        /// <inheritdoc/>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (state == ValueState.Value1)
            {
                if (value1 is IFormattable)
                {
                    return ((IFormattable)value1).ToString(format, formatProvider);
                }

                return value1.ToString();
            }
            else if (state == ValueState.Value2)
            {
                if (value2 is IFormattable)
                {
                    return ((IFormattable)value2).ToString(format, formatProvider);
                }

                return value2.ToString();
            }

            return ToString();
        }

        public override string ToString()
        {
            if (state == ValueState.Value1)
            {
                return value1.ToString();
            }
            else if (state == ValueState.Value2)
            {
                return value2.ToString();
            }

            return base.ToString();
        }

        #endregion
    }
}
