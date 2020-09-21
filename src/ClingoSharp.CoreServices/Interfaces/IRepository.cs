using ClingoSharp.NativeWrapper.Interfaces;
using System;

namespace ClingoSharp.CoreServices.Interfaces
{
    /// <summary>
    /// This interface represents a repository of clingo modules
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets a clingo module
        /// </summary>
        /// <param name="moduleType">A module type</param>
        /// <returns>The module implementation</returns>
        IClingoModule GetModule(Type moduleType);

        /// <summary>
        /// Gets a clingo module
        /// </summary>
        /// <typeparam name="T">A module type</typeparam>
        /// <returns>The module implementation</returns>
        T GetModule<T>() where T : IClingoModule;
    }
}
