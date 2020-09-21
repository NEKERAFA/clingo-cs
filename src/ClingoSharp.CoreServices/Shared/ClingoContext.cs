using ClingoSharp.CoreServices.Interfaces;
using ClingoSharp.NativeWrapper.Interfaces;
using System;
using System.Reflection;

namespace ClingoSharp.CoreServices.Shared
{
    internal sealed class ClingoContext : IClingoContext
    {
        private readonly ILibraryLoadContext loaderContext;

        #region Assembly load context methods

        public ClingoContext(string currentPath)
        {
            // Checks if we are running in a Unix platform
            // https://www.mono-project.com/docs/faq/technical/#how-to-detect-the-execution-platform
            int platform = (int)Environment.OSVersion.Platform;
            if ((platform == 4) || (platform == 6) || (platform == 128))
            {
                loaderContext = new PoxisLoadContext();
            }
            else
            {
                loaderContext = new WindowsLoadContext();
            }

            loaderContext.LoadClingoLibrary(currentPath);
        }

        ~ClingoContext()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            loaderContext.FreeClingoLibrary();

            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        #region Clingo context methods

        public IClingoModule GetModule(Type moduleType)
        {
            Type tModule = null;

            foreach (Type t in Assembly.GetAssembly(typeof(IClingoModule)).GetTypes())
            {
                if (moduleType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                {
                    tModule = t;
                    break;
                }
            }

            return (IClingoModule)Activator.CreateInstance(tModule);
        }

        public T GetModule<T>() where T : IClingoModule
        {
            return (T)GetModule(typeof(T));
        }

        #endregion
    }
}
