//-----------------------------------------------------------------------
// <copyright file="ISubscriber.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.EventPublisher
{
    using System.Threading.Tasks;
    using Beto.Core.Data;

    /// <summary>
    /// Interface of subscriber <![CDATA[Interfaz encargada de suscribir las clases a los eventos especificos que deseen suscribir]]>
    /// </summary>
    /// <typeparam name="T">The Type</typeparam>
    public interface ISubscriber<T>
    {
        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>the task</returns>
        Task HandleEvent(T message);
    }
}