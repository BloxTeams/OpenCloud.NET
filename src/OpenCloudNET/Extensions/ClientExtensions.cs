using OpenCloud.Models.Responses.OAuth;
using OpenCloud.Models;
using System.Net.Http.Json;

namespace OpenCloud.Extensions
{
    public static class ClientExtensions
    {
        /// <summary>
        /// Gets resources authorized for the specified access token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="accessToken">Access token obtained for the desired resources.</param>
        /// <returns>A list of resources authorized for the specified access token.</returns>
        public static async Task<List<Models.OAuth.ResourceInfo>> GetResourcesAsync(this OpenCloudClient client, string accessToken)
        {
            HttpRequestMessage req = new(HttpMethod.Post, "oauth/v1/token/resources")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "client_id", client.GetClientId() },
                    { "client_secret", client.GetClientSecret() },
                    { "token", accessToken }
                })
            };

            HttpResponseMessage res = await client.OAuthHttpClient.SendAsync(req);

            res.EnsureSuccessStatusCode();

            GetResourcesResponse resources = await res.Content.ReadFromJsonAsync<GetResourcesResponse>() ??
                throw new Exception("Could not parse JSON");

            return resources.ToResourcesListDto();
        }
    }
}
