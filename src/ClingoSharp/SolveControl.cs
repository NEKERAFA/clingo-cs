namespace ClingoSharp
{
    /// <summary>
    /// Object that allows for controlling a running search.
    /// </summary>
    public class SolveControl
    {
        #region Attributes

        private CoreServices.Types.SolveControl m_clingoSolveControl;

        #endregion

        #region Constructors

        internal SolveControl(CoreServices.Types.SolveControl clingoSolveControl)
        {
            m_clingoSolveControl = clingoSolveControl;
        }

        #endregion
    }
}