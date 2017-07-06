//-----------------------------------------------------------------------
// <copyright file="IDbContext.cs" company="Dasigno">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;

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
        Database Database { get; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>modified records</returns>
        int SaveChanges();

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>the task</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Sets this instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>the table</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}