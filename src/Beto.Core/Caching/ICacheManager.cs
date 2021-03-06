﻿//-----------------------------------------------------------------------
// <copyright file="ICacheManager.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Caching
{
    /// <summary>
    /// Interface of cache manager
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T">any type</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>the value in cache</returns>
        T Get<T>(string key);

        /// <summary>
        /// Determines whether the specified key is set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if the specified key is set; otherwise, <c>false</c>.
        /// </returns>
        bool IsSet(string key);

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(string key);

        /// <summary>
        /// Removes the by pattern.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        /// <param name="cacheTime">The cache time in minutes.</param>
        void Set(string key, object data, int cacheTime);
    }
}