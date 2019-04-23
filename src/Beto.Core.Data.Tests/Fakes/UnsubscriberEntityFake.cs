namespace Beto.Core.Data.Tests.Fakes
{
    using Beto.Core.Data.Notifications;

    public class UnsubscriberEntityFake : IUnsubscriberEntity
    {
        public int Id { get; set; }

        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public bool UnsubscribeEmail { get; set; }

        public bool UnsubscribeMobile { get; set; }
    }
}