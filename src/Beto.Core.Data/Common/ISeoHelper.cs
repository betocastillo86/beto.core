//-----------------------------------------------------------------------
// <copyright file="ISeoHelper.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Common
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Interface to friendly
    /// </summary>
    public interface ISeoHelper
    {
        /// <summary>
        /// Generates the name of the friendly.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="query">The query of SEO entities to validate if the friendly name already exists</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>the value</returns>
        string GenerateFriendlyName(string name, IQueryable<ISeoEntity> query = null, int maxLength = 280);

        /// <summary>
        /// Gets the site map XML.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <returns>the XML Site map</returns>
        string GetSiteMapXml(IList<SitemapRoute> routes);
    }
}