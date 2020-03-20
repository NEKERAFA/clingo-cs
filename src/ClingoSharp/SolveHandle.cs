using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Interfaces.Modules;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ClingoSharp
{
    /// <summary>
    /// Handle for solve calls.
    /// </summary>
    public class SolveHandle : IEnumerable<Model>
    {
        #region Attributes

        private static readonly ISolveHandleModule m_module;
        private readonly IntPtr m_clingoSolveHandle;

        #endregion

        #region Constructors

        static SolveHandle()
        {
            m_module = Repository.GetModule<ISolveHandleModule>();
        }

        internal SolveHandle(IntPtr clingoSolveHandle)
        {
            m_clingoSolveHandle = clingoSolveHandle;
        }

        #endregion

        #region Methods

        public void Cancel()
        {
            Clingo.HandleClingoError(m_module.Cancel(m_clingoSolveHandle));
        }

        public SolveResult Get()
        {
            Clingo.HandleClingoError(m_module.Get(m_clingoSolveHandle, out CoreServices.Enums.SolveResult result));

            bool? satisfiable = null;
            if (result.HasFlag(CoreServices.Enums.SolveResult.Satisfiable))
            {
                satisfiable = true;
            } 
            else if (result.HasFlag(CoreServices.Enums.SolveResult.Unsatisfiable))
            {
                satisfiable = false;
            }

            return new SolveResult()
            {
                IsSatisfiable = satisfiable,
                IsUnSatisfiable = satisfiable.HasValue ? !satisfiable : null,
                IsUnknown = !satisfiable.HasValue,
                IsExhausted = result.HasFlag(CoreServices.Enums.SolveResult.Exhausted),
                IsInterrupted = result.HasFlag(CoreServices.Enums.SolveResult.Interrupted)
            };
        }

        public void Resume()
        {
            Clingo.HandleClingoError(m_module.Resume(m_clingoSolveHandle));
        }

        public bool Wait(float? timeout = null)
        {
            m_module.Wait(m_clingoSolveHandle, Convert.ToDouble(timeout.HasValue ? -timeout : 0.0f), out bool result);

            return result;
        }

        public IEnumerator<Model> GetEnumerator()
        {
            IntPtr modelPtr;
            do
            {
                Clingo.HandleClingoError(m_module.Resume(m_clingoSolveHandle));
                Clingo.HandleClingoError(m_module.Model(m_clingoSolveHandle, out modelPtr));

                if (modelPtr != IntPtr.Zero)
                {
                    yield return new Model(modelPtr);
                }
            }
            while (modelPtr != IntPtr.Zero);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
