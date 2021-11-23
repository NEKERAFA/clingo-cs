using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ClingoSharp.Enums;
using static ClingoSharp.Clingo;
using static ClingoSharp.Native.Clingo_c;

namespace ClingoSharp.Symbols
{
    /// <summary>
    /// Represents a gringo symbol.
    /// 
    /// <para>
    /// This includes numbers, strings, functions(including constants with
    /// <c>len(arguments) == 0</c> and tuples with <c>len(name) == 0)</c>, <c>#inf</c> and <c>#sup.</c>
    /// </para>
    /// </summary>
    public sealed class Symbol : IEquatable<Symbol>, IComparable, IComparable<Symbol>
    {
        #region Attributes

        /// <summary>
        /// The <c>#inf</c> constructor.
        /// </summary>
        private static readonly Lazy<Symbol> _infimum = new(() =>
        {
            clingo_symbol_t[] symbol_c = new clingo_symbol_t[1];
            clingo_symbol_create_infimum(symbol_c);
            return new Symbol(symbol_c[0]);
        });

        /// <summary>
        /// The <c>#sup</c> constructor.
        /// </summary>
        private static readonly Lazy<Symbol> _supremum = new(() =>
        {
            clingo_symbol_t[] symbol_c = new clingo_symbol_t[1];
            clingo_symbol_create_supremum(symbol_c);
            return new Symbol(symbol_c[0]);
        });

        /// <summary>
        /// The clingo symbol
        /// </summary>
        private readonly clingo_symbol_t m_symbol;

        #endregion

        #region Class properties

        /// <summary>
        /// Construct a symbol representing <c>#inf</c>.
        /// </summary>
        public static Symbol Infimum => _infimum.Value;

        /// <summary>
        /// Construct a symbol representing <c>#sup</c>.
        /// </summary>
        public static Symbol Supremum => _supremum.Value;

        #endregion

        #region Instance properties

        /// <summary>
        /// The arguments of a function.
        /// </summary>
        public List<Symbol> Arguments
        {
            get
            {
                IntPtr[] args_ptr_c = new IntPtr[1];
                size_t[] args_size_c = new size_t[1];
                HandleError(clingo_symbol_arguments(m_symbol, args_ptr_c, args_size_c));
                clingo_symbol_t[] args_c = new clingo_symbol_t[args_size_c[0]];
                CopySymbols(args_ptr_c[0], args_c, 0, (int)(uint)args_size_c[0]);
                return args_c.Select(symbol => new Symbol(symbol)).ToList();
            }
        }

        /// <summary>
        /// The inverted sign of a function.
        /// </summary>
        public bool IsNegative
        {
            get
            {
                bool[] negative_c = new bool[1];
                HandleError(clingo_symbol_is_negative(m_symbol, negative_c));
                return negative_c[0];
            }
        }

        /// <summary>
        /// The sign of a function.
        /// </summary>
        public bool IsPositive
        {
            get
            {
                bool[] positive_c = new bool[1];
                HandleError(clingo_symbol_is_positive(m_symbol, positive_c));
                return positive_c[0];
            }
        }

        /// <summary>
        /// The name of a function.
        /// </summary>
        public string Name
        {
            get
            {
                IntPtr name_ptr_c = Marshal.AllocHGlobal(IntPtr.Size);
                HandleError(clingo_symbol_name(m_symbol, name_ptr_c));
                string name_c = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(name_ptr_c));
                Marshal.FreeHGlobal(name_ptr_c);
                return name_c;
            }
        }

        /// <summary>
        /// The value of a number.
        /// </summary>
        public int Number
        {
            get
            {
                int[] number_c = new int[1];
                HandleError(clingo_symbol_number(m_symbol, number_c));
                return number_c[0];
            }
        }

        /// <summary>
        /// The value of a string
        /// </summary>
        public string String
        {
            get
            {
                IntPtr str_ptr_c = Marshal.AllocHGlobal(IntPtr.Size);
                HandleError(clingo_symbol_string(m_symbol, str_ptr_c));
                string str_c = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(str_ptr_c));
                Marshal.FreeHGlobal(str_ptr_c);
                return str_c;
            }
        }

        public SymbolType Type
        {
            get
            {
                clingo_symbol_type_t type = clingo_symbol_type(m_symbol);
                return Enumeration.GetValue<SymbolType>((int)type);
            }
        }

        #endregion

        #region Constructors

        internal Symbol(clingo_symbol_t symbol)
        {
            m_symbol = symbol;
        }

        /// <summary>
        /// Construct a function symbol
        /// 
        /// <para>
        /// This includes constants and tuples. Constants have an empty argument list
        /// and tuples have an empty name.Functions can represent classically negated
        /// atoms.Argument `positive` has to be set to false to represent such atoms.
        /// </para>
        /// </summary>
        /// <param name="name">The name of the function (empty for tuples).</param>
        /// <param name="arguments">The arguments in form of a list of symbols.</param>
        /// <param name="isPositive">The sign of the function (tuples must not have signs).</param>
        public Symbol(string name, List<Symbol> arguments, bool isPositive = true)
        {
            size_t args_size_c = new(arguments != null ? (uint)arguments.Count : 0u);
            clingo_symbol_t[] args_c = arguments != null ? arguments.Select(symbol => symbol.m_symbol).ToArray() : Array.Empty<clingo_symbol_t>();
            clingo_symbol_t[] symbol_c = new clingo_symbol_t[1];
            HandleError(clingo_symbol_create_function(name, args_c, args_size_c, isPositive, symbol_c));
            m_symbol = symbol_c[0];
        }

        /// <summary>
        /// Construct a numeric symbol given a number.
        /// </summary>
        /// <param name="number">The given number.</param>
        public Symbol(int number)
        {
            clingo_symbol_t[] symbol_c = new clingo_symbol_t[1];
            clingo_symbol_create_number(number, symbol_c);
            m_symbol = symbol_c[0];
        }

        /// <summary>
        /// Construct a string symbol given a string.
        /// </summary>
        /// <param name="str">The given string.</param>
        public Symbol(string str)
        {
            clingo_symbol_t[] symbol_c = new clingo_symbol_t[1];
            HandleError(clingo_symbol_create_string(str, symbol_c));
            m_symbol = symbol_c[0];
        }

        public Symbol(string name, bool isPositive)
        {
            clingo_symbol_t[] symbol_c = new clingo_symbol_t[1];
            HandleError(clingo_symbol_create_id(name, isPositive, symbol_c));
            m_symbol = symbol_c[0];
        }

        /// <summary>
        /// A shortcut for <c>Symbol("", arguments)</c>
        /// </summary>
        /// <seealso cref="Symbol(string, List{Symbol}, bool)" />
        /// <param name="arguments">The arguments in form of a list of symbols.</param>
        public Symbol(List<Symbol> arguments) : this("", arguments) { }

        #endregion

        #region Class Methods

        /// <summary>
        /// Parse the given string using gringo's term parser for ground terms.
        /// 
        /// <para>
        /// The function also evaluates arithmetic functions.
        /// </para>
        /// </summary>
        /// <param name="str">The string to be parsed.</param>
        /// <param name="logger">Function to intercept messages normally printed to standard error.</param>
        /// <param name="messageLimit">Maximum number of messages passed to the logger.</param>
        /// <returns>The new symbol</returns>
        public static Symbol ParseTerm(string str, Action<MessageCode, string> logger = null, int messageLimit = 20)
        {
            void loggerCb_c(clingo_warning_t code, string message, IntPtr data)
            {
                if (logger != null)
                {
                    MessageCode messageCode = Enumeration.GetValue<MessageCode>((int)code);
                    logger(messageCode, message);
                    return;
                }

                HandleWarning(code, message);
            }

            clingo_symbol_t[] symbol_c = new clingo_symbol_t[1];
            HandleError(clingo_parse_term(str, loggerCb_c, IntPtr.Zero, Convert.ToUInt32(messageLimit), symbol_c));
            return new(symbol_c[0]);
        }

        public static implicit operator clingo_symbol_t(Symbol type) => type.m_symbol;

        public static implicit operator Symbol(int value) => new(value);

        public static implicit operator Symbol(string value) => new(value);

        public static bool operator <(Symbol a, Symbol b) => clingo_symbol_is_less_than(a, b);

        public static bool operator <=(Symbol a, Symbol b) => !clingo_symbol_is_less_than(b, a);

        public static bool operator >(Symbol a, Symbol b) => clingo_symbol_is_less_than(b, a);

        public static bool operator >=(Symbol a, Symbol b) => !clingo_symbol_is_less_than(a, b);

        public static bool operator ==(Symbol a, Symbol b) => clingo_symbol_is_equal_to(a, b);

        public static bool operator !=(Symbol a, Symbol b) => !clingo_symbol_is_equal_to(a, b);

        internal static void CopySymbols(IntPtr source, clingo_symbol_t[] destination, int startIndex, int length)
        {
            long[] destination_l = new long[length];
            Marshal.Copy(source, destination_l, startIndex, length);
            for (int i = 0; i < length; i++)
            {
                destination[i] = (ulong)destination_l[i];
            }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Check if this is a function symbol with the given signature.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="arity">The arity of the function.</param>
        /// <param name="isPositive">Whether to match positive or negative signatures.</param>
        /// <returns>Whether the function matches.</returns>
        public bool Match(string name, int arity, bool isPositive = true)
        {
            return (Type == SymbolType.Function) &&
                    (IsPositive == isPositive) &&
                    (Name == name) &&
                    Arguments.Count == arity;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public bool Equals(Symbol other) => clingo_symbol_is_equal_to(this, other);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="obj">An object to compare with this object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if ((obj == null) || (obj is not Symbol))
            {
                return false;
            }

            return Equals(obj as Symbol);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        /// <exception cref="ArgumentException"><c>obj</c> is not the same type as this instance.</exception>
        public int CompareTo(object obj)
        {
            if ((obj == null) || (obj is not Symbol))
            {
                throw new ArgumentException($"{obj.GetType().Name} object is null or not {this.GetType().Name} type");
            }

            return CompareTo(obj as Symbol);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(Symbol other)
        {
            if (clingo_symbol_is_less_than(this, other))
            {
                return -1;
            }

            if (clingo_symbol_is_less_than(other, this))
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => (int)(uint)clingo_symbol_hash(this);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            size_t[] str_size_c = new size_t[1];
            HandleError(clingo_symbol_to_string_size(m_symbol, str_size_c));
            StringBuilder str_c = new((int)(uint)str_size_c[0]);
            HandleError(clingo_symbol_to_string(m_symbol, str_c, str_size_c[0]));
            return str_c.ToString();
        }

        #endregion
    }
}