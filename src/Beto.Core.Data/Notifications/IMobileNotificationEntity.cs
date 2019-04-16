namespace Beto.Core.Data.Notifications
{
    using System;

    public interface IMobileNotificationEntity : IEntity
    {
        int UserId { get; set; }

        Guid DeviceId { get; set; }

        string Subject { get; set; }

        string TargetUrl { get; set; }

        DateTime CreatedDate { get; set; }

        DateTime? SentDate { get; set; }

        string MessageHash { get; set; }
    }
}