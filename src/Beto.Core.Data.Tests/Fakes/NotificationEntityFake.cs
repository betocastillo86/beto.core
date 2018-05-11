//-----------------------------------------------------------------------
// <copyright file="NotificationEntityFake.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Tests.Fakes
{
    using Beto.Core.Data.Notifications;

    /// <summary>
    /// Notification Entity Fake
    /// </summary>
    /// <seealso cref="Beto.Core.Data.Notifications.INotificationEntity" />
    public class NotificationEntityFake : INotificationEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Beto.Core.Data.Notifications.INotificationEntity" /> is active.
        /// </summary>
        /// <value>
        /// <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is system.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is system; otherwise, <c>false</c>.
        /// </value>
        public bool IsSystem { get; set; }

        /// <summary>
        /// Gets or sets the system text.
        /// </summary>
        /// <value>
        /// The system text.
        /// </value>
        public string SystemText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is email.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is email; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmail { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        /// <value>
        /// The email subject.
        /// </value>
        public string EmailSubject { get; set; }

        /// <summary>
        /// Gets or sets the email HTML.
        /// </summary>
        /// <value>
        /// The email HTML.
        /// </value>
        public string EmailHtml { get; set; }
    }
}