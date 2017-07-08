//-----------------------------------------------------------------------
// <copyright file="ApiErrorModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Web.Api
{
    using System.Collections.Generic;

    /// <summary>
    /// Error model for <![CDATA[Api]]>
    /// </summary>
    public class ApiErrorModel
    {
        /// <summary>
        /// The details
        /// </summary>
        private IList<ApiErrorModel> details;

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public IList<ApiErrorModel> Details
        {
            get
            {
                return this.details ?? (this.details = new List<ApiErrorModel>());
            }

            set
            {
                this.details = value;
            }
        }
    }
}