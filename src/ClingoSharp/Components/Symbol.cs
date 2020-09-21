using ClingoSharp.NativeWrapper.Interfaces.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using static ClingoSharp.NativeWrapper.Enums.SymbolType;

namespace ClingoSharp
{
    /// <summary>
    /// Represents a gringo symbol.
    /// This includes numbers, strings, functions (including constants with <c>len(arguments) == 0</c> and tuples with <c>len(name) == 0</c>), <c>#inf</c> and <c>#sup</c>.
    /// </summary>
    public sealed class Symbol : IEquatable<Symbol>, IComparable<Symbol>
    {
        #region Attributes

        private static ISymbol m_symbolModule = null;

        private readonly ulong m_clingoSymbol;

        #endregion

        #region Class properties

        /// <summary>
        /// Represents a symbol of type <see cref="SymbolType.Infimum"/>
        /// </summary>
        public static Symbol Infimum
        {
            get
            {
                SymbolModule.CreateInfimum(out ulong symbol);
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
                SymbolModule.CreateSupremum(out ulong symbol);
                return new Symbol(symbol);
            }
        }

        public static ISymbol SymbolModule
        {
            get
            {
                if (m_symbolModule == null)
                    m_symbolModule = Clingo.ClingoRepository.GetModule<ISymbol>();

                return m_symbolModule;
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
                Clingo.HandleClingoError(SymbolModule.GetArguments(m_clingoSymbol, out ulong[] arguments));
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
                Clingo.HandleClingoError(SymbolModule.GetName(m_clingoSymbol, out string name));
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
                Clingo.HandleClingoError(SymbolModule.IsNegative(m_clingoSymbol, out bool negative));
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
                Clingo.HandleClingoError(SymbolModule.GetNumber(m_clingoSymbol, out int number));
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
                Clingo.HandleClingoError(SymbolModule.IsPositive(m_clingoSymbol, out bool positive));
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
                Clingo.HandleClingoError(SymbolModule.GetString(m_clingoSymbol, out string value));
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
                var type = SymbolModule.GetType(m_clingoSymbol);

                switch (type)
                {
                    case clingo_symbol_type_infimum:
                        return SymbolType.Infimum;
                    case clingo_symbol_type_number:
                        return SymbolType.Number;
                    case clingo_symbol_type_string:
                        return SymbolType.String;
                    case clingo_symbol_type_function:
                        return SymbolType.Function;
                    case clingo_symbol_type_supremum:
                        return SymbolType.Supremum;
                    default:
                        return null;
                };
            }
        }

        #endregion

        #region Constructors

        public Symbol(ulong clingoSymbol)
        {
            m_clingoSymbol = clingoSymbol;
        }

        /// <summary>
        /// Construct a numeric symbol given a number.
        /// </summary>
        /// <param name="value">The given number</param>
        public Symbol(int value)
        {
            SymbolModule.CreateNumber(value, out ulong symbol);
            m_clingoSymbol = symbol;
        }

        /// <summary>
        /// Construct a string symbol given a string.
        /// </summary>
        /// <param name="value">The given string</param>
        public Symbol(string value)
        {
            SymbolModule.CreateString(value, out ulong symbol);
            m_clingoSymbol = symbol;
        }

        /// <summary>
        /// A shortcut for <c>Symbol("", arguments)</c>.
        /// </summary>
        /// <param name="arguments">The arguments in form of a list of symbols.</param>
        public Symbol(List<Symbol> arguments)
        {
            var symbols = arguments.Select(arg => arg.m_clingoSymbol).ToArray();
            SymbolModule.CreateFunction("", symbols, false, out ulong symbol);
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
            SymbolModule.CreateFunction(name, symbols, positive, out ulong symbol);
            m_clingoSymbol = symbol;
        }

        #endregion

        #region Class Methods

        public static implicit operator ulong(Symbol symbol)
        {
            return symbol.m_clingoSymbol;
        }

        public static implicit operator Symbol(ulong clingoSymbol)
        {
            return new Symbol(clingoSymbol);
        }

        /// <summary>
        /// Construct a numeric symbol given a number.
        /// </summary>
        /// <param name="value">The given number</param>
        public static implicit operator Symbol(int value)
        {
            SymbolModule.CreateNumber(value, out ulong symbol);
            return new Symbol(symbol);
        }

        /// <summary>
        /// Construct a string symbol given a string.
        /// </summary>
        /// <param name="value">The given string</param>
        public static implicit operator Symbol(string value)
        {
            SymbolModule.CreateString(value, out ulong symbol);
            return new Symbol(symbol);
        }

        public static bool operator <(Symbol a, Symbol b)
        {
            return SymbolModule.IsLessThan(a, b);
        }

        public static bool operator <=(Symbol a, Symbol b)
        {
            return !SymbolModule.IsLessThan(b, a);
        }

        public static bool operator >(Symbol a, Symbol b)
        {
            return SymbolModule.IsLessThan(b, a);
        }

        public static bool operator >=(Symbol a, Symbol b)
        {
            return !SymbolModule.IsLessThan(a, b);
        }

        public static bool operator ==(Symbol a, Symbol b)
        {
            return SymbolModule.IsEqualTo(a, b);
        }

        public static bool operator !=(Symbol a, Symbol b)
        {
            return !SymbolModule.IsEqualTo(a, b);
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
            if (SymbolModule.IsLessThan(this, other))
            {
                return -1;
            }
            
            if (SymbolModule.IsLessThan(other, this))
            {
                return 1;
            }

            if (SymbolModule.IsEqualTo(this, other))
            {
                return 0;
            }

            throw new InvalidOperationException($"cannot compare {this} with {other}");
        }

        /// <inheritdoc/>
        public bool Equals(Symbol other)
        {
            return SymbolModule.IsEqualTo(this, other);
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
            return Convert.ToInt32(SymbolModule.GetHash(this).ToUInt32());
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            Clingo.HandleClingoError(SymbolModule.ToString(this, out string value));
            return value;
        }

        #endregion
    }
}
