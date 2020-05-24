using ClingoSolveControl = ClingoSharp.CoreServices.Components.Types.SolveControl;

namespace ClingoSharp
{
    /// <summary>
    /// Object that allows for controlling a running search.
    /// </summary>
    public sealed class SolveControl
    {
        #region Attributes

        private ClingoSolveControl m_clingoSolveControl;

        #endregion

        #region Constructors

        internal SolveControl(ClingoSolveControl clingoSolveControl)
        {
            m_clingoSolveControl = clingoSolveControl;
        }

        #endregion
    }
}