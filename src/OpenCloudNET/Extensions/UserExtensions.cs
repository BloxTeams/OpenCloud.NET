using BloxTeams.OpenCloud.Helpers;
using BloxTeams.OpenCloud.Models;
using BloxTeams.OpenCloud.Models.OAuth;
using BloxTeams.OpenCloud.Models.Responses.OAuth;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BloxTeams.OpenCloud.Exceptions.OAuth;

namespace BloxTeams.OpenCloud.Extensions
{
    public static class UserExtensions
    {
        /// <summary>
        /// Gets user info with the specified access token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="accessToken">Access token obtained for the target user</param>
        /// <returns>A user object with information granted by each scope.</returns>
        /// <exception cref="OAuthInvalidRequestException"></exception>
        /// <exception cref="OAuthInvalidClientException"></exception>
        /// <exception cref="OAuthInvalidGrantException"></exception>
        /// <exception cref="OAuthUnauthorizedClientException"></exception>
        /// <exception cref="OAuthUnsupportedGrantTypeException"></exception>
        /// <exception cref="OAuthInvalidScopeException"></exception>
        /// <exception cref="OAuthInsufficientScopeException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public static async Task<User> GetUserInfoAsync(this OpenCloudClient client, string accessToken)
        {
            HttpRequestMessage req = new(HttpMethod.Get, "oauth/v1/userinfo");
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage res = await client.OAuthHttpClient.SendAsync(req);

            try
            {
                res.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                if (ex.CouldBeOAuthFail())
                {
                    await ErrorHandlingHelper.HandleOAuthError(res);
                }
            }

            GetUserInfoResponse user = await res.Content.ReadFromJsonAsync<GetUserInfoResponse>() ??
                throw new Exception("Could not parse JSON");

            return user.ToUserDto();
        }
    }
}
