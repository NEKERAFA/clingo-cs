using ClingoSharp.NativeWrapper.Interfaces;
using System;

namespace ClingoSharp.CoreServices.Interfaces
{
    /// <summary>
    /// This interface defines the main functions to get a implementation of clingo module
    /// </summary>
    internal interface IClingoContext : IDisposable
    {
        /// <summary>
        /// Gets a implementation module of a any type
        /// </summary>
        /// <param name="moduleType">The type of the module to find the implementation. It must be a subtype of <see cref="IClingoModule"/></param>
        /// <returns>A class that is the implementation of that module.</returns>
        IClingoModule GetModule(Type moduleType);

        /// <summary>
        /// Gets a implementation module of a any type
        /// </summary>
        /// <typeparam name="T">The type of the module to find the implementation.</typeparam>
        /// <returns>A class that is the implementation of that module.</returns>
        T GetModule<T>() where T : IClingoModule;
    }
}
