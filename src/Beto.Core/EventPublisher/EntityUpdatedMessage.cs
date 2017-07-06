//-----------------------------------------------------------------------
// <copyright file="EntityUpdatedMessage.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.EventPublisher
{
    using Beto.Core.Data;

    /// <summary>
    /// Entity Updated Message
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    public class EntityUpdatedMessage<T> where T : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityUpdatedMessage{T}"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        public EntityUpdatedMessage(T entity, string action)
        {
            this.Entity = entity;
            this.Action = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityUpdatedMessage{T}"/> class.
        /// </summary>
        /// <param name="newValues">The new values.</param>
        /// <param name="oldValues">The old values.</param>
        /// <param name="action">The action.</param>
        public EntityUpdatedMessage(T newValues, T oldValues, string action)
        {
            this.Entity = newValues;
            this.OldEntity = oldValues;
            this.Action = action;
        }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public T Entity { get; private set; }

        /// <summary>
        /// Gets or sets the old entity.
        /// </summary>
        /// <value>
        /// The old entity.
        /// </value>
        public T OldEntity { get; set; }
    }
}