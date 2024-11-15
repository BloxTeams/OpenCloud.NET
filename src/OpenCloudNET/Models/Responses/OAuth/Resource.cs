using System.Text.Json.Serialization;

namespace OpenCloud.Models.Responses.OAuth
{
    internal class Resource
    {
        /// <inheritdoc />
        [JsonPropertyName("creator")]
        public ResourceCreator? Creator { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("universe")]
        public ResourceUniverse? Universe { get; set; }

        public Models.OAuth.Resource ToResourceDto()
            => new()
            {
                Creator = Creator?.ToResourceCreatorDto(),
                Universe = Universe?.ToResourceUniverseDto()
            };
    }
}
