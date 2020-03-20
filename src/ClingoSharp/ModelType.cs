using ClingoSharp.Enums;
using System.Collections.Generic;

namespace ClingoSharp
{
    /// <summary>
    /// Enumeration of the different types of models.
    /// </summary>
    public class ModelType : Enumeration
    {
        #region Class attributes

        protected new static string[] m_names = new string[]
        {
            "StableModel",
            "BraveConsequences",
            "CautiousConsequences"
        };

        #endregion

        #region Class Properties

        /// <summary>
        /// The model captures a stable model
        /// </summary>
        public static ModelType StableModel => new ModelType(0);

        /// <summary>
        /// The model stores the set of brave consequences
        /// </summary>
        public static ModelType BraveConsequences => new ModelType(1);

        /// <summary>
        /// The model stores the set of cautious consequences
        /// </summary>
        public static ModelType CautiousConsequences => new ModelType(2);

        #endregion

        #region Constructors

        private ModelType(int value) : base(value) { }

        #endregion

        #region Class methods

        /// <summary>
        /// Gets a iterator of the constants in the enumeration
        /// </summary>
        /// <returns>A <see cref="Enumerable"/> iterator with the constants in the enumeration</returns>
        public new static IEnumerable<Enumeration> GetValues()
        {
            return new ModelType[] { StableModel, BraveConsequences, CautiousConsequences };
        }

        #endregion

        #region Instance methods

        /// <inheritdoc/>
        public override int CompareTo(Enumeration other)
        {
            if ((other == null) || !(other is ModelType))
            {
                return 1;
            }

            return Value.CompareTo(other.Value);
        }

        /// <inheritdoc/>
        public override bool Equals(Enumeration other)
        {
            if ((other == null) || !(other is ModelType))
            {
                return false;
            }

            return Value.Equals(other.Value);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"CligoSharp.ModelType<{Name}>";
        }

        #endregion
    }
}