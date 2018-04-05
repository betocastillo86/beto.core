//-----------------------------------------------------------------------
// <copyright file="EntitiesDeletedMessage.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.EventPublisher
{
    using System.Collections.Generic;
    using Beto.Core.Data;

    /// <summary>
    /// Entities Deleted Message
    /// </summary>
    /// <typeparam name="T">the entity</typeparam>
    public class EntitiesDeletedMessage<T> where T : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesDeletedMessage{T}"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public EntitiesDeletedMessage(IList<T> entities)
        {
            this.Entities = entities;
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public IList<T> Entities { get; private set; }
    }
}