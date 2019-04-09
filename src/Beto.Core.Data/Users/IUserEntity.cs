//-----------------------------------------------------------------------
// <copyright file="IUserEntity.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Users
{
    using System;

    public interface IUserEntity : IEntity
    {
        int Id { get; }

        string Name { get; }

        string Email { get; }

        Guid? DeviceId { get; }
    }
}