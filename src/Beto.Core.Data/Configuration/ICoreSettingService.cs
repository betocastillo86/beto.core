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
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the list of settings</returns>
        Task<IPagedList<ISettingEntity>> GetAsync(string key = null, string value = null, int page = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns>the setting</returns>
        ISettingEntity GetByKey(string keyword);

        /// <summary>
        /// Updates the specified setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns>the task</returns>
        Task Update(ISettingEntity setting);

        /// <summary>
        /// Gets the cached setting.
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>the value of the key typed</returns>
        T GetCachedSetting<T>(string key);
    }
}