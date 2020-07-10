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

        private readonly List<Model> m_models;

        private int m_position;

        #endregion

        #region Instance properties

        /// <summary>
        /// The current name of models
        /// </summary>
        public int Count => m_models.Count;

        public Model Current => m_models[m_position];

        object IEnumerator.Current => Current;

        #endregion

        #region Constructor

        public ModelEnumerator(IntPtr clingoSolveHandle)
        {
            m_clingoSolveHandle = clingoSolveHandle;
            m_models = new List<Model>();
            m_position = -1;
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

        #endregion

        #region Instance methods

        public void Dispose()
        {
            Clingo.HandleClingoError(SolveHandle.SolveHandleModule.Close(this));
            m_models.Clear();
        }

        public bool MoveNext()
        {
            bool result = false;

            if (m_position == (Count - 1))
            {
                Clingo.HandleClingoError(SolveHandle.SolveHandleModule.Resume(this));
                Clingo.HandleClingoError(SolveHandle.SolveHandleModule.Model(this, out IntPtr modelPtr));

                if (modelPtr != IntPtr.Zero)
                {
                    m_models.Add(modelPtr);
                    m_position++;
                    result = true;
                }
            }
            else if (m_position < (Count - 1))
            {
                m_position++;
                result = true;
            }

            return result;
        }

        public void Reset()
        {
            m_position = -1;
        }

        #endregion
    }
}
