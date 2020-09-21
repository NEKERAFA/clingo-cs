using System;

namespace ClingoSharp
{
    /// <summary>
    /// Object that allows for controlling a running search.
    /// </summary>
    public sealed class SolveControl
    {
        #region Attributes

        private IntPtr m_clingoSolveControl;

        #endregion

        #region Constructors

        internal SolveControl(IntPtr clingoSolveControl)
        {
            m_clingoSolveControl = clingoSolveControl;
        }

        #endregion
    }
}