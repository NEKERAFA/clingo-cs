using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.Interfaces.Modules;
using ClingoSharp.Exceptions;
using System;

namespace ClingoSharp
{
    public static class Clingo
    {
        private static string m_version = null;
        private static readonly IClingoModule m_module;

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

        static Clingo()
        {
            m_module = Repository.GetModule<IClingoModule>();
        }

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
    }
}
