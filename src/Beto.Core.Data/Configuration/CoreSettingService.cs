//-----------------------------------------------------------------------
// <copyright file="CoreSettingService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Configuration
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Caching;
    using Beto.Core.Data.Entities;
    using Beto.Core.EventPublisher;

    /// <summary>
    /// Service of core settings functionality
    /// </summary>
    public class CoreSettingService : ICoreSettingService
    {
        /// <summary>
        /// The settings get all
        /// </summary>
        private static string settingsGelAll = "cache.settings.all";

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// The context
        /// </summary>
        private readonly IDbContext context;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreSettingService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="publisher">The publisher.</param>
        public CoreSettingService(
            IDbContext context,
            ICacheManager cacheManager,
            IPublisher publisher)
        {
            this.context = context;
            this.cacheManager = cacheManager;
            this.publisher = publisher;
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the settings
        /// </returns>
        public async Task<IPagedList<TEntity>> GetAsync<TEntity>(string key = null, string value = null, int page = 0, int pageSize = int.MaxValue) where TEntity : class, ISettingEntity
        {
            var query = this.context.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(c => c.Name.Contains(key));
            }

            if (!string.IsNullOrEmpty(value))
            {
                query = query.Where(c => c.Value.Contains(value));
            }

            return await new PagedList<TEntity>().Async(query, page, pageSize);
        }

        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="keyword">The keyword.</param>
        /// <returns>
        /// The setting
        /// </returns>
        public TEntity GetByKey<TEntity>(string keyword) where TEntity : class, ISettingEntity
        {
            return this.context.Set<TEntity>().AsQueryable()
                .FirstOrDefault(c => c.Name.Equals(keyword));
        }

        /// <summary>
        /// Gets the cached setting.
        /// </summary>
        /// <typeparam name="T">the type of data saved on data base</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>
        /// the data saved
        /// </returns>
        public T GetCachedSetting<T, TEntity>(string key) where TEntity : class, ISettingEntity
        {
            string value = string.Empty;
            if (this.GetAllCachedSettings<TEntity>().TryGetValue(key, out value))
            {
                TypeConverter destinationConverter = TypeDescriptor.GetConverter(typeof(T));
                return (T)destinationConverter.ConvertFrom(null, System.Globalization.CultureInfo.InvariantCulture, value);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Updates the specified setting.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="setting">The setting.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Update<TEntity>(TEntity setting) where TEntity : class, ISettingEntity
        {
            await this.context.SaveChangesAsync();

            await this.publisher.EntityUpdated(setting);
        }

        /// <summary>
        /// Gets all cached settings.
        /// </summary>
        /// <typeparam name="T">the entity type</typeparam>
        /// <returns>the settings cached</returns>
        protected IDictionary<string, string> GetAllCachedSettings<T>() where T : class, ISettingEntity
        {
            var allKeys = this.cacheManager.Get(
                settingsGelAll,
                5000,
                () =>
                {
                    var dictionarySettings = new Dictionary<string, string>();
                    foreach (var setting in this.GetAsync<T>().Result)
                    {
                        dictionarySettings.Add(setting.Name, setting.Value);
                    }

                    return dictionarySettings;
                });
            return allKeys;
        }
    }
}