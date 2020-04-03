using ClingoSharp.CoreServices.Interfaces;
using ClingoSharp.CoreServices.Interfaces.Modules;

namespace ClingoSharp.CoreServices.Types
{
    /// <summary>
    /// <para>Struct used to specify the program parts that have to be grounded.</para>
    /// <para>Programs may be structured into parts, which can be grounded independently with <see cref="IControlModule.Ground(Control, Part[], Callbacks.GroundCallback)"/>. Program parts are mainly interesting for incremental grounding and multi-shot solving. For single-shot solving, program parts are not needed.</para>
    /// </summary>
    /// <remarks>
    /// Parts of a logic program without an explicit #program specification are by default put into a program called <c>base</c> without arguments
    /// </remarks>
    public sealed class Part : IClingoObject
    {
        /// <summary>
        /// name of the program part
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// array of parameters
        /// </summary>
        public Symbol[] Params { get; set; }
    }
}
