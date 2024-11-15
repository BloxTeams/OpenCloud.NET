using OpenCloud.Exceptions.OAuth;
using OpenCloud.Exceptions.Raw.OAuth;
using System.Net.Http.Json;

namespace OpenCloud.Helpers
{
    internal static class ErrorHandlingHelper
    {
        public static async Task HandleOAuthError(HttpResponseMessage res, HttpRequestException ex)
        {
            RobloxOAuthErrorException oauthEx = await res.Content.ReadFromJsonAsync<RobloxOAuthErrorException>() ??
                throw new Exception("Could not parse JSON");

            throw oauthEx.Error switch
            {
                "invalid_request" => new OAuthInvalidRequestException(oauthEx.Description),
                "invalid_client" => new OAuthInvalidClientException(oauthEx.Description),
                "invalid_grant" => new OAuthInvalidGrantException(oauthEx.Description),
                "unauthorized_client" => new OAuthUnauthorizedClientException(oauthEx.Description),
                "unsupported_grant_type" => new OAuthUnsupportedGrantTypeException(oauthEx.Description),
                "invalid_scope" => new OAuthInvalidScopeException(oauthEx.Description),
                _ => oauthEx,
            };
        }
    }
}
