using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Interfaces;
using ClingoSharp.Exceptions;
using ClingoSharp.NativeWrapper.Enums;
using ClingoSharp.NativeWrapper.Interfaces.Modules;
using System;
using System.IO;
using System.Reflection;
using static ClingoSharp.NativeWrapper.Enums.ErrorCode;
using static ClingoSharp.NativeWrapper.Enums.WarningCode;

namespace ClingoSharp
{
    /// <summary>
    /// The clingo-5.4.0 module.
    /// This module provides functions and classes to control the grounding and solving process.
    /// </summary>
    public sealed class Clingo
    {
        #region Attributes

        private static string m_workingPath;
        private static IRepository m_clingoRepository = null;
        private static IClingo m_clingoModule;

        private string m_version;

        #endregion

        #region Properties

        /// <summary>
        /// Version of the clingo module (<c>'5.4.0'</c>)
        /// </summary>
        public string Version
        {
            get
            {
                if (m_version == null)
                {
                    m_version = m_clingoModule.GetVersion();
                }

                return m_version;
            }
        }

        internal static IRepository ClingoRepository
        {
            get
            {
                if (m_clingoRepository == null)
                     m_clingoRepository = new Repository(m_workingPath);

                return m_clingoRepository;
            }
        }

        internal static IClingo ClingoModule
        {
            get
            {
                if (m_clingoModule == null)
                    m_clingoModule = ClingoRepository.GetModule<IClingo>();

                return m_clingoModule;
            }
        }

        #endregion

        #region Constructors

        public Clingo(string workingPath = null)
        {
            if (workingPath == null)
                m_workingPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath);
            else
                m_workingPath = workingPath;
            m_clingoModule = ClingoRepository.GetModule<IClingo>();
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
                ErrorCode code = m_clingoModule.GetErrorCode();
                string message = m_clingoModule.GetErrorMessage();
                if (message == null) { message = "no message"; }

                switch(code)
                {
                    case clingo_error_runtime:
                        throw new RuntimeException(message);
                    case clingo_error_logic:
                        throw new LogicException(message);
                    case clingo_error_bad_alloc:
                        throw new BadAllocationException(message);
                    case clingo_error_unknown:
                        throw new UnknownException(message);
                    case clingo_error_success:
                        throw new Exception(message);
                }
            }
        }

        internal static void HandleClingoWarning(WarningCode code, string message)
        {
            switch(code)
            {
                case clingo_warning_operation_undefined:
                    throw new OperationUndefinedException(message);
                case clingo_warning_runtime_error:
                    throw new RuntimeException(message);
                case clingo_warning_atom_undefined:
                    throw new AtomUndefinedException(message);
                case clingo_warning_file_included:
                    throw new FileIncludedException(message);
                case clingo_warning_variable_unbounded:
                    throw new VariableUnboundedException(message);
                case clingo_warning_global_variable:
                    throw new GlobalVariableException(message);
                case clingo_warning_other:
                    throw new UnknownException(message);
            }
        }

        #endregion
    }
}
