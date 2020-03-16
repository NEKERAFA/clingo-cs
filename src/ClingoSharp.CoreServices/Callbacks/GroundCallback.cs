using ClingoSharp.CoreServices.Types;

namespace ClingoSharp.CoreServices.Callbacks
{
    public delegate bool GroundCallback(Location location, string name, Symbol[] arguments, SymbolCallback callback);
}
