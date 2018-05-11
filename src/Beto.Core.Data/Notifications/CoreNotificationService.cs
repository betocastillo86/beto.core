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
    using Beto.Core.Data.Users;
    using Beto.Core.EventPublisher;

    /// <summary>
    /// Core Notification Service
    /// </summary>
    /// <seealso cref="Beto.Core.Data.Notifications.ICoreNotificationService" />
    public class CoreNotificationService : ICoreNotificationService
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IDbContext context;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreNotificationService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="publisher">The publisher</param>
        public CoreNotificationService(
            IDbContext context,
            IPublisher publisher)
        {
            this.context = context;
            this.publisher = publisher;
        }

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
        /// <returns>
        /// the task
        /// </returns>
        public async Task NewEmailNotification<TEmailNotification>(
            IList<IUserEntity> users,
            IUserEntity userTriggerEvent,
            INotificationEntity notification,
            string targetUrl,
            IList<NotificationParameter> parameters,
            NotificationSettings settings)
            where TEmailNotification : class, IEmailNotificationEntity, new()
        {
            await this.NewNotification<DefaultSystemNotification, TEmailNotification>(users, userTriggerEvent, notification, targetUrl, parameters, settings);
        }

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
        /// <returns>
        /// the task
        /// </returns>
        public async Task NewNotification<TSystemNotification, TEmailNotification>(IList<IUserEntity> users, IUserEntity userTriggerEvent, INotificationEntity notification, string targetUrl, IList<NotificationParameter> parameters, NotificationSettings settings)
            where TSystemNotification : class, ISystemNotificationEntity, new()
            where TEmailNotification : class, IEmailNotificationEntity, new()
        {
            await this.SaveNewNotification<TSystemNotification, TEmailNotification>(users, userTriggerEvent, notification, targetUrl, parameters, settings);
        }

        /// <summary>
        /// Gets the email notification to add.
        /// </summary>
        /// <typeparam name="TEmailNotification">The type of the email notification.</typeparam>
        /// <param name="notification">The notification.</param>
        /// <param name="user">The user.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="defaultFromName">The default from name.</param>
        /// <param name="defaultSubject">The default subject.</param>
        /// <param name="defaultMessage">The default message.</param>
        /// <param name="siteUrl">The site URL.</param>
        /// <param name="baseHtml">The base HTML.</param>
        /// <param name="isManual">if the notification is manual</param>
        /// <returns>the notification</returns>
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

        /// <summary>
        /// Gets the string formatted.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>the string formatted</returns>
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

        /// <summary>
        /// Saves the new notification.
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
        private async Task SaveNewNotification<TSystemNotification, TEmailNotification>(
            IList<IUserEntity> users,
            IUserEntity userTriggerEvent,
            INotificationEntity notification,
            string targetUrl,
            IList<NotificationParameter> parameters,
            NotificationSettings settings)
            where TSystemNotification : class, ISystemNotificationEntity, new()
            where TEmailNotification : class, IEmailNotificationEntity, new()
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

                    if (notification.IsEmail && !string.IsNullOrWhiteSpace(user.Email))
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
            }
        }
    }
}