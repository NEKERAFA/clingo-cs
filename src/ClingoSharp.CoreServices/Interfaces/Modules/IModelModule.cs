using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.Types;
using System;

namespace ClingoSharp.CoreServices.Interfaces.Modules
{
    public interface IModelModule : IModule
    {
        /// <summary>
        /// Constant time lookup to test whether an atom is in a model.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="atom">the atom to lookup</param>
        /// <param name="contained">whether the atom is contained</param>
        /// <returns>true if the function is success, false otherwise</returns>
        bool Contains(IntPtr model, Symbol atom, out bool contained);

        /// <summary>
        /// Add symbols to the model.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="atoms">the symbols to add</param>
        /// <returns>true if the function is success, false otherwise</returns>
        bool Extends(IntPtr model, Symbol[] atoms);

        /// <summary>
        /// Get the type of the model.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="type">the type of the model</param>
        /// <returns>true if the function is success, false otherwise</returns>
        bool GetType(IntPtr model, out ModelType type);

        /// <summary>
        /// Check if a program literal is true in a model.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="literal">the literal to lookup</param>
        /// <param name="result">whether the literal is true</param>
        /// <returns>true if the function is success, false otherwise</returns>
        bool IsTrue(IntPtr model, Literal literal, out bool result);

        /// <summary>
        /// Get the running number of the model.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="number">the number of the model</param>
        /// <returns>true if the function is success, false otherwise</returns>
        bool GetNumber(IntPtr model, out ulong number);

        /// <summary>
        /// Whether the optimality of a model has been proven.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="proven">whether the optimality has been proven</param>
        /// <returns>true if the function is success, false otherwise</returns>
        bool IsOptimalityProven(IntPtr model, out bool proven);

        /// <summary>
        /// Get the id of the solver thread that found the model.
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="id">the resulting thread id</param>
        /// <returns>true if the function is success, false otherwise</returns>
        bool GetThreadId(IntPtr model, out uint id);

        /// <summary>
        /// Get the cost vector of a model
        /// </summary>
        /// <param name="model">a model object</param>
        /// <param name="costs">the resulting costs</param>
        /// <returns>true if the function is success, false otherwise</returns>
        bool GetCosts(IntPtr model, out long[] costs);

        /// <summary>
        /// Get the symbols of the selected types in the model.
        /// </summary>
        /// <remarks>
        /// CSP assignments are represented using functions with name "$" where the first argument is the name of the CSP variable and the second one its value.
        /// </remarks>
        /// <param name="model">a model object</param>
        /// <param name="showType">which symbols to select</param>
        /// <param name="symbols">the resulting symbols</param>
        /// <returns>true if the function is success, false otherwise</returns>
        bool GetSymbols(IntPtr model, ShowType showType, out Symbol[] symbols);

        /// <summary>
        /// Get the associated solve control object of a model.
        /// </summary>
        /// This object allows for adding clauses during model enumeration.
        /// <param name="model">a model object</param>
        /// <param name="context">the resulting solve control object</param>
        /// <returns>true if the function is success, false otherwise</returns>
        bool GetContext(IntPtr model, out IntPtr context);
    }
}
