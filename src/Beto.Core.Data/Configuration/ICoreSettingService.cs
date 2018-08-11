//-----------------------------------------------------------------------
// <copyright file="ICoreSettingService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Configuration
{
    using System.Threading.Tasks;
    using Beto.Core.Data.Entities;

    /// <summary>
    /// Interface of core functionalities of Setting Service
    /// </summary>
    public interface ICoreSettingService
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the settings</returns>
        Task<IPagedList<TEntity>> GetAsync<TEntity>(string key = null, string value = null, int page = 0, int pageSize = int.MaxValue) where TEntity : class, ISettingEntity;

        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="keyword">The keyword.</param>
        /// <returns>The setting</returns>
        TEntity GetByKey<TEntity>(string keyword) where TEntity : class, ISettingEntity;

        /// <summary>
        /// Gets the cached setting.
        /// </summary>
        /// <typeparam name="T">the type of data saved on data base</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>the data saved</returns>
        T GetCachedSetting<T, TEntity>(string key) where TEntity : class, ISettingEntity;

        /// <summary>
        /// Inserts the specified setting.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="setting">The setting.</param>
        /// <returns>the task</returns>
        Task Insert<TEntity>(TEntity setting) where TEntity : class, ISettingEntity;

        /// <summary>
        /// Updates the specified setting.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="setting">The setting.</param>
        /// <returns>the task</returns>
        Task Update<TEntity>(TEntity setting) where TEntity : class, ISettingEntity;
    }
}