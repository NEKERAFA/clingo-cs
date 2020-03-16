namespace ClingoSharp
{
    public class SolveResult
    {
        public bool IsExhausted { get; internal set; }
        public bool IsInterrupted { get; internal set; }
        public bool? IsSatisfiable { get; internal set; }
        public bool IsUnknown { get; internal set; }
        public bool? IsUnSatisfiable { get; internal set; }

        internal SolveResult() { }
    }
}
