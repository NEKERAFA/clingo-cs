using ClingoSharp.CoreServices.Components.Types;
using System;

namespace ClingoSharp.CoreServices.Components.Callbacks
{
    /// <summary>
    /// Callback function to inject symbols
    /// </summary>
    /// <param name="symbols">array of symbols</param>
    /// <param name="data">user data of the callback</param>
    /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
    public delegate bool SymbolCallback(Symbol[] symbols, IntPtr data);
}
