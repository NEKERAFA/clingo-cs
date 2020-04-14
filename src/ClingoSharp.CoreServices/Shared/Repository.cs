using ClingoSharp.CoreServices.Interfaces;
using ClingoSharp.CoreServices.Shared;
using System;

namespace ClingoSharp.CoreServices
{
    /// <summary>
    /// This class represents a repository of clingo modules
    /// </summary>
    public static class Repository
    {
        #region Attributes

        private static readonly IClingoContext m_clingoContext;

        #endregion

        #region Constructors

        /// <summary>
        /// Loads the main context
        /// </summary>
        static Repository()
        {
            m_clingoContext = new ClingoContext();
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Gets a clingo module
        /// </summary>
        /// <typeparam name="T">A module type</typeparam>
        /// <returns>The module implementation</returns>
        public static T GetModule<T>() where T : IClingoModule
        {
            return m_clingoContext.GetModule<T>();
        }

        /// <summary>
        /// Gets a clingo module
        /// </summary>
        /// <param name="moduleType">A module type</param>
        /// <returns>The module implementation</returns>
        public static IClingoModule GetModule(Type moduleType)
        {
            return m_clingoContext.GetModule(moduleType);
        }

        #endregion
    }
}
