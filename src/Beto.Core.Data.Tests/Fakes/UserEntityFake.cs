//-----------------------------------------------------------------------
// <copyright file="UserEntityFake.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Tests.Fakes
{
    using Beto.Core.Data.Users;
    using System;

    public class UserEntityFake : IUserEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Guid? DeviceId { get; set; }
    }
}