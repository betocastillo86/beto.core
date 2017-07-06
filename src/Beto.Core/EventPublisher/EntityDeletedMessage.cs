//-----------------------------------------------------------------------
// <copyright file="EntityDeletedMessage.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.EventPublisher
{
    using Beto.Core.Data;

    /// <summary>
    /// Entity Deleted Message
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    public class EntityDeletedMessage<T> where T : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDeletedMessage{T}"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public EntityDeletedMessage(T entity)
        {
            this.Entity = entity;
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public T Entity { get; private set; }
    }
}