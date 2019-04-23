namespace Beto.Core.Data.Notifications
{
    public class DefaultUnsubscriber : IUnsubscriberEntity
    {
        public int Id { get; set; }

        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public bool UnsubscribeEmail { get; set; }

        public bool UnsubscribeMobile { get; set; }
    }
}