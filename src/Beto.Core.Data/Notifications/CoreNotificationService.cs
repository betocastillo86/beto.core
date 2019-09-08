//-----------------------------------------------------------------------
// <copyright file="CoreNotificationService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Beto.Core.Caching;
    using Beto.Core.Data.Users;
    using Beto.Core.EventPublisher;
    using Beto.Core.Helpers;

    public class CoreNotificationService : ICoreNotificationService
    {
        private readonly IDbContext context;

        private readonly IPublisher publisher;

        private readonly ICacheManager cacheManager;

        public CoreNotificationService(
            IDbContext context,
            IPublisher publisher,
            ICacheManager cacheManager)
        {
            this.context = context;
            this.publisher = publisher;
            this.cacheManager = cacheManager;
        }

        public async Task NewEmailNotification<TEmailNotification>(
            IList<IUserEntity> users,
            IUserEntity userTriggerEvent,
            INotificationEntity notification,
            string targetUrl,
            IList<NotificationParameter> parameters,
            NotificationSettings settings)
            where TEmailNotification : class, IEmailNotificationEntity, new()
        {
            await this.NewNotification<DefaultSystemNotification, TEmailNotification, DefaultUnsubscriber>(users, userTriggerEvent, notification, targetUrl, parameters, settings);
        }

        public async Task NewNotification<TSystemNotification, TEmailNotification, TUnsubscriber>(IList<IUserEntity> users, IUserEntity userTriggerEvent, INotificationEntity notification, string targetUrl, IList<NotificationParameter> parameters, NotificationSettings settings)
            where TSystemNotification : class, ISystemNotificationEntity, new()
            where TEmailNotification : class, IEmailNotificationEntity, new()
            where TUnsubscriber : class, IUnsubscriberEntity, new()
        {
            await this.SaveNewNotification<TSystemNotification, TEmailNotification, DefaultMobileNotification, TUnsubscriber>(users, userTriggerEvent, notification, targetUrl, parameters, settings);
        }

        public async Task NewNotification<TSystemNotification, TEmailNotification, TMobileNotification, TUnsubscriber>(IList<IUserEntity> users, IUserEntity userTriggerEvent, INotificationEntity notification, string targetUrl, IList<NotificationParameter> parameters, NotificationSettings settings)
            where TSystemNotification : class, ISystemNotificationEntity, new()
            where TEmailNotification : class, IEmailNotificationEntity, new()
            where TMobileNotification : class, IMobileNotificationEntity, new()
            where TUnsubscriber : class, IUnsubscriberEntity, new()
        {
            await this.SaveNewNotification<TSystemNotification, TEmailNotification, TMobileNotification, TUnsubscriber>(users, userTriggerEvent, notification, targetUrl, parameters, settings);
        }

        private TEmailNotification GetEmailNotificationToAdd<TEmailNotification>(
           INotificationEntity notification,
           IUserEntity user,
           IList<NotificationParameter> parameters,
           string defaultFromName,
           string defaultSubject,
           string defaultMessage,
           string siteUrl,
           string baseHtml,
           bool isManual)
            where TEmailNotification : class, IEmailNotificationEntity, new()
        {
            string fromName = string.Empty;
            string subject = string.Empty;
            string message = string.Empty;

            ////Cuando es manual envia otros parametros
            if (isManual)
            {
                if (string.IsNullOrEmpty(defaultFromName) || string.IsNullOrEmpty(defaultSubject) || string.IsNullOrEmpty(defaultMessage))
                {
                    throw new ArgumentNullException("fromName or defaultSubject or defaultMessage");
                }

                fromName = defaultFromName;
                subject = defaultSubject;
                message = defaultMessage;
            }
            else
            {
                fromName = defaultFromName;
                subject = this.GetStringFormatted(notification.EmailSubject, parameters);
                message = this.GetStringFormatted(notification.EmailHtml, parameters);
            }

            ////Reemplaza el HTML
            string body = baseHtml
                .Replace("%%Body%%", message)
                .Replace("%%RootUrl%%", siteUrl);

            var emailNotification = new TEmailNotification();
            emailNotification.To = user.Email;
            emailNotification.ToName = user.Name;
            emailNotification.Subject = subject;
            emailNotification.Body = body;
            emailNotification.CreatedDate = DateTime.UtcNow;
            emailNotification.Cc = null;
            emailNotification.ScheduledDate = null;
            emailNotification.SentDate = null;
            return emailNotification;
        }

        private string GetStringFormatted(string value, IList<NotificationParameter> parameters)
        {
            if (parameters == null)
            {
                return value;
            }

            var strValue = new StringBuilder(value);
            foreach (var parameter in parameters)
            {
                strValue.Replace(parameter.Key, parameter.Value);
            }

            return strValue.ToString();
        }

        private async Task SaveNewNotification<TSystemNotification, TEmailNotification, TMobileNotification, TUnsubscriber>(
            IList<IUserEntity> users,
            IUserEntity userTriggerEvent,
            INotificationEntity notification,
            string targetUrl,
            IList<NotificationParameter> parameters,
            NotificationSettings settings)
            where TSystemNotification : class, ISystemNotificationEntity, new()
            where TEmailNotification : class, IEmailNotificationEntity, new()
            where TMobileNotification : class, IMobileNotificationEntity, new()
            where TUnsubscriber : class, IUnsubscriberEntity, new()
        {
            ////En los casos manuales no las busca, sino que quedan quemadas
            ////var notification = type != NotificationType.Manual ? this.GetCachedNotification(type) : new Notification() { Active = true, IsEmail = true };

            if (parameters == null)
            {
                parameters = new List<NotificationParameter>();
            }

            if (notification.Active)
            {
                ////Listado de usuarios a los que no se les envía la notificación
                var usersNotSend = new List<int>();

                ////Se agrega la raiz del sitio
                parameters.Add("RootUrl", settings.SiteUrl);

                ////Asigna por defecto el parametro url el target url
                if (!string.IsNullOrEmpty(targetUrl) && !parameters.Any(c => c.Key.Equals("Url")))
                {
                    if (targetUrl.StartsWith("/"))
                    {
                        string.Concat(settings.SiteUrl, targetUrl);
                    }

                    parameters.Add("Url", targetUrl);
                }

                /////TODO: Agregar antes <<<<<<<<-----------------------------------------------------------
                /////parameters.Add("FacebookUrl", this.generalSettings.FacebookUrl);
                /////parameters.Add("InstagramUrl", this.generalSettings.InstagramUrl);

                if (userTriggerEvent != null)
                {
                    parameters.AddOrReplace("TriggerUser.Name", userTriggerEvent.Name);
                    parameters.AddOrReplace("TriggerUser.Email", userTriggerEvent.Email);
                }

                var systemNotificationsToInsert = new List<TSystemNotification>();
                var emailNotificationsToInsert = new List<TEmailNotification>();
                var mobileNotificationsToInsert = new List<TMobileNotification>();

                int[] emailUnsubscribers = null;
                int[] mobileUnsubscribers = null;

                if (!typeof(TUnsubscriber).Equals(typeof(DefaultUnsubscriber)))
                {
                    emailUnsubscribers = this.GetEmailUnsubscriberByNotificationId<TUnsubscriber>(notification.Id);
                    mobileUnsubscribers = this.GetMobileUnsubscriberByNotificationId<TUnsubscriber>(notification.Id);
                }

                ////Recorre los usuarios a los que debe realizar la notificación

                foreach (var user in users)
                {
                    parameters.AddOrReplace("NotifiedUser.Name", user.Name);
                    parameters.AddOrReplace("NotifiedUser.Email", user.Email);

                    ////Si la notificación es del sistema la envia
                    if (notification.IsSystem)
                    {
                        var systemNotification = new TSystemNotification();
                        systemNotification.UserId = user.Id;
                        systemNotification.Value = this.GetStringFormatted(notification.SystemText, parameters);
                        systemNotification.TargetUrl = targetUrl;
                        systemNotification.CreationDate = DateTime.UtcNow;
                        systemNotification.Seen = false;

                        ////Inserta la notificación de este tipo
                        systemNotificationsToInsert.Add(systemNotification);
                    }

                    if (notification.IsMobile && (user.DeviceId.HasValue || user.IOsDeviceId.HasValue) && (mobileUnsubscribers == null || !mobileUnsubscribers.Contains(user.Id)))
                    {
                        Func<TMobileNotification> getMobileNotification = () => 
                        {
                            var mobileNotification = new TMobileNotification();
                            mobileNotification.UserId = user.Id;
                            mobileNotification.Subject = this.GetStringFormatted(notification.MobileText, parameters);
                            mobileNotification.TargetUrl = targetUrl;
                            mobileNotification.CreatedDate = DateTime.UtcNow;
                            mobileNotification.MessageHash = StringHelpers.ToMd5(mobileNotification.Subject);
                            return mobileNotification;
                        };

                        // Notification for Android
                        if (user.DeviceId.HasValue)
                        {
                            var mobileNotification = getMobileNotification();
                            mobileNotification.DeviceId = user.DeviceId.Value;
                            mobileNotification.IsAndroid = true;
                            mobileNotificationsToInsert.Add(mobileNotification);
                        }

                        // Notification for iOS
                        if (user.IOsDeviceId.HasValue)
                        {
                            var mobileNotification = getMobileNotification();
                            mobileNotification.DeviceId = user.IOsDeviceId.Value;
                            mobileNotificationsToInsert.Add(mobileNotification);
                        }
                    }

                    if (notification.IsEmail && !string.IsNullOrWhiteSpace(user.Email) && (emailUnsubscribers == null || !emailUnsubscribers.Contains(user.Id)))
                    {
                        var emailNotification = this.GetEmailNotificationToAdd<TEmailNotification>(
                            notification,
                            user,
                            parameters,
                            settings.DefaultFromName,
                            settings.DefaultSubject,
                            settings.DefaultMessage,
                            settings.SiteUrl,
                            settings.BaseHtml,
                            settings.IsManual);

                        emailNotificationsToInsert.Add(emailNotification);
                    }
                }

                if (emailNotificationsToInsert.Count > 0)
                {
                    var emailNotificationRepository = this.context.Set<TEmailNotification>();
                    foreach (var entity in emailNotificationsToInsert)
                    {
                        emailNotificationRepository.Add(entity);
                    }

                    await this.context.SaveChangesAsync();
                }

                if (systemNotificationsToInsert.Count > 0)
                {
                    var systemNotificationRepository = this.context.Set<TSystemNotification>();
                    foreach (var entity in systemNotificationsToInsert)
                    {
                        systemNotificationRepository.Add(entity);
                    }

                    await this.context.SaveChangesAsync();

                    await this.publisher.EntitiesInserted(systemNotificationsToInsert);
                }

                if (mobileNotificationsToInsert.Count > 0)
                {
                    var systemNotificationRepository = this.context.Set<TMobileNotification>();
                    foreach (var entity in mobileNotificationsToInsert)
                    {
                        systemNotificationRepository.Add(entity);
                    }

                    await this.context.SaveChangesAsync();

                    await this.publisher.EntitiesInserted(mobileNotificationsToInsert);
                }
            }
        }

        private int[] GetEmailUnsubscriberByNotificationId<TUnsubscriber>(int notificationId) where TUnsubscriber : class, IUnsubscriberEntity, new()
        {
            return this.cacheManager.Get(
                $"cache.core.notifications.unsubscribers.email.{notificationId}", 
                () =>
            {
                return this.context.Set<TUnsubscriber>()
                        .Where(c => c.NotificationId == notificationId && c.UnsubscribeEmail)
                        .Select(c => c.UserId)
                        .ToArray();
            });
        }

        private int[] GetMobileUnsubscriberByNotificationId<TUnsubscriber>(int notificationId) where TUnsubscriber : class, IUnsubscriberEntity, new()
        {
            return this.cacheManager.Get(
                $"cache.core.notifications.unsubscribers.mobile.{notificationId}", 
                () =>
            {
                return this.context.Set<TUnsubscriber>()
                        .Where(c => c.NotificationId == notificationId && c.UnsubscribeMobile)
                        .Select(c => c.UserId)
                        .ToArray();
            });
        }
    }
}