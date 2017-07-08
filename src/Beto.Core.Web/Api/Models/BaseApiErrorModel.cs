//-----------------------------------------------------------------------
// <copyright file="BaseApiErrorModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Api
{
    /// <summary>
    /// Base <c>Api</c> Error
    /// </summary>
    public class BaseApiErrorModel
    {
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public ApiErrorModel Error { get; set; }
    }
}