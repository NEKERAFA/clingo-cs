using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.Interfaces;
using ClingoSharp.CoreServices.Interfaces.Modules;
using ClingoSharp.Exceptions;
using System;

namespace ClingoSharp
{
    /// <summary>
    /// The clingo-5.4.0 module.
    /// This module provides functions and classes to control the grounding and solving process.
    /// </summary>
    public static class Clingo
    {
        #region Attributes

        private static string m_version = null;
        private static readonly IMainModule m_module;

        #endregion

        #region Properties

        /// <summary>
        /// Version of the clingo module (<c>'5.4.0'</c>)
        /// </summary>
        public static string Version
        {
            get
            {
                if (m_version == null)
                {
                    m_version = m_module.GetVersion();
                }

                return m_version;
            }
        }

        #endregion

        #region Constructors

        static Clingo()
        {
            m_module = Repository.GetModule<IMainModule>();
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Gets the error message and the code that returns clingo and creates a new exception
        /// </summary>
        /// <param name="success"><c>false</c> to check clingo error</param>
        /// <exception cref="RuntimeException"></exception>
        /// <exception cref="LogicException"></exception>
        /// <exception cref="BadAllocationException"></exception>
        /// <exception cref="UnknownException"></exception>
        /// <exception cref="Exception"></exception>
        internal static void HandleClingoError(bool success)
        {
            if (!success)
            {
                string message = m_module.GetErrorMessage();
                if (message == null) { message = "no message"; }

                switch(m_module.GetErrorCode())
                {
                    case ErrorCode.RuntimeError:
                        throw new RuntimeException(message);
                    case ErrorCode.LogicError:
                        throw new LogicException(message);
                    case ErrorCode.BadAlloc:
                        throw new BadAllocationException(message);
                    case ErrorCode.Unknown:
                        throw new UnknownException(message);
                    case ErrorCode.Success:
                        throw new Exception(message);
                }
            }
        }

        /// <summary>
        /// Gets the asociated API module in clingo
        /// </summary>
        /// <returns>The asociated module</returns>
        public static IClingoModule GetModule()
        {
            return m_module;
        }

        /// <summary>
        /// Gets the main module in clingo
        /// </summary>
        /// <returns>The the main module</returns>
        public static IMainModule GetMainModule()
        {
            return m_module;
        }

        #endregion
    }
}
