using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Interfaces.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using ClingoModel = ClingoSharp.CoreServices.Components.Types.Model;
using ClingoSolveHandle = ClingoSharp.CoreServices.Components.Types.SolveHandle;
using ClingoSolveResult = ClingoSharp.CoreServices.Components.Enums.SolveResult;

namespace ClingoSharp
{
    /// <summary>
    /// Handle for solve calls.
    /// </summary>
    public sealed class SolveHandle : IEnumerable<Model>
    {
        #region Attributes

        private static readonly ISolveHandleModule m_module;
        private readonly ClingoSolveHandle m_clingoSolveHandle;

        #endregion

        #region Constructors

        static SolveHandle()
        {
            m_module = Repository.GetModule<ISolveHandleModule>();
        }

        public SolveHandle(ClingoSolveHandle clingoSolveHandle)
        {
            m_clingoSolveHandle = clingoSolveHandle;
        }

        #endregion

        #region Class Methods
        
        public static implicit operator ClingoSolveHandle(SolveHandle solveHandle)
        {
            return solveHandle.m_clingoSolveHandle;
        }

        public static implicit operator SolveHandle(ClingoSolveHandle clingoSolveHandle)
        {
            return new SolveHandle(clingoSolveHandle);
        }
        
        #endregion

        #region Instance Methods

        public void Cancel()
        {
            Clingo.HandleClingoError(m_module.Cancel(this));
        }

        public SolveResult Get()
        {
            Clingo.HandleClingoError(m_module.Get(this, out ClingoSolveResult result));

            bool? satisfiable = null;
            if ((result & ClingoSolveResult.Satisfiable) == ClingoSolveResult.Satisfiable)
            {
                satisfiable = true;
            } 
            else if ((result & ClingoSolveResult.Unsatisfiable) == ClingoSolveResult.Unsatisfiable)
            {
                satisfiable = false;
            }

            return new SolveResult()
            {
                IsSatisfiable = satisfiable,
                IsUnSatisfiable = satisfiable.HasValue ? !satisfiable : null,
                IsUnknown = !satisfiable.HasValue,
                IsExhausted = (result & ClingoSolveResult.Exhausted) == ClingoSolveResult.Exhausted,
                IsInterrupted = (result & ClingoSolveResult.Interrupted) == ClingoSolveResult.Interrupted
            };
        }

        public void Resume()
        {
            Clingo.HandleClingoError(m_module.Resume(this));
        }

        public bool Wait(float? timeout = null)
        {
            m_module.Wait(this, Convert.ToDouble(timeout.HasValue ? -timeout : 0.0f), out bool result);

            return result;
        }

        public IEnumerator<Model> GetEnumerator()
        {
            ClingoModel modelPtr;
            do
            {
                Clingo.HandleClingoError(m_module.Resume(this));
                Clingo.HandleClingoError(m_module.Model(this, out modelPtr));

                if (modelPtr.Object != IntPtr.Zero)
                {
                    yield return new Model(modelPtr);
                }
            }
            while (modelPtr.Object != IntPtr.Zero);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
