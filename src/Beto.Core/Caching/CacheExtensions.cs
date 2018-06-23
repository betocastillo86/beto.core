//-----------------------------------------------------------------------
// <copyright file="CacheExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Caching
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Static class Cache Extensions
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">the Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">the Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="cacheTime">Cache time in minutes (0 - do not cache)</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }

            var result = acquire();
            if (cacheTime > 0)
            {
                cacheManager.Set(key, result, cacheTime);
            }

            return result;
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="key">The key.</param>
        /// <param name="acquire">The acquire.</param>
        /// <returns>the task</returns>
        public static async Task<T> GetAsync<T>(this ICacheManager cacheManager, string key, Func<Task<T>> acquire)
        {
            return await GetAsync(cacheManager, key, 60, acquire);
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="key">The key.</param>
        /// <param name="cacheTime">The cache time.</param>
        /// <param name="acquire">The acquire.</param>
        /// <returns>the task</returns>
        public static async Task<T> GetAsync<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<Task<T>> acquire)
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }

            var result = await acquire();
            if (cacheTime > 0)
            {
                cacheManager.Set(key, result, cacheTime);
            }

            return result;
        }
    }
}