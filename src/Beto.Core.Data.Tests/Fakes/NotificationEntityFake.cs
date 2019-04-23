//-----------------------------------------------------------------------
// <copyright file="NotificationEntityFake.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Tests.Fakes
{
    using Beto.Core.Data.Notifications;

    public class NotificationEntityFake : INotificationEntity
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public bool IsSystem { get; set; }

        public string SystemText { get; set; }

        public bool IsEmail { get; set; }

        public string EmailSubject { get; set; }

        public string EmailHtml { get; set; }

        public bool IsMobile { get; set; }

        public string MobileText { get; set; }
    }
}