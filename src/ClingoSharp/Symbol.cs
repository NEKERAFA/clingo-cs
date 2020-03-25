using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Interfaces.Modules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClingoSharp
{
    /// <summary>
    /// Represents a gringo symbol.
    /// This includes numbers, strings, functions (including constants with <c>len(arguments) == 0</c> and tuples with <c>len(name) == 0</c>), <c>#inf</c> and <c>#sup</c>.
    /// </summary>
    public class Symbol : IEquatable<Symbol>, IComparable<Symbol>
    {
        #region Attributes

        private static readonly ISymbolModule m_module;
        internal readonly CoreServices.Types.Symbol m_clingoSymbol;

        #endregion

        #region Class properties

        /// <summary>
        /// Represents a symbol of type <see cref="SymbolType.Infimum"/>
        /// </summary>
        public static Symbol Infimum
        {
            get
            {
                m_module.CreateInfimum(out var symbol);
                return new Symbol(symbol);
            }
        }

        /// <summary>
        /// Represents a symbol of type <see cref="SymbolType.Supremum"/>
        /// </summary>
        public static Symbol Supremum
        {
            get
            {
                m_module.CreateSupremum(out var symbol);
                return new Symbol(symbol);
            }
        }

        #endregion

        #region Instance properties

        /// <summary>
        /// The arguments of a function.
        /// </summary>
        public List<Symbol> Arguments
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetArguments(m_clingoSymbol, out CoreServices.Types.Symbol[] arguments));
                return arguments.Select(argument => new Symbol(argument)).ToList();
            }
        }

        /// <summary>
        /// The name of a function.
        /// </summary>
        public string Name
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetName(m_clingoSymbol, out string name));
                return name;
            }
        }

        /// <summary>
        /// The inverted sign of a function.
        /// </summary>
        public bool IsNegative
        {
            get
            {
                Clingo.HandleClingoError(m_module.IsNegative(m_clingoSymbol, out bool negative));
                return negative;
            }
        }

        /// <summary>
        /// The value of a number.
        /// </summary>
        public int Number
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetNumber(m_clingoSymbol, out int number));
                return number;
            }
        }

        /// <summary>
        /// The sign of a function.
        /// </summary>
        public bool IsPositive
        {
            get
            {
                Clingo.HandleClingoError(m_module.IsPositive(m_clingoSymbol, out bool positive));
                return positive;
            }
        }

        /// <summary>
        /// The value of a string.
        /// </summary>
        public string String
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetString(m_clingoSymbol, out string value));
                return value;
            }
        }

        /// <summary>
        /// The type of the symbol.
        /// </summary>
        public SymbolType Type
        {
            get
            {
                var type = m_module.GetType(m_clingoSymbol);

                return type switch
                {
                    CoreServices.Enums.SymbolType.Infimun => SymbolType.Infimum,
                    CoreServices.Enums.SymbolType.Number => SymbolType.Number,
                    CoreServices.Enums.SymbolType.String => SymbolType.String,
                    CoreServices.Enums.SymbolType.Function => SymbolType.Function,
                    CoreServices.Enums.SymbolType.Supremum => SymbolType.Supremum,
                    _ => null
                };
            }
        }

        #endregion

        #region Constructors

        static Symbol()
        {
            m_module = Repository.GetModule<ISymbolModule>();
        }

        internal Symbol(CoreServices.Types.Symbol clingoSymbol)
        {
            m_clingoSymbol = clingoSymbol;
        }

        /// <summary>
        /// Construct a numeric symbol given a number.
        /// </summary>
        /// <param name="value">The given number</param>
        public Symbol(int value)
        {
            m_module.CreateNumber(value, out var symbol);
            m_clingoSymbol = symbol;
        }

        /// <summary>
        /// Construct a string symbol given a string.
        /// </summary>
        /// <param name="value">The given string</param>
        public Symbol(string value)
        {
            m_module.CreateString(value, out var symbol);
            m_clingoSymbol = symbol;
        }

        /// <summary>
        /// A shortcut for <c>Symbol("", arguments)</c>.
        /// </summary>
        /// <param name="arguments">The arguments in form of a list of symbols.</param>
        public Symbol(List<Symbol> arguments)
        {
            var symbols = arguments.Select(arg => arg.m_clingoSymbol).ToArray();
            m_module.CreateFunction("", symbols, false, out var symbol);
            m_clingoSymbol = symbol;
        }

        /// <summary>
        /// Construct a function symbol.
        /// </summary>
        /// This includes constants and tuples. Constants have an empty argument list and tuples have an empty name. Functions can represent classically negated atoms. Argument <paramref name="arguments"/> has to be set to false to represent such atoms.
        /// <param name="name">The name of the function (empty for tuples)</param>
        /// <param name="arguments">The arguments in form of a list of symbols</param>
        /// <param name="positive">The sign of the function (tuples must not have signs)</param>
        public Symbol(string name, List<Symbol> arguments = null, bool positive = true)
        {
            var symbols = arguments.Select(arg => arg.m_clingoSymbol).ToArray();
            m_module.CreateFunction(name, symbols, positive, out var symbol);
            m_clingoSymbol = symbol;
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Construct a numeric symbol given a number.
        /// </summary>
        /// <param name="value">The given number</param>
        public static implicit operator Symbol(int value)
        {
            m_module.CreateNumber(value, out var symbol);
            return new Symbol(symbol);
        }

        /// <summary>
        /// Construct a string symbol given a string.
        /// </summary>
        /// <param name="value">The given string</param>
        public static implicit operator Symbol(string value)
        {
            m_module.CreateString(value, out var symbol);
            return new Symbol(symbol);
        }

        public static bool operator <(Symbol a, Symbol b)
        {
            return m_module.IsLessThan(a.m_clingoSymbol, b.m_clingoSymbol);
        }

        public static bool operator <=(Symbol a, Symbol b)
        {
            return !m_module.IsLessThan(b.m_clingoSymbol, a.m_clingoSymbol);
        }

        public static bool operator >(Symbol a, Symbol b)
        {
            return m_module.IsLessThan(b.m_clingoSymbol, a.m_clingoSymbol);
        }

        public static bool operator >=(Symbol a, Symbol b)
        {
            return !m_module.IsLessThan(a.m_clingoSymbol, b.m_clingoSymbol);
        }

        public static bool operator ==(Symbol a, Symbol b)
        {
            return m_module.IsEqualTo(a.m_clingoSymbol, b.m_clingoSymbol);
        }

        public static bool operator !=(Symbol a, Symbol b)
        {
            return !m_module.IsEqualTo(a.m_clingoSymbol, b.m_clingoSymbol);
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Check if this is a function symbol with the given signature.
        /// </summary>
        /// <param name="name">The name of the function</param>
        /// <param name="arity">The arity of the function</param>
        /// <returns>Whether the function matches</returns>
        public bool Match(string name, int arity)
        {
            if (Type != SymbolType.Function) { return false; }
            if (!string.Equals(Name, name)) { return false; }
            if ((Arguments != null && arity != 0) || (Arguments.Count == arity)) { return false; }
            return true;
        }

        /// <inheritdoc/>
        public int CompareTo(Symbol other)
        {
            if (m_module.IsLessThan(m_clingoSymbol, other.m_clingoSymbol))
            {
                return -1;
            }
            
            if (m_module.IsLessThan(other.m_clingoSymbol, m_clingoSymbol))
            {
                return 1;
            }

            if (m_module.IsEqualTo(m_clingoSymbol, other.m_clingoSymbol))
            {
                return 0;
            }

            throw new InvalidOperationException($"cannot compare {this} with {other}");
        }

        /// <inheritdoc/>
        public bool Equals(Symbol other)
        {
            return m_module.IsEqualTo(m_clingoSymbol, other.m_clingoSymbol);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Symbol))
            {
                return false;
            }

            return Equals(obj as Symbol);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Convert.ToInt32(m_module.GetHash(m_clingoSymbol).ToUInt32());
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            Clingo.HandleClingoError(m_module.ToString(m_clingoSymbol, out string value));
            return value;
        }

        #endregion
    }
}
