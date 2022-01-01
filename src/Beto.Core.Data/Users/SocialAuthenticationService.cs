//-----------------------------------------------------------------------
// <copyright file="SocialAuthenticationService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Data.Users
{
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Social Authentication Service
    /// </summary>
    /// <seealso cref="Beto.Core.Data.Users.ISocialAuthenticationService" />
    public class SocialAuthenticationService : ISocialAuthenticationService
    {
        /// <summary>
        /// Gets the facebook user.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// the authenticated user
        /// </returns>
        public async Task<FacebookUserModel> GetFacebookUser(string token)
        {
            using (var client = new HttpClient())
            {
                var uri = "https://graph.facebook.com/me?fields=id,name,email&access_token=" + token;
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                FacebookUserModel facebookUser = JsonSerializer.Deserialize<FacebookUserModel>(json);
                return facebookUser;
            }
        }
    }
}