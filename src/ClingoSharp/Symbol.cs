using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Interfaces;
using ClingoSharp.CoreServices.Interfaces.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using ClingoSymbol = ClingoSharp.CoreServices.Types.Symbol;
using ClingoSymbolType = ClingoSharp.CoreServices.Enums.SymbolType;

namespace ClingoSharp
{
    /// <summary>
    /// Represents a gringo symbol.
    /// This includes numbers, strings, functions (including constants with <c>len(arguments) == 0</c> and tuples with <c>len(name) == 0</c>), <c>#inf</c> and <c>#sup</c>.
    /// </summary>
    public sealed class Symbol : IEquatable<Symbol>, IComparable<Symbol>
    {
        #region Attributes

        private static readonly ISymbolModule m_module;
        private readonly ClingoSymbol m_clingoSymbol;

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
                Clingo.HandleClingoError(m_module.GetArguments(this, out ClingoSymbol[] arguments));
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
                Clingo.HandleClingoError(m_module.GetName(this, out string name));
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
                Clingo.HandleClingoError(m_module.IsNegative(this, out bool negative));
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
                Clingo.HandleClingoError(m_module.GetNumber(this, out int number));
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
                Clingo.HandleClingoError(m_module.IsPositive(this, out bool positive));
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
                Clingo.HandleClingoError(m_module.GetString(this, out string value));
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
                var type = m_module.GetType(this);

                return type switch
                {
                    ClingoSymbolType.Infimum => SymbolType.Infimum,
                    ClingoSymbolType.Number => SymbolType.Number,
                    ClingoSymbolType.String => SymbolType.String,
                    ClingoSymbolType.Function => SymbolType.Function,
                    ClingoSymbolType.Supremum => SymbolType.Supremum,
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

        public Symbol(ClingoSymbol clingoSymbol)
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
        /// Gets the asociated API module in clingo
        /// </summary>
        /// <returns>The asociated module</returns>
        public static IClingoModule GetModule()
        {
            return m_module;
        }

        /// <summary>
        /// Gets the symbol module in clingo
        /// </summary>
        /// <returns>The symbol module</returns>
        public static ISymbolModule GetSymbolModule()
        {
            return m_module;
        }

        public static implicit operator ClingoSymbol(Symbol symbol)
        {
            return symbol.m_clingoSymbol;
        }

        public static implicit operator Symbol(ClingoSymbol clingoSymbol)
        {
            return new Symbol(clingoSymbol);
        }

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
            return m_module.IsLessThan(a, b);
        }

        public static bool operator <=(Symbol a, Symbol b)
        {
            return !m_module.IsLessThan(b, a);
        }

        public static bool operator >(Symbol a, Symbol b)
        {
            return m_module.IsLessThan(b, a);
        }

        public static bool operator >=(Symbol a, Symbol b)
        {
            return !m_module.IsLessThan(a, b);
        }

        public static bool operator ==(Symbol a, Symbol b)
        {
            return m_module.IsEqualTo(a, b);
        }

        public static bool operator !=(Symbol a, Symbol b)
        {
            return !m_module.IsEqualTo(a, b);
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
            if (m_module.IsLessThan(this, other))
            {
                return -1;
            }
            
            if (m_module.IsLessThan(other, this))
            {
                return 1;
            }

            if (m_module.IsEqualTo(this, other))
            {
                return 0;
            }

            throw new InvalidOperationException($"cannot compare {this} with {other}");
        }

        /// <inheritdoc/>
        public bool Equals(Symbol other)
        {
            return m_module.IsEqualTo(this, other);
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
            return Convert.ToInt32(m_module.GetHash(this).ToUInt32());
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            Clingo.HandleClingoError(m_module.ToString(this, out string value));
            return value;
        }

        #endregion
    }
}
