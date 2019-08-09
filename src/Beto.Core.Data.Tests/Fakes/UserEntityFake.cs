//-----------------------------------------------------------------------
// <copyright file="UserEntityFake.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Tests.Fakes
{
    using System;
    using Beto.Core.Data.Users;

    public class UserEntityFake : IUserEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Guid? DeviceId { get; set; }

        public Guid? IOsDeviceId { get; set; }
    }
}