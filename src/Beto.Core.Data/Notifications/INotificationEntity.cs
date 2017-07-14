//-----------------------------------------------------------------------
// <copyright file="INotificationEntity.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Notifications
{
    /// <summary>
    /// Interface of notification Entity
    /// </summary>
    public interface INotificationEntity
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="INotificationEntity"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        bool Active { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is system.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is system; otherwise, <c>false</c>.
        /// </value>
        bool IsSystem { get; }

        /// <summary>
        /// Gets the system text.
        /// </summary>
        /// <value>
        /// The system text.
        /// </value>
        string SystemText { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is email.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is email; otherwise, <c>false</c>.
        /// </value>
        bool IsEmail { get; }

        /// <summary>
        /// Gets the email subject.
        /// </summary>
        /// <value>
        /// The email subject.
        /// </value>
        string EmailSubject { get; }

        /// <summary>
        /// Gets the email HTML.
        /// </summary>
        /// <value>
        /// The email HTML.
        /// </value>
        string EmailHtml { get; }
    }
}