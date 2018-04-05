//-----------------------------------------------------------------------
// <copyright file="PublisherExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.EventPublisher
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Beto.Core.Data;

    /// <summary>
    /// Publisher Extensions
    /// </summary>
    public static class PublisherExtensions
    {
        /// <summary>
        /// When entities are deleted
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="list">The list.</param>
        /// <returns>the task</returns>
        public static async Task EntitiesDeleted<T>(this IPublisher eventPublisher, IList<T> list) where T : IEntity
        {
            await eventPublisher.Publish(new EntitiesDeletedMessage<T>(list));
        }

        /// <summary>
        /// When entities are inserted
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="list">The list.</param>
        /// <returns>the task</returns>
        public static async Task EntitiesInserted<T>(this IPublisher eventPublisher, IList<T> list) where T : IEntity
        {
            await eventPublisher.Publish(new EntitiesInsertedMessage<T>(list));
        }

        /// <summary>
        /// When entities are updated
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="list">The list.</param>
        /// <param name="action">The action.</param>
        /// <returns>the task</returns>
        public static async Task EntitiesUpdated<T>(this IPublisher eventPublisher, IList<T> list, string action = null) where T : IEntity
        {
            await eventPublisher.Publish(new EntitiesUpdatedMessage<T>(list));
        }

        /// <summary>
        /// When an entity is deleted
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>the task</returns>
        public static async Task EntityDeleted<T>(this IPublisher eventPublisher, T entity) where T : IEntity
        {
            await eventPublisher.Publish(new EntityDeletedMessage<T>(entity));
        }

        /// <summary>
        /// When an entity is inserted
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>the task</returns>
        public static async Task EntityInserted<T>(this IPublisher eventPublisher, T entity) where T : IEntity
        {
            await eventPublisher.Publish(new EntityInsertedMessage<T>(entity));
        }

        /// <summary>
        /// When an entity is updated
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        /// <returns>the task</returns>
        public static async Task EntityUpdated<T>(this IPublisher eventPublisher, T entity, string action = null) where T : IEntity
        {
            await eventPublisher.Publish(new EntityUpdatedMessage<T>(entity, action));
        }

        /// <summary>
        /// When an entity is updated
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="oldData">The old data.</param>
        /// <param name="action">The action.</param>
        /// <returns>the task</returns>
        public static async Task EntityUpdated<T>(this IPublisher eventPublisher, T entity, T oldData, string action = null) where T : IEntity
        {
            await eventPublisher.Publish(new EntityUpdatedMessage<T>(entity, oldData, action));
        }
    }
}