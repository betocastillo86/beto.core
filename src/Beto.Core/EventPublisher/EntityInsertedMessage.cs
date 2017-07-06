//-----------------------------------------------------------------------
// <copyright file="EntityInsertedMessage.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.EventPublisher
{
    using Beto.Core.Data;

    /// <summary>
    /// Entity Inserted Message
    /// </summary>
    /// <typeparam name="T">the type</typeparam>
    public class EntityInsertedMessage<T> where T : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityInsertedMessage{T}"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public EntityInsertedMessage(T entity)
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