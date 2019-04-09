//-----------------------------------------------------------------------
// <copyright file="INotificationEntity.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Notifications
{
    public interface INotificationEntity : IEntity
    {
        bool Active { get; }

        bool IsSystem { get; }

        bool IsMobile { get; }

        string SystemText { get; }

        string MobileText { get; }

        bool IsEmail { get; }

        string EmailSubject { get; }

        string EmailHtml { get; }
    }
}