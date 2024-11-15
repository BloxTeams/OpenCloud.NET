using BloxTeams.OpenCloud.Enums;
using BloxTeams.OpenCloud.JsonConverters;
using BloxTeams.OpenCloud.Models.OAuth;
using System.Text.Json.Serialization;

namespace BloxTeams.OpenCloud.Models.Responses.OAuth
{
    internal class ResourceOwner : IResourceOwner
    {
        /// <inheritdoc />
        [JsonPropertyName("id")]
        public required ulong Id { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("type")]
        [JsonConverter(typeof(ResourceOwnerTypeJsonConverter))]
        public required ResourceOwnerType Type { get; set; }
    }
}
