﻿namespace Beto.Core.Data.Tests.Fakes
{
    using System;
    using Beto.Core.Data.Notifications;

    public class MobileNotificationEntityFake : IMobileNotificationEntity
    {
        public int Id { get; set; }

        public Guid DeviceId { get; set; }

        public string Subject { get; set; }

        public string TargetUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? SentDate { get; set; }

        public int UserId { get; set; }

        public string MessageHash { get; set; }

        public bool IsAndroid { get; set; }
    }
}