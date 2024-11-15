using OpenCloud.Exceptions.OAuth;
using OpenCloud.Helpers;
using OpenCloud.JsonConverters;
using OpenCloud.Models;
using OpenCloud.Models.OAuth;
using OpenCloud.Models.Responses.OAuth;
using System.Net.Http.Json;
using System.Text.Json;

namespace OpenCloud.Extensions
{
    public static class OAuthTokensExtensions
    {
        /// <summary>
        /// Exchanges an OAuth code for access and refresh tokens
        /// </summary>
        /// <param name="client"></param>
        /// <param name="code">OAuth code returned by Roblox</param>
        /// <param name="codeVerifier">PKCE code verifier if applicable</param>
        /// <returns>Object with tokens information</returns>
        /// <exception cref="OAuthInvalidRequestException"></exception>
        /// <exception cref="OAuthInvalidClientException"></exception>
        /// <exception cref="OAuthInvalidGrantException"></exception>
        /// <exception cref="OAuthUnauthorizedClientException"></exception>
        /// <exception cref="OAuthUnsupportedGrantTypeException"></exception>
        /// <exception cref="OAuthInvalidScopeException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public static async Task<OAuthTokensSet> ExchangeCodeForTokensAsync(this OpenCloudClient client, string code, string? codeVerifier = null)
        {
            Dictionary<string, string> reqContent = new()
            {
                { "client_id", client.GetClientId() },
                { "client_secret", client.GetClientSecret() },
                { "grant_type", "authorization_code" },
                { "code", code },
            };

            if (codeVerifier != null)
            {
                reqContent.Add("code_verifier", codeVerifier);
            }

            HttpRequestMessage req = new(HttpMethod.Post, "oauth/v1/token")
            {
                Content = new FormUrlEncodedContent(reqContent)
            };

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

            GetOAuthTokensResponse resContent = await res.Content.ReadFromJsonAsync<GetOAuthTokensResponse>() ??
                throw new Exception("Could not parse JSON");

            return resContent.ToOAuthTokensSetDto();
        }

        /// <summary>
        /// Obtain a set of tokens with a refresh token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="refreshToken"></param>
        /// <exception cref="OAuthInvalidRequestException"></exception>
        /// <exception cref="OAuthInvalidClientException"></exception>
        /// <exception cref="OAuthInvalidGrantException"></exception>
        /// <exception cref="OAuthUnauthorizedClientException"></exception>
        /// <exception cref="OAuthUnsupportedGrantTypeException"></exception>
        /// <exception cref="OAuthInvalidScopeException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public static async Task<OAuthTokensSet> GetOAuthTokensAsync(this OpenCloudClient client, string refreshToken)
        {
            HttpRequestMessage req = new(HttpMethod.Post, "oauth/v1/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "client_id", client.GetClientId() },
                    { "client_secret", client.GetClientSecret() },
                    { "grant_type", "refresh_token" },
                    { "refresh_token", refreshToken }
                })
            };

            HttpResponseMessage res = await client.OAuthHttpClient.SendAsync(req);

            try
            {
                res.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                // Check if the exception is 400

                if (ex.CouldBeOAuthFail())
                {
                    // RFC 6749 compliant exceptions
                    await ErrorHandlingHelper.HandleOAuthError(res);
                }

                throw;
            }

            JsonSerializerOptions jsonSerializerOptions = new();
            jsonSerializerOptions.Converters.Add(new RobloxOAuthScopeJsonConverter());

            GetOAuthTokensResponse result = await res.Content.ReadFromJsonAsync<GetOAuthTokensResponse>(jsonSerializerOptions) ??
                throw new Exception("Could not parse JSON");

            return result.ToOAuthTokensSetDto();
        }

        /// <summary>
        /// Get information about an access token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="accessToken"></param>
        /// <exception cref="OAuthInvalidRequestException"></exception>
        /// <exception cref="OAuthInvalidClientException"></exception>
        /// <exception cref="OAuthInvalidGrantException"></exception>
        /// <exception cref="OAuthUnauthorizedClientException"></exception>
        /// <exception cref="OAuthUnsupportedGrantTypeException"></exception>
        /// <exception cref="OAuthInvalidScopeException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public static async Task<AccessTokenIntrospection> IntrospectAccessToken(this OpenCloudClient client, string accessToken)
        {
            HttpRequestMessage req = new(HttpMethod.Post, "oauth/v1/token/introspect")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "token", accessToken },
                    { "client_id", client.GetClientId() },
                    { "client_secret", client.GetClientSecret() }
                })
            };

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

            IntrospectAccessTokenResponse token = await res.Content.ReadFromJsonAsync<IntrospectAccessTokenResponse>() ??
                throw new Exception("Could not parse JSON");

            return token.ToAccessTokenDto();
        }

        /// <summary>
        /// Revokes a refresh token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="refreshToken">Refresh token of the set</param>
        /// <exception cref="OAuthInvalidRequestException"></exception>
        /// <exception cref="OAuthInvalidClientException"></exception>
        /// <exception cref="OAuthInvalidGrantException"></exception>
        /// <exception cref="OAuthUnauthorizedClientException"></exception>
        /// <exception cref="OAuthUnsupportedGrantTypeException"></exception>
        /// <exception cref="OAuthInvalidScopeException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public static async Task RevokeRefreshTokenAsync(this OpenCloudClient client, string refreshToken)
        {
            HttpRequestMessage req = new(HttpMethod.Post, "oauth/v1/token/revoke")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "token", refreshToken },
                    { "client_id", client.GetClientId() },
                    { "client_secret", client.GetClientSecret() }
                })
            };

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
        }
    }
}
