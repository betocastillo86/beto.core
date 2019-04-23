namespace Beto.Core.Data.Notifications
{
    public interface IUnsubscriberEntity : IEntity
    {
        int Id { get; set; }

        int NotificationId { get; set; }

        int UserId { get; set; }

        bool UnsubscribeEmail { get; set; }

        bool UnsubscribeMobile { get; set; }
    }
}