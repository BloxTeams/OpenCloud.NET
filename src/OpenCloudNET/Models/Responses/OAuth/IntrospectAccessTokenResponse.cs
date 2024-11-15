using OpenCloud.Enums;
using OpenCloud.JsonConverters;
using OpenCloud.Models.OAuth;
using System.Text.Json.Serialization;

namespace OpenCloud.Models.Responses.OAuth
{
    internal class IntrospectAccessTokenResponse
    {
        /// <summary>
        /// Indicates if the token is still valid.  Even if the user has revoked the authorization linked to the access token, the endpoint still shows the access token as valid because it checks
        /// whether the token is active based on its lifetime.
        /// </summary>
        [JsonPropertyName("active")]
        public required bool Active { get; set; }

        /// <summary>
        /// JWT ID. <see cref="https://datatracker.ietf.org/doc/html/rfc7519#section-4.1.7"/>.
        /// </summary>
        [JsonPropertyName("jti")]
        public required string JTI { get; set; }

        /// <summary>
        /// Authorization server that issued the token.
        /// </summary>
        [JsonPropertyName("iss")]
        public required string Issuer { get; set; }

        /// <summary>
        /// Authentication scheme supported for this token.
        /// </summary>
        [JsonPropertyName("token_type")]
        public required string Type { get; set; }

        /// <summary>
        /// ID of the client that requested this token.
        /// </summary>
        [JsonPropertyName("client_id")]
        public required string ClientId { get; set; }

        /// <summary>
        /// ID of the client this token is intended for.
        /// </summary>
        [JsonPropertyName("aud")]
        public required string Audience { get; set; }

        /// <summary>
        /// ID of the Roblox user who this token was issued for.
        /// </summary>
        [JsonPropertyName("sub")]
        public required string UserId { get; set; }

        /// <summary>
        /// Scopes allowed for this token.
        /// </summary>
        [JsonPropertyName("scope")]
        [JsonConverter(typeof(RobloxOAuthScopeJsonConverter))]
        public required RobloxOAuthScope Scope { get; set; }

        /// <summary>
        /// Timestamp when this token expires.
        /// </summary>
        [JsonPropertyName("exp")]
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset Expires { get; set; }

        /// <summary>
        /// Timestamp when this token was issued.
        /// </summary>
        [JsonPropertyName("iat")]
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset Issued { get; set; }

        public AccessTokenIntrospection ToAccessTokenDto()
            => new()
            {
                Active = Active,
                JTI = JTI,
                Issuer = Issuer,
                Type = Type,
                ClientId = ClientId,
                Audience = Audience,
                UserId = UserId,
                Scope = Scope,
                Expires = Expires,
                Issued = Issued
            };
    }
}
