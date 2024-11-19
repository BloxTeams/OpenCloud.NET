using BloxTeams.OpenCloud.Exceptions.Raw.OAuth;
using BloxTeams.OpenCloud.Exceptions.OAuth;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace BloxTeams.OpenCloud.Helpers
{
    internal static class ErrorHandlingHelper
    {
        public static async Task HandleOAuthError(HttpResponseMessage res)
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
                "invalid_token" => new OAuthInvalidTokenErrorException(oauthEx.Description),
                "insufficient_scope" => new OAuthInsufficientScopeException(oauthEx.Description),
                _ => oauthEx
            };
        }
    }
}
