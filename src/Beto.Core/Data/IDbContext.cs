//-----------------------------------------------------------------------
// <copyright file="IDbContext.cs" company="Dasigno">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    /// <summary>
    /// Interface for Entity Framework Context
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        DatabaseFacade Database { get; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>modified records</returns>
        int SaveChanges();

        /// <summary>
        /// Saves Changes asynchronous
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the task</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Sets this instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>the table</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}