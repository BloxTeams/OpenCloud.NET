using BloxTeams.OpenCloud.Enums;
using BloxTeams.OpenCloud.JsonConverters;
using System.Text.Json.Serialization;

namespace BloxTeams.OpenCloud.Models.OAuth
{
    public interface IOAuthTokensSet
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        /// <summary>
        /// Authentication scheme supported for this token.
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// Time in seconds this token expires in
        /// </summary>
        public uint ExpiresIn { get; set; }

        public RobloxOAuthScope Scope { get; set; }
    }

    public class OAuthTokensSet : IOAuthTokensSet
    {
        /// <inheritdoc />
        public required string AccessToken { get; set; }

        /// <inheritdoc />
        public required string RefreshToken { get; set; }

        /// <inheritdoc />
        public required string TokenType { get; set; }

        /// <inheritdoc />
        public required uint ExpiresIn { get; set; }

        /// <inheritdoc />
        [JsonConverter(typeof(RobloxOAuthScopeJsonConverter))]
        public required RobloxOAuthScope Scope { get; set; }
    }
}
