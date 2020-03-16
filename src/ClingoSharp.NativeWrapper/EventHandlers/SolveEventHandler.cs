using ClingoSharp.NativeWrapper.Enums;
using System;

namespace ClingoSharp.NativeWrapper.EventHandlers
{
    /// <summary>
    /// Callback function called during search to notify when the search is finished or a model is ready.
    /// </summary>
    /// If a (non-recoverable) clingo API function fails in this callback, it must return false. In case of errors not related to clingo, set error code <see cref="clingo_error.clingo_error_unknown"/> and return false to stop solving with an error.
    /// The event is either a pointer to a model, a pointer to two statistics objects (per step and accumulated statistics), or a solve result.
    /// <param name="type">the event type</param>
    /// <param name="event_data">the current event</param>
    /// <param name="data">user data of the callback</param>
    /// <param name="goon">can be set to false to stop solving</param>
    /// <returns>whether the call was successful</returns>
    internal delegate int clingo_solve_event_callback(clingo_solve_event_type type, IntPtr event_data, IntPtr data, out bool[] goon);
}
