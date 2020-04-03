using ClingoSharp.CoreServices.Enums;
using System;

namespace ClingoSharp.CoreServices.EventHandlers
{
    /// <summary>
    /// <para>Callback function called during search to notify when the search is finished or a model is ready.</para>
    /// <para>If a(non-recoverable) clingo API function fails in this callback, it must return false. In case of errors not related to clingo, set error code clingo_error_unknown and return false to stop solving with an error.</para>
    /// <para>The event is either a pointer to a model, a pointer to two statistics objects (per step and accumulated statistics), or a solve result.</para>
    /// </summary>
    /// <remarks>
    /// If the search is finished, the model is <c>null</c>.
    /// </remarks>
    /// <param name="type">the current event</param>
    /// <param name="eventData">user data of the callback</param>
    /// <param name="goon">can be set to false to stop solving</param>
    /// <returns>whether the call was successful</returns>
    public delegate bool SolveEventHandler(SolveEventType type, IntPtr eventData, out bool goon);
}
