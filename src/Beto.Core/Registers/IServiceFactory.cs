//-----------------------------------------------------------------------
// <copyright file="IServiceFactory.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Registers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface of service which allows to get service implementations
    /// </summary>
    public interface IServiceFactory
    {
        /// <summary>
        /// Gets the services of the implemented type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>the objects of the implementations</returns>
        IEnumerable<object> GetServices(Type type);

        /// <summary>
        /// Gets the service  of the implemented type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>the object of the implemented type</returns>
        object GetService(Type type);
    }
}