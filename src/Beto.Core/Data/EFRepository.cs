//-----------------------------------------------------------------------
// <copyright file="EFRepository.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Repository of Entity Framework
    /// </summary>
    /// <typeparam name="T">Any entity</typeparam>
    /// <seealso cref="Beto.Core.Data.IRepository{T}" />
    public partial class EFRepository<T> : IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IDbContext context;

        /// <summary>
        /// The entities
        /// </summary>
        private DbSet<T> entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EFRepository(IDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <value>
        /// The table.
        /// </value>
        public virtual IQueryable<T> Table => this.Entities;

        /// <summary>
        /// Gets the table no tracking.
        /// </summary>
        /// <value>
        /// The table no tracking.
        /// </value>
        public IQueryable<T> TableNoTracking => this.Entities.AsNoTracking();

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (this.entities == null)
                {
                    this.entities = this.context.Set<T>();
                }

                return this.entities;
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>the value</returns>
        /// <exception cref="System.ArgumentNullException">the entity</exception>
        public virtual int Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.context.Entry(entity).State = EntityState.Deleted;

            this.Entities.Remove(entity);

            return this.context.SaveChanges();
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">The Entities</param>
        /// <exception cref="System.ArgumentNullException">the entities</exception>
        public virtual void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.context.Entry(entity).State = EntityState.Deleted;
                this.Entities.Remove(entity);
            }

            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the number of rows</returns>
        /// <exception cref="System.ArgumentNullException">the entity</exception>
        public async Task<int> DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.context.Entry(entity).State = EntityState.Deleted;
            this.Entities.Remove(entity);

            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <exception cref="System.ArgumentNullException">the entity</exception>
        public virtual void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.Entities.Add(entity);

            this.context.SaveChanges();
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">The Entities</param>
        /// <exception cref="System.ArgumentNullException">the entities</exception>
        public virtual void Insert(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            this.Entities.AddRange(entities);

            this.context.SaveChanges();
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>
        /// the task
        /// </returns>
        /// <exception cref="System.ArgumentNullException">null entities</exception>
        public virtual async Task InsertAsync(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            await this.Entities.AddRangeAsync(entities);

            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>the task</returns>
        /// <exception cref="System.ArgumentNullException">the entities</exception>
        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.Entities.Add(entity);

            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>the value</returns>
        /// <exception cref="System.ArgumentNullException">the entity</exception>
        public virtual int Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.context.Entry(entity).State = EntityState.Modified;

            return this.context.SaveChanges();
        }

        /// <summary>
        /// Updates the entities
        /// </summary>
        /// <param name="entities">the entities</param>
        public virtual void Update(ICollection<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.context.Entry(entity).State = EntityState.Modified;
            }

            this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// the value
        /// </returns>
        /// <exception cref="System.ArgumentNullException">the entity</exception>
        public async Task<int> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.context.Entry(entity).State = EntityState.Modified;

            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>
        /// the value
        /// </returns>
        /// <exception cref="System.ArgumentNullException">null entities</exception>
        public virtual async Task UpdateAsync(ICollection<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.context.Entry(entity).State = EntityState.Modified;
            }

            await this.context.SaveChangesAsync();
        }
    }
}