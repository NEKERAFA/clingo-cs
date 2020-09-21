namespace ClingoSharp.CoreServices.Interfaces
{
    /// <summary>
    /// Represents the interface to load unmanaged code
    /// </summary>
    public interface ILibraryLoadContext
    {
        void LoadClingoLibrary(string currentPath);
        void FreeClingoLibrary();
    }
}
