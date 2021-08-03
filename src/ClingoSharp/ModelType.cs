﻿using ClingoSharp.Enums;

namespace ClingoSharp
{
    /// <summary>
    /// Enumeration of the different types of models.
    /// </summary>
    public sealed class ModelType : Enumeration
    {
        #region Class attributes

        private static readonly string[] ModelTypeNames = new string[]
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

        private ModelType(int value) : base(value, ModelTypeNames[value]) { }

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

        #endregion
    }
}