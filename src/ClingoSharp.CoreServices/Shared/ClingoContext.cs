using ClingoSharp.CoreServices.Interfaces;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace ClingoSharp.CoreServices.Shared
{
    internal sealed class ClingoContext : AssemblyLoadContext, IClingoContext
    {
        #region Assembly load context methods

        protected override Assembly Load(AssemblyName assemblyName)
        {
            if (assemblyName.Name.Equals(Constants.NativeWrapper))
            {
                // Gets assembly path file
                var assemblyPath = new Uri(Assembly.GetExecutingAssembly().Location).LocalPath;
                // Gets assembly folder
                var assemblyFolder = Path.GetDirectoryName(assemblyPath);

                return LoadFromAssemblyPath(Path.Combine(assemblyFolder, $"{Constants.NativeWrapper}.dll"));
            }

            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            if (unmanagedDllName.Contains(Constants.ClingoLib))
            {
                return LoadNativeClingoLibrary();
            }

            return base.LoadUnmanagedDll(unmanagedDllName);
        }

        private IntPtr LoadNativeClingoLibrary()
        {
            // Gets assembly path file
            var assemblyPath = new Uri(Assembly.GetExecutingAssembly().Location).LocalPath;
            // Gets assembly folder
            var assemblyFolder = Path.GetDirectoryName(assemblyPath);

            // Gets the path to the native libraries
            string nativeLibraryFolder;
            // Gets the extension of the native libray
            string extension;
            // Gets the prefix of the native library
            string prefix;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                nativeLibraryFolder = $"win";
                prefix = "";
                extension = "dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                nativeLibraryFolder = $"linux"; 
                prefix = "lib";
                extension = "so";
            }
            else
            {
                throw new PlatformNotSupportedException("ClingoSharp is not tested to this platform");
            }

            // Loads clingo library
            return LoadUnmanagedDllFromPath(Path.Combine(assemblyFolder, "runtimes", nativeLibraryFolder, "native", $"{prefix}clingo.{extension}"));
        }

        #endregion

        #region Clingo context methods

        public IClingoModule GetModule(Type moduleType)
        {
            var nativeWrapperAssembly = LoadFromAssemblyName(new AssemblyName(Constants.NativeWrapper));
            var type = nativeWrapperAssembly.GetType($"{Constants.NativeWrapper}.{moduleType.Name.Substring(1)}Impl", true);
            return (IClingoModule)Activator.CreateInstance(type);
        }

        public T GetModule<T>() where T : IClingoModule
        {
            return (T)GetModule(typeof(T));
        }

        #endregion
    }
}
