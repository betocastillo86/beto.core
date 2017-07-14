//-----------------------------------------------------------------------
// <copyright file="NotificationSettings.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Notifications
{
    /// <summary>
    /// Additional parameters for notifications
    /// </summary>
    public class NotificationSettings
    {
        /// <summary>
        /// Gets or sets the default from name for manual notifications
        /// </summary>
        /// <value>
        /// The default from name.
        /// </value>
        public string DefaultFromName { get; set; }

        /// <summary>
        /// Gets or sets the default subject for manual notifications
        /// </summary>
        /// <value>
        /// The default subject.
        /// </value>
        public string DefaultSubject { get; set; }

        /// <summary>
        /// Gets or sets the default message for manual notifications.
        /// </summary>
        /// <value>
        /// The default message.
        /// </value>
        public string DefaultMessage { get; set; }

        /// <summary>
        /// Gets or sets the site URL. It's mandatory.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        public string SiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the base HTML.
        /// </summary>
        /// <value>
        /// The base HTML.
        /// </value>
        public string BaseHtml { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is manual.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is manual; otherwise, <c>false</c>.
        /// </value>
        public bool IsManual { get; set; }
    }
}