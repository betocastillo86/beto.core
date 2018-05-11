//-----------------------------------------------------------------------
// <copyright file="UserEntityFake.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Tests.Fakes
{
    using Beto.Core.Data.Users;

    /// <summary>
    /// User Entity Fake
    /// </summary>
    /// <seealso cref="Beto.Core.Data.Users.IUserEntity" />
    public class UserEntityFake : IUserEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
    }
}