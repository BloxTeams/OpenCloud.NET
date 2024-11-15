using OpenCloud.Enums;
using OpenCloud.JsonConverters;
using System.Text.Json.Serialization;

namespace OpenCloud.Models.OAuth
{
    public interface IAccessToken
    {
        /// <summary>
        /// Indicates if the token is still valid.  Even if the user has revoked the authorization linked to the access token, the endpoint still shows the access token as valid because it checks
        /// whether the token is active based on its lifetime.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// JWT ID. <see cref="https://datatracker.ietf.org/doc/html/rfc7519#section-4.1.7"/>.
        /// </summary>
        public string JTI { get; set; }

        /// <summary>
        /// Authorization server that issued the token.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Authentication scheme supported for this token.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// ID of the client that requested this token.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// ID of the client this token is intended for.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// ID of the Roblox user who this token was issued for.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Scopes allowed for this token.
        /// </summary>
        public RobloxOAuthScope Scope { get; set; }

        /// <summary>
        /// Timestamp when this token expires.
        /// </summary>
        public DateTimeOffset Expires { get; set; }

        /// <summary>
        /// Timestamp when this token was issued.
        /// </summary>
        public DateTimeOffset Issued { get; set; }
    }

    public class AccessTokenIntrospection : IAccessToken
    {
        /// <inheritdoc />
        public required bool Active { get; set; }

        /// <inheritdoc />
        public required string JTI { get; set; }

        /// <inheritdoc />
        public required string Issuer { get; set; }

        /// <inheritdoc />
        public required string Type { get; set; }

        /// <inheritdoc />
        public required string ClientId { get; set; }

        /// <inheritdoc />
        public required string Audience { get; set; }

        /// <inheritdoc />
        public required string UserId { get; set; }

        /// <inheritdoc />
        [JsonConverter(typeof(RobloxOAuthScopeJsonConverter))]
        public required RobloxOAuthScope Scope { get; set; }

        /// <inheritdoc />
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public required DateTimeOffset Expires { get; set; }

        /// <inheritdoc />
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public required DateTimeOffset Issued { get; set; }
    }
}
