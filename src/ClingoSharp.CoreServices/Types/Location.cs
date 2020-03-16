namespace ClingoSharp.CoreServices.Types
{
    public class Location
    {
        public string BeginFile { get; set; }
        public string EndFile { get; set; }
        public ulong BeginLine { get; set; }
        public ulong BeginColumn { get; set; }
        public ulong EndLine { get; set; }
        public ulong EndColumn { get; set; }
    }
}
