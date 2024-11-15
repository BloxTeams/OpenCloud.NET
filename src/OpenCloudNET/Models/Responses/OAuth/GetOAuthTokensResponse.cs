using BloxTeams.OpenCloud.Enums;
using BloxTeams.OpenCloud.JsonConverters;
using BloxTeams.OpenCloud.Models.OAuth;
using System.Text.Json.Serialization;

namespace BloxTeams.OpenCloud.Models.Responses.OAuth
{
    internal class GetOAuthTokensResponse
    {
        [JsonPropertyName("access_token")]
        public required string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public required string RefreshToken { get; set; }

        [JsonPropertyName("token_type")]
        public required string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public required uint ExpiresIn { get; set; }

        [JsonPropertyName("scope")]
        [JsonConverter(typeof(RobloxOAuthScopeJsonConverter))]
        public required RobloxOAuthScope Scope { get; set; }

        public OAuthTokensSet ToOAuthTokensSetDto()
            => new()
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                TokenType = TokenType,
                ExpiresIn = ExpiresIn,
                Scope = Scope
            };
    }
}
