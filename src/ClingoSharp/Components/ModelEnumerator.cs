using System;
using System.Collections;
using System.Collections.Generic;

namespace ClingoSharp
{
    /// <summary>
    /// Represents a enumerator for iterate the models of the solve result
    /// </summary>
    public class ModelEnumerator : IEnumerator<Model>
    {
        #region Attributes

        private readonly IntPtr m_clingoSolveHandle;

        #endregion

        #region Instance properties

        object IEnumerator.Current => Current;

        public Model Current { get; private set; }

        #endregion

        #region Constructor

        public ModelEnumerator(IntPtr clingoSolveHandle)
        {
            m_clingoSolveHandle = clingoSolveHandle;
        }

        #endregion

        #region Class Methods

        public static implicit operator IntPtr(ModelEnumerator enumerator)
        {
            return enumerator.m_clingoSolveHandle;
        }

        public static implicit operator ModelEnumerator(IntPtr clingoSolveHandle)
        {
            return new ModelEnumerator(clingoSolveHandle);
        }

        public static implicit operator Model(ModelEnumerator enumerator)
        {
            return enumerator.Current;
        }

        #endregion

        #region Instance methods

        public void Dispose()
        {
            Clingo.HandleClingoError(SolveHandle.SolveHandleModule.Close(this));
        }

        public bool MoveNext()
        {
            bool result = false;

            Clingo.HandleClingoError(SolveHandle.SolveHandleModule.Resume(this));
            Clingo.HandleClingoError(SolveHandle.SolveHandleModule.Model(this, out IntPtr modelPtr));

            if (modelPtr != IntPtr.Zero)
            {
                Current = new Model(modelPtr);
                result = true;
            }

            return result;
        }

        public void Reset()
        {
            throw new InvalidOperationException();
        }

        #endregion
    }
}
