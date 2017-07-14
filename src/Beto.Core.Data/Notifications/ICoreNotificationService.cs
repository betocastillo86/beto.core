//-----------------------------------------------------------------------
// <copyright file="ICoreNotificationService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Notifications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Beto.Core.Data.Users;

    /// <summary>
    /// Interface of core notification service
    /// </summary>
    public interface ICoreNotificationService
    {
        /// <summary>
        /// News the notification.
        /// </summary>
        /// <typeparam name="TSystemNotification">The type of the system notification.</typeparam>
        /// <typeparam name="TEmailNotification">The type of the email notification.</typeparam>
        /// <param name="users">The users.</param>
        /// <param name="userTriggerEvent">The user trigger event.</param>
        /// <param name="notification">The notification.</param>
        /// <param name="targetUrl">The target URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>the task</returns>
        Task NewNotification<TSystemNotification, TEmailNotification>(
            IList<IUserEntity> users,
            IUserEntity userTriggerEvent,
            INotificationEntity notification,
            string targetUrl,
            IList<NotificationParameter> parameters,
            NotificationSettings settings) where TSystemNotification : class, ISystemNotificationEntity where TEmailNotification : class, IEmailNotificationEntity;

        /// <summary>
        /// News the email notification.
        /// </summary>
        /// <typeparam name="TEmailNotification">The type of the email notification.</typeparam>
        /// <param name="users">The users.</param>
        /// <param name="userTriggerEvent">The user trigger event.</param>
        /// <param name="notification">The notification.</param>
        /// <param name="targetUrl">The target URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>the task</returns>
        Task NewEmailNotification<TEmailNotification>(
            IList<IUserEntity> users,
            IUserEntity userTriggerEvent,
            INotificationEntity notification,
            string targetUrl,
            IList<NotificationParameter> parameters,
            NotificationSettings settings) where TEmailNotification : class, IEmailNotificationEntity;
    }
}