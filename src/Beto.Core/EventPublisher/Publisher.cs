//-----------------------------------------------------------------------
// <copyright file="Publisher.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.EventPublisher
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Beto.Core.Registers;

    /// <summary>
    /// The publisher <![CDATA[Clase encargada de publicar los envetos que se generen a lo largo del sitio]]>
    /// </summary>
    public class Publisher : IPublisher
    {
        /// <summary>
        /// The application builder
        /// </summary>
        private readonly IServiceFactory serviceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> class.
        /// </summary>
        /// <param name="serviceFactory">The service factory.</param>
        public Publisher(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        /// <summary>
        /// Publishes the specified message. <![CDATA[Consulta las clases suscritas y envia el mensaje]]>
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>the task</returns>
        public async Task Publish<T>(T message)
        {
            var subscriptios = this.GetSubscriptions<T>();
            foreach (var subscription in subscriptios)
            {
                await subscription.HandleEvent(message);
            }
        }

        /// <summary>
        /// Gets the subscriptions.<![CDATA[Consulta cuales son las interfaces suscritas a los eventos y genera las instancias respectivas para ser llamadas]]>
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <returns>list of subscribers</returns>
        private IList<ISubscriber<T>> GetSubscriptions<T>()
        {
            return (IList<ISubscriber<T>>)this.serviceFactory.GetServices(typeof(ISubscriber<T>));
        }
    }
}