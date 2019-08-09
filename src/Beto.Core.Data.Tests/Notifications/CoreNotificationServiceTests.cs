//-----------------------------------------------------------------------
// <copyright file="CoreNotificationServiceTests.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Tests.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Caching;
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
        private CoreNotificationService coreNotificationService;

        private Mock<IDbContext> context;

        private IList<IUserEntity> defaultListUser;

        private NotificationEntityFake defaultNotification;

        private Mock<DbSet<EmailNotificationEntityFake>> emailNotificationRepository;

        private NotificationSettings notificationSettings;

        private Mock<IPublisher> publisher;

        private Mock<DbSet<SystemNotificationEntityFake>> systemNotificationRepository;

        private Mock<DbSet<MobileNotificationEntityFake>> mobileNotificationRepository;

        private Mock<DbSet<UnsubscriberEntityFake>> unsubscriberRepository;

        private UserEntityFake user;

        private int notificationId = 1;

        private Mock<ICacheManager> cacheManager;

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

            Assert.That(() => this.CallDefaultNewNotification<DefaultUnsubscriber>(), Throws.ArgumentNullException);
        }

        [Test]
        public async Task NewNotification_ManualNotification_ValidStrings()
        {
            this.notificationSettings.IsManual = true;
            this.notificationSettings.DefaultMessage = "manual";
            this.notificationSettings.DefaultSubject = "subject";
            this.notificationSettings.DefaultFromName = "from";

            await this.CallDefaultNewNotification<DefaultUnsubscriber>();

            this.emailNotificationRepository.Verify(c => c.Add(
                                            It.Is<EmailNotificationEntityFake>(x =>
                                                                    x.Body.Equals("basehtml " + this.notificationSettings.DefaultMessage) &&
                                                                    x.Subject.Equals(this.notificationSettings.DefaultSubject))));
        }

        [Test]
        public async Task NewNotification_NoEmail_OneCallOnSystem()
        {
            this.defaultNotification.IsEmail = false;

            await this.CallDefaultNewNotification<DefaultUnsubscriber>();

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()), Times.Never);
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()));
            this.mobileNotificationRepository.Verify(c => c.Add(It.IsAny<MobileNotificationEntityFake>()), Times.Exactly(2));
        }

        [Test]
        public async Task NewNotification_NoSystem_OneCallOnEmail()
        {
            this.defaultNotification.IsSystem = false;

            await this.CallDefaultNewNotification<DefaultUnsubscriber>();

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()));
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()), Times.Never);
            this.mobileNotificationRepository.Verify(c => c.Add(It.IsAny<MobileNotificationEntityFake>()), Times.Exactly(2));
        }

        [Test]
        public async Task NewNotification_NoMobile_DontSendMobileNotification()
        {
            this.defaultNotification.IsMobile = false;

            await this.CallDefaultNewNotification<DefaultUnsubscriber>();

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()));
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()));
            this.mobileNotificationRepository.Verify(c => c.Add(It.IsAny<MobileNotificationEntityFake>()), Times.Never);
        }

        [Test]
        public async Task NewNotification_WithoutDevice_DontSendMobileNotification()
        {
            this.user.DeviceId = null;
            this.user.IOsDeviceId = null;

            await this.CallDefaultNewNotification<DefaultUnsubscriber>();

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()));
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()));
            this.mobileNotificationRepository.Verify(c => c.Add(It.IsAny<MobileNotificationEntityFake>()), Times.Never);
        }

        [Test]
        public async Task NewNotification_WithoutAndroidDevice_SendOnlyiOsNotification()
        {
            this.user.DeviceId = null;

            await this.CallDefaultNewNotification<DefaultUnsubscriber>();

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()));
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()));
            this.mobileNotificationRepository.Verify(c => c.Add(It.Is<MobileNotificationEntityFake>(x => x.DeviceId == this.user.IOsDeviceId && !x.IsAndroid)), Times.Once);
        }

        [Test]
        public async Task NewNotification_WithoutiOsDevice_SendOnlyAndroidNotification()
        {
            this.user.IOsDeviceId = null;

            await this.CallDefaultNewNotification<DefaultUnsubscriber>();

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()));
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()));
            this.mobileNotificationRepository.Verify(c => c.Add(It.Is<MobileNotificationEntityFake>(x => x.DeviceId == this.user.DeviceId && x.IsAndroid)), Times.Once);
        }

        [Test]
        public async Task NewNotification_UnsubscriberFoundMobile_DontSendMobile()
        {
            this.SetupCacheUnsubscribers(this.notificationId, "mobile", new int[] { this.user.Id });
            this.SetupCacheUnsubscribers(this.notificationId, "email", new int[] { });

            await this.CallDefaultNewNotification<UnsubscriberEntityFake>();

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()));
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()));
            this.mobileNotificationRepository.Verify(c => c.Add(It.IsAny<MobileNotificationEntityFake>()), Times.Never);
        }

        [Test]
        public async Task NewNotification_UnsubscriberFoundEmail_DontSendEmail()
        {
            this.SetupCacheUnsubscribers(this.notificationId, "mobile", new int[] { });
            this.SetupCacheUnsubscribers(this.notificationId, "email", new int[] { this.user.Id });

            await this.CallDefaultNewNotification<UnsubscriberEntityFake>();

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()), Times.Never);
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()));
            this.mobileNotificationRepository.Verify(c => c.Add(It.IsAny<MobileNotificationEntityFake>()));
        }

        [Test]
        public async Task NewNotification_OtherUnsubscriberFoundEmail_SendEmailAndMobile()
        {
            var otherUserId = 3000;

            this.SetupCacheUnsubscribers(this.notificationId, "mobile", new int[] { otherUserId });
            this.SetupCacheUnsubscribers(this.notificationId, "email", new int[] { otherUserId });

            await this.CallDefaultNewNotification<UnsubscriberEntityFake>();

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()));
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()));
            this.mobileNotificationRepository.Verify(c => c.Add(It.IsAny<MobileNotificationEntityFake>()));
        }

        [Test]
        public async Task NewNotification_ValidateMessageNotificationsEmail_ValidStrings()
        {
            await this.CallDefaultNewNotification<DefaultUnsubscriber>();

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

        [Test]
        public async Task NewNotification_ValidateMessageNotificationsSystem_ValidStrings()
        {
            await this.CallDefaultNewNotification<DefaultUnsubscriber>();

            var systemResponse = new SystemNotificationEntityFake
            {
                Value = "name systemtext"
            };

            this.systemNotificationRepository.Verify(c => c.Add(
                                            It.Is<SystemNotificationEntityFake>(x =>
                                                                    x.Value.Equals(systemResponse.Value))));
        }

        [Test]
        public async Task NewNotification_WhenIsCallSave_SameNumberOfCallsAndUsers()
        {
            var timesCalled = 3;

            var users = this.GetFakeUsers(timesCalled);

            await this.CallDefaultNewNotification<DefaultUnsubscriber>(users.Select(c => (IUserEntity)c).ToList());

            this.emailNotificationRepository.Verify(c => c.Add(It.IsAny<EmailNotificationEntityFake>()), Times.Exactly(timesCalled));
            this.systemNotificationRepository.Verify(c => c.Add(It.IsAny<SystemNotificationEntityFake>()), Times.Exactly(timesCalled));
        }

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
                Id = this.notificationId,
                Active = true,
                EmailHtml = "%%NotifiedUser.Name%% emailhtml",
                EmailSubject = "%%NotifiedUser.Name%% emailsubject",
                IsEmail = true,
                IsSystem = true,
                SystemText = "%%NotifiedUser.Name%% systemtext",
                IsMobile = true,
                MobileText = "%%NotifiedUser.Name%% mobile"
            };

            this.user = new UserEntityFake
            {
                Name = "name",
                Email = "email",
                Id = 1,
                DeviceId = Guid.NewGuid(),
                IOsDeviceId = Guid.NewGuid()
            };

            this.emailNotificationRepository = new Mock<DbSet<EmailNotificationEntityFake>>();
            this.systemNotificationRepository = new Mock<DbSet<SystemNotificationEntityFake>>();
            this.mobileNotificationRepository = new Mock<DbSet<MobileNotificationEntityFake>>();
            this.unsubscriberRepository = new Mock<DbSet<UnsubscriberEntityFake>>();

            this.context = new Mock<IDbContext>();
            this.context.Setup(c => c.Set<EmailNotificationEntityFake>())
                .Returns(() => this.emailNotificationRepository.Object);
            this.context.Setup(c => c.Set<SystemNotificationEntityFake>())
                .Returns(() => this.systemNotificationRepository.Object);
            this.context.Setup(c => c.Set<MobileNotificationEntityFake>())
                .Returns(() => this.mobileNotificationRepository.Object);
            this.context.Setup(c => c.Set<UnsubscriberEntityFake>())
                .Returns(() => this.unsubscriberRepository.Object);

            this.publisher = new Mock<IPublisher>();
            this.cacheManager = new Mock<ICacheManager>();

            this.defaultListUser = new List<IUserEntity> { this.user };

            this.coreNotificationService = new CoreNotificationService(this.context.Object, this.publisher.Object, this.cacheManager.Object);
        }

        protected void SetupCacheUnsubscribers(int notificationId, string type, int[] unsubscribers)
        {
            var cacheKey = $"cache.core.notifications.unsubscribers.{type}.{notificationId}";
            this.cacheManager.Setup(c => c.IsSet(cacheKey)).Returns(() => true);
            this.cacheManager.Setup(c => c.Get<int[]>(cacheKey))
                .Returns(() => unsubscribers);
        }

        private async Task CallDefaultNewNotification<TUnsubscriber>(IList<IUserEntity> users = null) where TUnsubscriber : class, IUnsubscriberEntity, new()
        {
            await this.coreNotificationService.NewNotification<SystemNotificationEntityFake, EmailNotificationEntityFake, MobileNotificationEntityFake, TUnsubscriber>(
               users ?? this.defaultListUser,
               null,
               this.defaultNotification,
               string.Empty,
               null,
               this.notificationSettings);
        }

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