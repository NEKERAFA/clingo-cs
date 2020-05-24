using ClingoSharp.CoreServices.Components.Enums;
using ClingoSharp.CoreServices.Components.Types;
using System;

namespace ClingoSharp.CoreServices.Components.Callbacks
{
    /// <summary>
    /// <para>Callback function to implement external functions.</para>
    /// <para>If an external function of form <c>@name(parameters)</c> occurs in a logic program, then this function is called with its location, name, parameters, and a callback to inject symbols as arguments.The callback can be called multiple times; all symbols passed are injected.</para>
    /// <para>If a (non-recoverable) clingo API function fails in this callback, for example, the symbol callback, the callback must return false. In case of errors not related to clingo, this function can set error <see cref="ErrorCode.Unknown"/> and return false to stop grounding with an error.</para>
    /// </summary>
    /// <param name="location">location from which the external function was called</param>
    /// <param name="name">name of the called external function</param>
    /// <param name="arguments">arguments of the called external function</param>
    /// <param name="data">user data of the callback</param>
    /// <param name="callback">function to inject symbols</param>
    /// <param name="callbackData">user data for the symbol callback (must be passed untouched)</param>
    /// <returns><c>true</c> if the function is success, <c>false</c> otherwise</returns>
    public delegate bool GroundCallback(Location location, string name, Symbol[] arguments, IntPtr data, SymbolCallback callback, IntPtr callbackData);
}
