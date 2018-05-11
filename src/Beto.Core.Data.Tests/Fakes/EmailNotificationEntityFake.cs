﻿//-----------------------------------------------------------------------
// <copyright file="EmailNotificationEntityFake.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Tests.Fakes
{
    using System;
    using Beto.Core.Data.Notifications;

    /// <summary>
    /// Email Notification Entity Fake
    /// </summary>
    /// <seealso cref="Beto.Core.Data.Notifications.IEmailNotificationEntity" />
    public class EmailNotificationEntityFake : IEmailNotificationEntity
    {
        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To email.
        /// </value>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets to name.
        /// </summary>
        /// <value>
        /// To name.
        /// </value>
        public string ToName { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the cc.
        /// </summary>
        /// <value>
        /// The cc.
        /// </value>
        public string Cc { get; set; }

        /// <summary>
        /// Gets or sets the scheduled date.
        /// </summary>
        /// <value>
        /// The scheduled date.
        /// </value>
        public DateTime? ScheduledDate { get; set; }

        /// <summary>
        /// Gets or sets the sent date.
        /// </summary>
        /// <value>
        /// The sent date.
        /// </value>
        public DateTime? SentDate { get; set; }
    }
}