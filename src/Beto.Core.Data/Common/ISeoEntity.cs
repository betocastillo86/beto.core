//-----------------------------------------------------------------------
// <copyright file="ISeoEntity.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Common
{
    /// <summary>
    /// Interface for <c>Seo</c> Entities
    /// </summary>
    public interface ISeoEntity
    {
        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        string FriendlyName { get; set; }
    }
}