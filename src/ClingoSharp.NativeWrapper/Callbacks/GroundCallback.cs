using ClingoSharp.NativeWrapper.Types;
using ClingoSharp.NativeWrapper.Enums;
using System;
using clingo_symbol = System.UInt64;

namespace ClingoSharp.NativeWrapper.Callbacks
{
    /// <summary>
    /// Callback function to implement external functions.
    /// </summary>
    /// If an external function of form <code>@name(parameters)</code> occurs in a logic program, then this function is called with its location, name, parameters, and a callback to inject symbols as arguments.The callback can be called multiple times; all symbols passed are injected.
    /// If a (non-recoverable) clingo API function fails in this callback, for example, the symbol callback, the callback must return false. In case of errors not related to clingo, this function can set error <see cref="clingo_error.clingo_error_unknown"/> and return false to stop grounding with an error.
    /// <param name="location">location from which the external function was called</param>
    /// <param name="name">name of the called external function</param>
    /// <param name="arguments">arguments of the called external function</param>
    /// <param name="arguments_size">number of arguments</param>
    /// <param name="data">user data of the callback</param>
    /// <param name="symbol_callback">function to inject symbols</param>
    /// <param name="symbol_callback_data">user data for the symbol callback (must be passed untouched)</param>
    /// <returns>whether the call was successful</returns>
    internal delegate int clingo_ground_callback(clingo_location[] location, string name, clingo_symbol[] arguments, UIntPtr arguments_size, IntPtr data, clingo_symbol_callback symbol_callback, IntPtr symbol_callback_data);
}
