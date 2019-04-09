//-----------------------------------------------------------------------
// <copyright file="ICoreNotificationService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Notifications
{
    using Beto.Core.Data.Users;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface of core notification service
    /// </summary>
    public interface ICoreNotificationService
    {
        Task NewNotification<TSystemNotification, TEmailNotification, TMobileNotification>(
            IList<IUserEntity> users,
            IUserEntity userTriggerEvent,
            INotificationEntity notification,
            string targetUrl,
            IList<NotificationParameter> parameters,
            NotificationSettings settings)
            where TSystemNotification : class, ISystemNotificationEntity, new()
            where TEmailNotification : class, IEmailNotificationEntity, new()
            where TMobileNotification : class, IMobileNotificationEntity, new();

        Task NewNotification<TSystemNotification, TEmailNotification>(
            IList<IUserEntity> users,
            IUserEntity userTriggerEvent,
            INotificationEntity notification,
            string targetUrl,
            IList<NotificationParameter> parameters,
            NotificationSettings settings)
            where TSystemNotification : class, ISystemNotificationEntity, new()
            where TEmailNotification : class, IEmailNotificationEntity, new();

        Task NewEmailNotification<TEmailNotification>(
            IList<IUserEntity> users,
            IUserEntity userTriggerEvent,
            INotificationEntity notification,
            string targetUrl,
            IList<NotificationParameter> parameters,
            NotificationSettings settings)
            where TEmailNotification : class, IEmailNotificationEntity, new();
    }
}