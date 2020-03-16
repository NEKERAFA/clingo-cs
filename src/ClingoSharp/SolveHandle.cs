using ClingoSharp.CoreServices;
using ClingoSharp.CoreServices.Interfaces.Modules;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ClingoSharp
{
    public class SolveHandle : IEnumerable<Model>
    {
        private readonly IntPtr m_clingoSolveHandle;
        private static readonly ISolveHandleModule m_module;

        static SolveHandle()
        {
            m_module = Repository.GetModule<ISolveHandleModule>();
        }

        internal SolveHandle(IntPtr clingoSolveHandle)
        {
            m_clingoSolveHandle = clingoSolveHandle;
        }

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
            IntPtr model;
            do
            {
                Clingo.HandleClingoError(m_module.Resume(m_clingoSolveHandle));
                Clingo.HandleClingoError(m_module.Model(m_clingoSolveHandle, out model));

                if (model != IntPtr.Zero)
                {
                    yield return new Model();
                }
            }
            while (model != IntPtr.Zero);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
