using ClingoSharp.NativeWrapper.Interfaces.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using static ClingoSharp.NativeWrapper.Enums.SolveResult;

namespace ClingoSharp
{
    /// <summary>
    /// Handle for solve calls.
    /// </summary>
    public sealed class SolveHandle : IEnumerable<Model>
    {
        #region Attributes

        private static ISolveHandle m_solveHandleModule = null;

        private readonly IntPtr m_clingoSolveHandle;

        #endregion

        #region Class Properties

        public static ISolveHandle SolveHandleModule
        {
            get
            {
                if (m_solveHandleModule == null)
                    m_solveHandleModule = Clingo.ClingoRepository.GetModule<ISolveHandle>();

                return m_solveHandleModule;
            }
        }

        #endregion

        #region Constructors

        public SolveHandle(IntPtr clingoSolveHandle)
        {
            m_clingoSolveHandle = clingoSolveHandle;
        }

        #endregion

        #region Class Methods
        
        public static implicit operator IntPtr(SolveHandle solveHandle)
        {
            return solveHandle.m_clingoSolveHandle;
        }

        public static implicit operator SolveHandle(IntPtr clingoSolveHandle)
        {
            return new SolveHandle(clingoSolveHandle);
        }
        
        #endregion

        #region Instance Methods

        public void Cancel()
        {
            Clingo.HandleClingoError(SolveHandleModule.Cancel(this));
        }

        public SolveResult Get()
        {
            Clingo.HandleClingoError(SolveHandleModule.Get(this, out var result));

            bool? satisfiable = null;
            if ((result & clingo_solve_result_satisfiable) == clingo_solve_result_satisfiable)
            {
                satisfiable = true;
            } 
            else if ((result & clingo_solve_result_unsatisfiable) == clingo_solve_result_unsatisfiable)
            {
                satisfiable = false;
            }

            return new SolveResult()
            {
                IsSatisfiable = satisfiable,
                IsUnSatisfiable = satisfiable.HasValue ? !satisfiable : null,
                IsUnknown = !satisfiable.HasValue,
                IsExhausted = (result & clingo_solve_result_exhausted) == clingo_solve_result_exhausted,
                IsInterrupted = (result & clingo_solve_result_interrupted) == clingo_solve_result_interrupted
            };
        }

        public void Resume()
        {
            Clingo.HandleClingoError(SolveHandleModule.Resume(this));
        }

        public bool Wait(float? timeout = null)
        {
            SolveHandleModule.Wait(this, Convert.ToDouble(timeout.HasValue ? -timeout : 0.0f), out bool result);

            return result;
        }

        public IEnumerator<Model> GetEnumerator()
        {
            return new ModelEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
