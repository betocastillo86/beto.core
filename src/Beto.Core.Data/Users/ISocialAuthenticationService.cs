//-----------------------------------------------------------------------
// <copyright file="ISocialAuthenticationService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Users
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface of social authentication
    /// </summary>
    public interface ISocialAuthenticationService
    {
        /// <summary>
        /// Gets the facebook user.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>the authenticated user</returns>
        Task<FacebookUserModel> GetFacebookUser(string token);
    }
}