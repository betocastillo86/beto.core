//-----------------------------------------------------------------------
// <copyright file="DefaultSystemNotification.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Notifications
{
    using System;

    /// <summary>
    /// Default System Notification
    /// </summary>
    /// <seealso cref="Beto.Core.Data.Notifications.ISystemNotificationEntity" />
    internal class DefaultSystemNotification : ISystemNotificationEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Gets or sets the target URL.
        /// </summary>
        /// <value>
        /// The target URL.
        /// </value>
        public string TargetUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Beto.Core.Data.Notifications.ISystemNotificationEntity" /> is seen.
        /// </summary>
        /// <value>
        /// <c>true</c> if seen; otherwise, <c>false</c>.
        /// </value>
        public bool Seen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}