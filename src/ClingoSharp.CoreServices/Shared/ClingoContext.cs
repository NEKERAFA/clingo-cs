using ClingoSharp.CoreServices.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace ClingoSharp.CoreServices.Shared
{
    internal sealed class ClingoContext : IClingoContext
    {
        private readonly ILibraryLoadContext loaderContext;

        #region Assembly load context methods

        public ClingoContext()
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

            loaderContext.LoadClingoLibrary();
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
            string currentPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath);
            Assembly nativeWrapperAssembly = Assembly.LoadFrom(Path.Combine(currentPath, string.Format("{0}.dll", Constants.NativeWrapper)));

            string typeName = string.Format("{0}.Managers.{1}Impl", Constants.NativeWrapper, moduleType.Name.Substring(1));
            Type type = nativeWrapperAssembly.GetType(typeName);

            return (IClingoModule)Activator.CreateInstance(type);
        }

        public T GetModule<T>() where T : IClingoModule
        {
            return (T)GetModule(typeof(T));
        }

        #endregion
    }
}
