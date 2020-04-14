using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.Types;

namespace ClingoSharp.CoreServices.Interfaces.Modules
{
    /// <summary>
    /// Inspection of models and a high-level interface to add constraints during solving.
    /// </summary>
    public interface IModelModule : Interfaces.IClingoModule
    {
        #region Functions for Inspecting Models

        /// <summary>
        /// Constant time lookup to test whether an atom is in a model.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="atom">the atom to lookup</param>
        /// <param name="contained">whether the atom is contained</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool Contains(Model model, Symbol atom, out bool contained);

        /// <summary>
        /// Add symbols to the model.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="atoms">the symbols to add</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool Extends(Model model, Symbol[] atoms);

        /// <summary>
        /// Get the type of the model.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="type">the type of the model</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetType(Model model, out ModelType type);

        /// <summary>
        /// Check if a program literal is true in a model.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="literal">the literal to lookup</param>
        /// <param name="result">whether the literal is true</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool IsTrue(Model model, Literal literal, out bool result);

        /// <summary>
        /// Get the running number of the model.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="number">the number of the model</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetNumber(Model model, out ulong number);

        /// <summary>
        /// Whether the optimality of a model has been proven.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="proven">whether the optimality has been proven</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool IsOptimalityProven(Model model, out bool proven);

        /// <summary>
        /// Get the id of the solver thread that found the model.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="id">the resulting thread id</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetThreadId(Model model, out Id id);

        /// <summary>
        /// Get the cost vector of a model
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="costs">the resulting costs</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetCosts(Model model, out long[] costs);

        /// <summary>
        /// Get the symbols of the selected types in the model.
        /// </summary>
        /// <remarks>
        /// CSP assignments are represented using functions with name <c>$</c> where the first argument is the name of the CSP variable and the second one its value.
        /// </remarks>
        /// <param name="model">a model object</param>
        /// <param name="showType">which symbols to select</param>
        /// <param name="symbols">the resulting symbols</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetSymbols(Model model, ShowType showType, out Symbol[] symbols);

        #endregion

        #region Functions for Adding Clauses

        /// <summary>
        /// Get the associated solve control object of a model.
        /// This object allows for adding clauses during model enumeration.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="context">the resulting solve control object</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetContext(Model model, out SolveControl context);

        /// <summary>
        /// Get an object to inspect the symbolic atoms.
        /// </summary>
        /// <param name="control">the target</param>
        /// <param name="atoms">the resulting object</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool GetSymbolicAtoms(SolveControl control, out SymbolicAtoms atoms);

        /// <summary>
        /// Add a clause that applies to the current solving step during model enumeration.
        /// </summary>
        /// <remarks>
        /// The Theory Propagation module provides a more sophisticated interface to add clauses - even on partial assignments.
        /// </remarks>
        /// <param name="control">the target</param>
        /// <param name="clause">array of literals representing the clause</param>
        /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
        bool AddClause(SolveControl control, Literal[] clause);

        #endregion
    }
}
