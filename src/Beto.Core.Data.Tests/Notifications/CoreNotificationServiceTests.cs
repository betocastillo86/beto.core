//-----------------------------------------------------------------------
// <copyright file="CoreNotificationServiceTests.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Tests.Notifications
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data.Notifications;
    using Beto.Core.Data.Tests.Fakes;
    using Beto.Core.Data.Users;
    using Beto.Core.EventPublisher;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Core Notification Service Tests
    /// </summary>
    [TestFixture]
    public class CoreNotificationServiceTests
    {
        /// <summary>
        /// The core notification service
        /// </summary>
        private CoreNotificationService coreNotificationService;

        /// <summary>
        /// The database context
        /// </summary>
        private Mock<IDbContext> context;

        /// <summary>
        /// The default list user
        /// </summary>
        private IList<IUserEntity> defaultListUser;

        /// <summary>
        /// The default notification
        /// </summary>
        private NotificationEntityFake defaultNotification;

        /// <summary>
        /// The email notification repository
        /// </summary>
        private Mock<DbSet<EmailNotificationEntityFake>> emailNotificationRepository;

        /// <summary>
        /// The notification settings
        /// </summary>
        private NotificationSettings notificationSettings;

        /// <summary>
        /// The publisher
        /// </summary>
        private Mock<IPublisher> publisher;

        /// <summary>
        /// The system notification repository
        /// </summary>
        private Mock<DbSet<SystemNotificationEntityFake>> systemNotificationRepository;

        /// <summary>
        /// The user
        /// </summary>
        private UserEntityFake user;

        /// <summary>
        /// News the notification manual notification throws argument null exception.
        /// </summary>
        /// <param name="defaultMessage">The default message.</param>
        /// <param name="defaultSubject">The default subject.</param>
        /// <param name="defaultFromName">The default from name.</param>
        [Test]
        [TestCase("message", "subject", null)]
        [TestCase("message", null, "name")]
        [TestCase(null, "subject", "name")]
        public void NewNotification_ManualNotification_ThrowsArgumentNullException(string defaultMessage, string defaultSubject, string defaultFromName)
        {
            this.notificationSettings.IsManual = true;
            this.notificationSettings.DefaultMessage = defaultMessage;
            this.notificationSettings.DefaultSubject = defaultSubject;
            this.notificationSettings.DefaultFromName = defaultFromName;

            Assert.That(() => this.CallDefaultNewNotification(), Throws.ArgumentNullException);
        }

        /// <summary>
        /// News the notification manual notification valid strings.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task NewNotification_ManualNotification_ValidStrings()
        {
            this.notificationSettings.IsManual = true;
            this.notificationSettings.DefaultMessage = "manual";
            this.notificationSettings.DefaultSubject = "subject";
            this.notificationSettings.DefaultFromName = "from";

            await this.CallDefaultNewNotification();

            this.emailNotificationRepository.Verify(c => c.Add(
                                            It.Is<EmailNotificationEntityFake>(x =>
                                                                    x.Body.Equals("basehtml " + this.notificationSettings.DefaultMessage) &&
                                                                    x.Subject.Equals(this.notificationSettings.DefaultSubject))));
        }

        /// <summary>
        /// News the notification no email one call on system.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task NewNotification_NoEmail_OneCallOnSystem()
        {
            this.defaultNotification.IsEmail = false;

            await this.CallDefaultNewNotification();

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()), Times.Never);
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()));
        }

        /// <summary>
        /// News the notification no system one call on email.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task NewNotification_NoSystem_OneCallOnEmail()
        {
            this.defaultNotification.IsSystem = false;

            await this.CallDefaultNewNotification();

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()));
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()), Times.Never);
        }

        /// <summary>
        /// News the notification validate message notifications email valid strings.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task NewNotification_ValidateMessageNotificationsEmail_ValidStrings()
        {
            await this.CallDefaultNewNotification();

            var emailResponse = new EmailNotificationEntityFake
            {
                Body = "basehtml name emailhtml",
                Subject = "name emailsubject",
                ToName = this.user.Name,
                To = this.user.Email
            };

            this.emailNotificationRepository.Verify(c => c.Add(
                                            It.Is<EmailNotificationEntityFake>(x =>
                                                                    x.Body.Equals(emailResponse.Body) &&
                                                                    x.To.Equals(emailResponse.To) &&
                                                                    x.ToName.Equals(emailResponse.ToName) &&
                                                                    x.Subject.Equals(emailResponse.Subject))));
        }

        /// <summary>
        /// News the notification validate message notifications system valid strings.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task NewNotification_ValidateMessageNotificationsSystem_ValidStrings()
        {
            await this.CallDefaultNewNotification();

            var systemResponse = new SystemNotificationEntityFake
            {
                Value = "name systemtext"
            };

            this.systemNotificationRepository.Verify(c => c.Add(
                                            It.Is<SystemNotificationEntityFake>(x =>
                                                                    x.Value.Equals(systemResponse.Value))));
        }

        /// <summary>
        /// News the notification when is call save same number of calls and users.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task NewNotification_WhenIsCallSave_SameNumberOfCallsAndUsers()
        {
            var timesCalled = 3;

            var users = this.GetFakeUsers(timesCalled);

            await this.CallDefaultNewNotification(users.Select(c => (IUserEntity)c).ToList());

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()), Times.Exactly(timesCalled));
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()), Times.Exactly(timesCalled));
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.notificationSettings = new NotificationSettings
            {
                BaseHtml = "basehtml %%Body%%",
                DefaultFromName = "defaultfromname",
                DefaultMessage = "defaultmessage",
                DefaultSubject = "defaultsubject",
                IsManual = false,
                SiteUrl = "siteurl"
            };

            this.defaultNotification = new NotificationEntityFake
            {
                Active = true,
                EmailHtml = "%%NotifiedUser.Name%% emailhtml",
                EmailSubject = "%%NotifiedUser.Name%% emailsubject",
                IsEmail = true,
                IsSystem = true,
                SystemText = "%%NotifiedUser.Name%% systemtext"
            };

            this.user = new UserEntityFake
            {
                Name = "name",
                Email = "email",
                Id = 1
            };

            this.emailNotificationRepository = new Mock<DbSet<EmailNotificationEntityFake>>();
            this.systemNotificationRepository = new Mock<DbSet<SystemNotificationEntityFake>>();

            this.context = new Mock<IDbContext>();
            this.context.Setup(c => c.Set<EmailNotificationEntityFake>())
                .Returns(() => this.emailNotificationRepository.Object);
            this.context.Setup(c => c.Set<SystemNotificationEntityFake>())
                .Returns(() => this.systemNotificationRepository.Object);

            this.publisher = new Mock<IPublisher>();

            this.defaultListUser = new List<IUserEntity> { this.user };

            this.coreNotificationService = new CoreNotificationService(this.context.Object, this.publisher.Object);
        }

        /// <summary>
        /// Calls the default new notification.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <returns>the task</returns>
        private async Task CallDefaultNewNotification(IList<IUserEntity> users = null)
        {
            await this.coreNotificationService.NewNotification<SystemNotificationEntityFake, EmailNotificationEntityFake>(
               users ?? this.defaultListUser,
               null,
               this.defaultNotification,
               string.Empty,
               null,
               this.notificationSettings);
        }

        /// <summary>
        /// Gets the fake users.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>the tasks</returns>
        private List<UserEntityFake> GetFakeUsers(int count = 1)
        {
            var users = new List<UserEntityFake>();

            for (int i = 0; i < count; i++)
            {
                users.Add(new UserEntityFake { Id = i, Email = "email" + i, Name = "name" + i });
            }

            return users;
        }
    }
}