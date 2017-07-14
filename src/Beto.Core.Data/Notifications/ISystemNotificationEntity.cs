//-----------------------------------------------------------------------
// <copyright file="ISystemNotificationEntity.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Notifications
{
    using System;

    /// <summary>
    /// Interface of System Notification Entity
    /// </summary>
    public interface ISystemNotificationEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        string Value { get; set; }

        /// <summary>
        /// Gets or sets the target URL.
        /// </summary>
        /// <value>
        /// The target URL.
        /// </value>
        string TargetUrl { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ISystemNotificationEntity"/> is seen.
        /// </summary>
        /// <value>
        ///   <c>true</c> if seen; otherwise, <c>false</c>.
        /// </value>
        bool Seen { get; set; }
    }
}