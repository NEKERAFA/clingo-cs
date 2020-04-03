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
                var assemblyPath = new Uri(typeof(ClingoContext).Assembly.CodeBase).LocalPath;
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
            var assemblyPath = new Uri(typeof(ClingoContext).Assembly.CodeBase).LocalPath;
            // Gets assembly folder
            var assemblyFolder = Path.GetDirectoryName(assemblyPath);
            // Gets arch process
            string path = Environment.Is64BitProcess ? "lib" : "lib32";
            // Gets extension name
            string extension;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                extension = "dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                extension = "dylib";
            }
            else
            {
                extension = "so";
            }

            // Loads clingo library
            return LoadUnmanagedDllFromPath(Path.Combine(assemblyFolder, path, $"clingo.{extension}"));
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
