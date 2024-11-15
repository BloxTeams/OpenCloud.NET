using OpenCloud.JsonConverters;
using OpenCloud.Models.OAuth;
using System.Text.Json.Serialization;

namespace OpenCloud.Models.Responses.OAuth
{
    internal class GetUserInfoResponse : IUser
    {
        /// <inheritdoc />
        [JsonPropertyName("sub")]
        public required string UserId { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("name")]
        public string? DisplayName { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("nickname")]
        public string? Nickname { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("preferred_username")]
        public string? Username { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("profile")]
        public string? ProfileUrl { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("picture")]
        public string? ProfilePicture { get; set; }

        public User ToUserDto()
            => new()
            {
                UserId = UserId,
                DisplayName = DisplayName,
                Nickname = Nickname,
                Username = Username,
                ProfileUrl = ProfileUrl,
                ProfilePicture = ProfilePicture,
                Created = Created
            };
    }
}
