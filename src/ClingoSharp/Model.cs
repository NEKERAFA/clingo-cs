using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.Interfaces.Modules;
using ClingoSharp.CoreServices.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClingoSharp
{
    /// <summary>
    /// Provides access to a model during a solve call and provides a <see cref="SolveControl"/> object to provided limited support to influence the running search.
    /// </summary>
    public class Model
    {
        #region Attributes

        private static readonly IModelModule m_module;
        private readonly CoreServices.Types.Model m_clingoModel;

        #endregion

        #region Properties

        /// <summary>
        /// Object that allows for controlling the running search.
        /// </summary>
        public SolveControl Context
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetContext(m_clingoModel, out CoreServices.Types.SolveControl context));
                return new SolveControl(context);
            }
        }

        /// <summary>
        /// Return the list of integer cost values of the model.
        /// </summary>
        /// The return values correspond to clasp's cost output.
        public List<int> Cost 
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetCosts(m_clingoModel, out long[] costs));
                return costs.Select(cost => Convert.ToInt32(cost)).ToList();
            }
        }

        /// <summary>
        /// The running number of the model.
        /// </summary>
        public int Number
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetNumber(m_clingoModel, out ulong number));
                return Convert.ToInt32(number);
            }
        }

        /// <summary>
        /// Whether the optimality of the model has been proven.
        /// </summary>
        public bool OptimalityProven
        {
            get
            {
                Clingo.HandleClingoError(m_module.IsOptimalityProven(m_clingoModel, out bool proven));
                return proven;
            }
        }

        /// <summary>
        /// The id of the thread which found the model.
        /// </summary>
        public int ThreadId
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetThreadId(m_clingoModel, out uint id));
                return Convert.ToInt32(id);
            }
        }

        /// <summary>
        /// The type of the model.
        /// </summary>
        public ModelType Type
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetType(m_clingoModel, out var type));

                return type switch
                {
                    CoreServices.Enums.ModelType.StableModel => ModelType.StableModel,
                    CoreServices.Enums.ModelType.BraveConsequences => ModelType.BraveConsequences,
                    CoreServices.Enums.ModelType.CautiousConsequences => ModelType.CautiousConsequences,
                    _ => null
                };
            }
        }

        #endregion

        #region Constructors

        static Model()
        {
            m_module = Repository.GetModule<IModelModule>();
        }

        internal Model(CoreServices.Types.Model clingoModel)
        {
            m_clingoModel = clingoModel;
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Efficiently check if an atom is contained in the model.
        /// </summary>
        /// <param name="atom">The atom to lookup</param>
        /// <returns>Whether the given atom is contained in the model.</returns>
        /// <remarks>The atom must be represented using a function symbol.</remarks>
        public bool Contains(Symbol atom)
        {
            Clingo.HandleClingoError(m_module.Contains(m_clingoModel, atom.m_clingoSymbol, out bool contained));
            return contained;
        }

        /// <summary>
        /// Extend a model with the given symbols.
        /// </summary>
        /// <param name="symbols">The symbols to add to the model.</param>
        public void Extend(List<Symbol> symbols)
        {
            var atoms = symbols.Select(symbol => symbol.m_clingoSymbol).ToArray();
            Clingo.HandleClingoError(m_module.Extends(m_clingoModel, atoms));
        }

        /// <summary>
        /// Check if the given program literal is true.
        /// </summary>
        /// <param name="literal">The given program literal.</param>
        /// <returns>Whether the given program literal is true.</returns>
        public bool IsTrue(int literal)
        {
            var literalValue = new Literal()
            {
                Value = literal
            };

            Clingo.HandleClingoError(m_module.IsTrue(m_clingoModel, literalValue, out bool result));

            return result;
        }

        /// <summary>
        /// Return the list of atoms, terms, or CSP assignments in the model.
        /// </summary>
        /// <param name="atoms">Select all atoms in the model (independent of <c>#show</c> statements).</param>
        /// <param name="terms">Select all terms displayed with <c>#show</c> statements in the model.</param>
        /// <param name="shown">Select all atoms and terms as outputted by clingo.</param>
        /// <param name="csp">Select all csp assignments (independent of <c>#show</c> statements).</param>
        /// <param name="complement">Return the complement of the answer set w.r.t. to the atoms known to the grounder. (Does not affect csp assignments.)</param>
        /// <returns>The selected symbols.</returns>
        public List<Symbol> GetSymbols(bool atoms = false, bool terms = false, bool shown = false, bool csp = false, bool complement = false)
        {
            ShowType atomset = 0;
            if (atoms) { atomset |= ShowType.Atoms; }
            if (terms) { atomset |= ShowType.Terms; }
            if (shown) { atomset |= ShowType.Shown; }
            if (csp) { atomset |= ShowType.CSP; }
            if (complement) { atomset |= ShowType.Complement; }

            Clingo.HandleClingoError(m_module.GetSymbols(m_clingoModel, atomset, out var symbols));

            return symbols.Select(symbol => new Symbol(symbol)).ToList();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            List<Symbol> ret = GetSymbols(shown: true);

            bool comma = false;
            foreach (Symbol symbol in ret)
            {
                if (comma)
                {
                    sb.Append(" ");
                }
                else
                {
                    comma = true;
                }

                if (symbol.Type == SymbolType.Function)
                {
                    string name = symbol.Name;
                    List<Symbol> args = symbol.Arguments;
                    if (args.Count == 2 && name.Equals("$"))
                    {
                        sb.Append($"{args[0]}={args[1]}");
                    }
                    else
                    {
                        sb.Append($"{symbol}");
                    }
                }
                else
                {
                    sb.Append($"{symbol}");
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}
