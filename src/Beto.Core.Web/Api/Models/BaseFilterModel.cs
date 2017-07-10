//-----------------------------------------------------------------------
// <copyright file="BaseFilterModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Api
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base Model for filter
    /// </summary>
    public abstract class BaseFilterModel
    {
        /// <summary>
        /// The errors
        /// </summary>
        private IList<ApiErrorModel> errors;

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IList<ApiErrorModel> Errors
        {
            get
            {
                return this.errors ?? (this.errors = new List<ApiErrorModel>());
            }
        }

        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        /// <value>
        /// The order by.
        /// </value>
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public int Page { get; set; } = 0;

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the maximum size of the page.
        /// </summary>
        /// <value>
        /// The maximum size of the page.
        /// </value>
        public int MaxPageSize { get; protected set; } = 50;

        /// <summary>
        /// Gets or sets the valid orders by.
        /// </summary>
        /// <value>
        /// The valid orders by.
        /// </value>
        public string[] ValidOrdersBy { get; protected set; }
    }
}