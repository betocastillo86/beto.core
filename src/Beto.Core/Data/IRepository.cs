﻿//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface of Repository
    /// </summary>
    /// <typeparam name="T">the entity</typeparam>
    public partial interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <value>
        /// The table.
        /// </value>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Gets the table no tracking.
        /// </summary>
        /// <value>
        /// The table no tracking.
        /// </value>
        IQueryable<T> TableNoTracking { get; }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the value</returns>
        int Delete(T entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">The Entities</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the entities modified</returns>
        Task<int> DeleteAsync(T entity);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">The entity</param>
        void Insert(T entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">The Entities</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the task</returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the task</returns>
        Task InsertAsync(IEnumerable<T> entities);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the value</returns>
        int Update(T entity);

        /// <summary>
        /// Updates the entities
        /// </summary>
        /// <param name="entities">the entities</param>
        void Update(ICollection<T> entities);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the value</returns>
        Task<int> UpdateAsync(T entity);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the value</returns>
        Task UpdateAsync(ICollection<T> entities);
    }
}