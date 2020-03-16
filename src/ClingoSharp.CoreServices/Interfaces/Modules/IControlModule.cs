using ClingoSharp.CoreServices.Callbacks;
using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.EventHandlers;
using ClingoSharp.CoreServices.Types;
using System;

namespace ClingoSharp.CoreServices.Interfaces.Modules
{
    public interface IControlModule : IModule
    {
        /// <summary>
        /// Creates a new control object.
        /// </summary>
        /// <param name="arguments">An array of command line arguments</param>
        /// <param name="logger">callback functions for warnings and info messages</param>
        /// <param name="messageLimit">maximum number of times the logger callback is called</param>
        /// <param name="control">resulting control object</param>
        /// <returns>true if the function is success and false otherwise</returns>
        bool New(string[] arguments, LoggerCallback logger, uint messageLimit, out IntPtr control);
        
        /// <summary>
        /// Frees a control object created with <see cref="New(string[], LoggerCallback, uint, out IntPtr)"/>
        /// </summary>
        /// <param name="control">a control pointer</param>
        void Free(IntPtr control);

        /// <summary>
        /// Extend the logic program with the given non-ground logic program in string form.
        /// This function puts the given program into a block of form: <c>#program name(parameters).</c>
        /// After extending the logic program, the corresponding program parts are typically grounded with <see cref="Ground(IntPtr, Part[], GroundCallback)"/>
        /// </summary>
        /// <param name="control">a control ponter</param>
        /// <param name="name">name of the program block</param>
        /// <param name="parameters">string array of parameters of the program block</param>
        /// <param name="program">string representation of the program</param>
        /// <returns>true if the function is success and false otherwise</returns>
        bool Add(IntPtr control, string name, string[] parameters, string program);

        /// <summary>
        /// Ground the selected parts of the current (non-ground) logic program.
        /// After grounding, logic programs can be solved with <see cref="Solve(IntPtr, SolveMode, Literal[], SolveEventHandler, out IntPtr)"/>.
        /// <remarks>Parts of a logic program without an explicit <c>#program</c> specification are by default put into a program called base without arguments.</remarks>
        /// </summary>
        /// <param name="control">a control ponter</param>
        /// <param name="parts">array of parts to ground</param>
        /// <param name="callback">callback to implement external functions</param>
        /// <returns>true if the function is success and false otherwise</returns>
        bool Ground(IntPtr control, Part[] parts, GroundCallback callback);

        /// <summary>
        /// Solve the currently grounded logic program enumerating its models.
        /// </summary>
        /// <param name="control">a control ponter</param>
        /// <param name="mode">configures the search mode</param>
        /// <param name="assumptions">array of assumptions to solve under</param>
        /// <param name="callback">the event handler to register</param>
        /// <param name="handle">handle to the current search to enumerate models</param>
        /// <returns>true if the function is success and false otherwise</returns>
        bool Solve(IntPtr control, SolveMode mode, Literal[] assumptions, SolveEventHandler callback, out IntPtr handle);
    }
}
