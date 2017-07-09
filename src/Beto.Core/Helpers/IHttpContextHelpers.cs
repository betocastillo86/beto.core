﻿//-----------------------------------------------------------------------
// <copyright file="IHttpContextHelpers.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Helpers
{
    /// <summary>
    /// Interface for Context Helpers
    /// </summary>
    public interface IHttpContextHelpers
    {
        /// <summary>
        /// Gets the current <c>ip</c> address.
        /// </summary>
        /// <returns>the value</returns>
        string GetCurrentIpAddress();

        /// <summary>
        /// Gets the this page URL.
        /// </summary>
        /// <param name="includeQueryString">if set to <c>true</c> [include query string].</param>
        /// <returns>the value</returns>
        string GetThisPageUrl(bool includeQueryString);
    }
}