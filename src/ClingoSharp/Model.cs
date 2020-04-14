using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Interfaces.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClingoId = ClingoSharp.CoreServices.Types.Id;
using ClingoLiteral = ClingoSharp.CoreServices.Types.Literal;
using ClingoModel = ClingoSharp.CoreServices.Types.Model;
using ClingoModelType = ClingoSharp.CoreServices.Enums.ModelType;
using ClingoShowType = ClingoSharp.CoreServices.Enums.ShowType;
using ClingoSymbol = ClingoSharp.CoreServices.Types.Symbol;
using ClingoSolveControl = ClingoSharp.CoreServices.Types.SolveControl;

namespace ClingoSharp
{
    /// <summary>
    /// Provides access to a model during a solve call and provides a <see cref="SolveControl"/> object to provided limited support to influence the running search.
    /// </summary>
    public sealed class Model
    {
        #region Attributes

        private static readonly IModelModule m_module;
        private readonly ClingoModel m_clingoModel;

        #endregion

        #region Properties

        /// <summary>
        /// Object that allows for controlling the running search.
        /// </summary>
        public SolveControl Context
        {
            get
            {
                Clingo.HandleClingoError(m_module.GetContext(this, out ClingoSolveControl context));
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
                Clingo.HandleClingoError(m_module.GetCosts(this, out long[] costs));
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
                Clingo.HandleClingoError(m_module.GetNumber(this, out ulong number));
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
                Clingo.HandleClingoError(m_module.IsOptimalityProven(this, out bool proven));
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
                Clingo.HandleClingoError(m_module.GetThreadId(this, out ClingoId id));
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
                Clingo.HandleClingoError(m_module.GetType(this, out var type));

                return type switch
                {
                    ClingoModelType.StableModel => ModelType.StableModel,
                    ClingoModelType.BraveConsequences => ModelType.BraveConsequences,
                    ClingoModelType.CautiousConsequences => ModelType.CautiousConsequences,
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

        public Model(ClingoModel clingoModel)
        {
            m_clingoModel = clingoModel;
        }

        #endregion

        #region Class Methods
        
        public static implicit operator ClingoModel(Model model)
        {
            return model.m_clingoModel;
        }

        public static implicit operator Model(ClingoModel clingoModel)
        {
            return new Model(clingoModel);
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
            Clingo.HandleClingoError(m_module.Contains(this, atom, out bool contained));
            return contained;
        }

        /// <summary>
        /// Extend a model with the given symbols.
        /// </summary>
        /// <param name="symbols">The symbols to add to the model.</param>
        public void Extend(List<Symbol> symbols)
        {
            var atoms = symbols.Select(symbol => (ClingoSymbol)symbol).ToArray();
            Clingo.HandleClingoError(m_module.Extends(this, atoms));
        }

        /// <summary>
        /// Check if the given program literal is true.
        /// </summary>
        /// <param name="literal">The given program literal.</param>
        /// <returns>Whether the given program literal is true.</returns>
        public bool IsTrue(int literal)
        {
            var literalValue = new ClingoLiteral()
            {
                Value = literal
            };

            Clingo.HandleClingoError(m_module.IsTrue(this, literalValue, out bool result));

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
            ClingoShowType atomset = 0;
            if (atoms) { atomset |= ClingoShowType.Atoms; }
            if (terms) { atomset |= ClingoShowType.Terms; }
            if (shown) { atomset |= ClingoShowType.Shown; }
            if (csp) { atomset |= ClingoShowType.CSP; }
            if (complement) { atomset |= ClingoShowType.Complement; }

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
