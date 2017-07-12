//-----------------------------------------------------------------------
// <copyright file="GeneratedAuthenticationToken.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Security
{
    /// <summary>
    /// The token generated after authentication
    /// </summary>
    public class GeneratedAuthenticationToken
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the expires.
        /// </summary>
        /// <value>
        /// The expires.
        /// </value>
        public int Expires { get; set; }
    }
}