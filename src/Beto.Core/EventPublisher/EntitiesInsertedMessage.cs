//-----------------------------------------------------------------------
// <copyright file="EntitiesInsertedMessage.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.EventPublisher
{
    using System.Collections.Generic;
    using Beto.Core.Data;

    /// <summary>
    /// Entities Inserted Message
    /// </summary>
    /// <typeparam name="T">the entity</typeparam>
    public class EntitiesInsertedMessage<T> where T : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesInsertedMessage{T}"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public EntitiesInsertedMessage(IList<T> entities)
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