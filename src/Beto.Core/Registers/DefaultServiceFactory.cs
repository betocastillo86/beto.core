//-----------------------------------------------------------------------
// <copyright file="DefaultServiceFactory.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Registers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;

    public class DefaultServiceFactory : IServiceFactory
    {
        /// <summary>
        /// The application builder
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultServiceFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public DefaultServiceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the service  of the implemented type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// the object of the implemented type
        /// </returns>
        public object GetService(Type type)
        {
            return this.serviceProvider.GetService(type);
        }

        /// <summary>
        /// Gets the services of the implemented type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// the objects of the implementations
        /// </returns>
        public IEnumerable<object> GetServices(Type type)
        {
            return this.serviceProvider.GetServices(type);
        }
    }
}