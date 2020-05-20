using ClingoSharp.CoreServices.Callbacks;
using ClingoSharp.CoreServices.Enums;
using ClingoSharp.CoreServices.EventHandlers;
using ClingoSharp.CoreServices.Types;
using System;

namespace ClingoSharp.CoreServices.Interfaces.Modules
{
    /// <summary>
    /// Functions to control the grounding and solving process.
    /// </summary>
    public interface IControlModule : Interfaces.IClingoModule
    {
        #region Functions

        /// <summary>
        /// Creates a new control object.
        /// </summary>
        /// <param name="arguments">An array of command line arguments</param>
        /// <param name="logger">callback functions for warnings and info messages</param>
        /// <param name="messageLimit">maximum number of times the logger callback is called</param>
        /// <param name="control">resulting control object</param>
        /// <returns><c>true</c> if the function is success and <c>false</c> otherwise</returns>
        bool New(string[] arguments, LoggerCallback logger, uint messageLimit, out Control control);
        
        /// <summary>
        /// Frees a control object created with <see cref="New(string[], LoggerCallback, uint, out Control)"/>
        /// </summary>
        /// <param name="control">a control pointer</param>
        void Free(Control control);

        #endregion

        #region Grounding Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool Load(Control control, string filename);

        /// <summary>
        /// <para>Extend the logic program with the given non-ground logic program in string form.</para>
        /// <para>This function puts the given program into a block of form: <c>#program name(parameters).</c></para>
        /// <para>After extending the logic program, the corresponding program parts are typically grounded with <see cref="Ground(IntPtr, Part[], GroundCallback)"/></para>
        /// </summary>
        /// <param name="control">a control ponter</param>
        /// <param name="name">name of the program block</param>
        /// <param name="parameters">string array of parameters of the program block</param>
        /// <param name="program">string representation of the program</param>
        /// <returns><c>true</c> if the function is success and <c>false</c> otherwise</returns>
        bool Add(Control control, string name, string[] parameters, string program);

        /// <summary>
        /// <para>Ground the selected parts of the current (non-ground) logic program.</para>
        /// <para>After grounding, logic programs can be solved with <see cref="Solve(Control, SolveMode, Literal[], SolveEventHandler, out IntPtr)"/>.</para>
        /// <remarks>
        /// Parts of a logic program without an explicit <c>#program</c> specification are by default put into a program called base without arguments.
        /// </remarks>
        /// </summary>
        /// <param name="control">a control ponter</param>
        /// <param name="parts">array of parts to ground</param>
        /// <param name="callback">callback to implement external functions</param>
        /// <returns><c>true</c> if the function is success and <c>false</c> otherwise</returns>
        bool Ground(Control control, Part[] parts, GroundCallback callback);

        #endregion

        #region Solving Functions

        /// <summary>
        /// Solve the currently grounded logic program enumerating its models.
        /// </summary>
        /// <param name="control">a control ponter</param>
        /// <param name="mode">configures the search mode</param>
        /// <param name="assumptions">array of assumptions to solve under</param>
        /// <param name="callback">the event handler to register</param>
        /// <param name="handle">handle to the current search to enumerate models</param>
        /// <returns><c>true</c> if the function is success and <c>false</c> otherwise</returns>
        bool Solve(Control control, SolveMode mode, Literal[] assumptions, SolveEventHandler callback, out SolveHandle handle);

        #endregion

        #region Program Inspection Functions

        /// <summary>
        /// Return the symbol for a constant definition of form: <c>#const name = symbol.</c>
        /// </summary>
        /// <param name="control">the target</param>
        /// <param name="name">the name of the constant</param>
        /// <param name="symbol">the resulting symbol</param>
        /// <returns><c>true</c> if the function is success and <c>false</c> otherwise</returns>
        bool GetConst(Control control, string name, out Symbol symbol);

        /// <summary>
        /// Check if there is a constant definition for the given constant.
        /// </summary>
        /// <param name="control">the target</param>
        /// <param name="name">the name of the constant</param>
        /// <param name="exists">whether a matching constant definition exists</param>
        /// <returns><c>true</c> if the function is success and <c>false</c> otherwise</returns>
        bool HasConst(Control control, string name, out bool exists);

        /// <summary>
        /// Get an object to inspect symbolic atoms (the relevant Herbrand base) used for grounding
        /// </summary>
        /// <param name="control">the target</param>
        /// <param name="symbolicAtoms">the symbolic atoms object</param>
        /// <returns><c>true</c> if the function is success and <c>false</c> otherwise</returns>
        bool GetSymbolicAtoms(Control control, out SymbolicAtoms symbolicAtoms);
        
        #endregion
    }
}
