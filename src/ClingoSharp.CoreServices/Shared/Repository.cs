using ClingoSharp.CoreServices.Interfaces;
using ClingoSharp.CoreServices.Shared;
using ClingoSharp.NativeWrapper.Interfaces;
using System;
using System.Collections.Generic;

namespace ClingoSharp.CoreServices
{
    /// <summary>
    /// This class represents a repository of clingo modules
    /// </summary>
    public sealed class Repository : IRepository
    {
        #region Attributes

        private readonly IClingoContext m_clingoContext;
        private readonly IDictionary<string, IClingoModule> m_clingoModules;

        #endregion

        #region Constructors

        /// <summary>
        /// Loads the main context
        /// </summary>
        public Repository(string currentPath)
        {
            m_clingoContext = new ClingoContext(currentPath);
            m_clingoModules = new Dictionary<string, IClingoModule>();
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Gets a clingo module
        /// </summary>
        /// <param name="moduleType">A module type</param>
        /// <returns>The module implementation</returns>
        public IClingoModule GetModule(Type moduleType)
        {
            if (!m_clingoModules.TryGetValue(moduleType.Name, out IClingoModule module))
            {
                module = m_clingoContext.GetModule(moduleType);
                m_clingoModules.Add(moduleType.Name, module);
            }

            return module;
        }

        /// <summary>
        /// Gets a clingo module
        /// </summary>
        /// <typeparam name="T">A module type</typeparam>
        /// <returns>The module implementation</returns>
        public T GetModule<T>() where T : IClingoModule
        {
            return (T)GetModule(typeof(T));
        }

        #endregion
    }
}
