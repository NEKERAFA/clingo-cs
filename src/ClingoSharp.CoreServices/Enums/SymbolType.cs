namespace ClingoSharp.CoreServices.Enums
{
    /// <summary>
    /// Enumeration of available symbol types
    /// </summary>
    public enum SymbolType
    {
        /// <summary>
        /// the <c>#inf</c> symbol
        /// </summary>
        Infimum = 0,

        /// <summary>
        /// a numeric symbol, e.g., <c>1</c>
        /// </summary>
        Number = 1,

        /// <summary>
        /// a numeric symbol, e.g., <c>"a"</c>
        /// </summary>
        String = 4,

        /// <summary>
        /// a function symbol, e.g., <c>c</c>, <c>(1, "a")</c>, or <c>f(1,"a")</c>
        /// </summary>
        Function = 5,

        /// <summary>
        /// the <c>#sup</c> symbol
        /// </summary>
        Supremum = 7
    }
}
