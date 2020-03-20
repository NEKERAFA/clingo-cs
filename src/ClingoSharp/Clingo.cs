using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Enums;
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
        private static readonly IClingoModule m_module;

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
            m_module = Repository.GetModule<IClingoModule>();
        }

        #endregion

        #region Class Methods

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

        #endregion
    }
}
