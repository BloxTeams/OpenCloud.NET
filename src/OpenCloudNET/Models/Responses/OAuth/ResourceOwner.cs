using OpenCloud.Enums;
using OpenCloud.JsonConverters;
using OpenCloud.Models.OAuth;
using System.Text.Json.Serialization;

namespace OpenCloud.Models.Responses.OAuth
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
