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
        Task NewNotification<TSystemNotification, TEmailNotification, TMobileNotification, TUnsubscriber>(
            IList<IUserEntity> users,
            IUserEntity userTriggerEvent,
            INotificationEntity notification,
            string targetUrl,
            IList<NotificationParameter> parameters,
            NotificationSettings settings)
            where TSystemNotification : class, ISystemNotificationEntity, new()
            where TEmailNotification : class, IEmailNotificationEntity, new()
            where TMobileNotification : class, IMobileNotificationEntity, new()
            where TUnsubscriber : class, IUnsubscriberEntity, new();

        Task NewNotification<TSystemNotification, TEmailNotification, TUnsubscriber>(
            IList<IUserEntity> users,
            IUserEntity userTriggerEvent,
            INotificationEntity notification,
            string targetUrl,
            IList<NotificationParameter> parameters,
            NotificationSettings settings)
            where TSystemNotification : class, ISystemNotificationEntity, new()
            where TEmailNotification : class, IEmailNotificationEntity, new()
            where TUnsubscriber : class, IUnsubscriberEntity, new();

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