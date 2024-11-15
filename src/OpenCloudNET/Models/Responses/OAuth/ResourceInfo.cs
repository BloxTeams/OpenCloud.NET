using System.Text.Json.Serialization;

namespace BloxTeams.OpenCloud.Models.Responses.OAuth
{
    internal class ResourceInfo
    {
        /// <inheritdoc />
        [JsonPropertyName("owner")]
        public required ResourceOwner Owner { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("resources")]
        public required Resource Resources { get; set; }
    }
}
