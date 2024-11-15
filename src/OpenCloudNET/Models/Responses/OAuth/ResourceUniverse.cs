﻿using OpenCloud.Models.OAuth;
using System.Text.Json.Serialization;

namespace OpenCloud.Models.Responses.OAuth
{
    internal class ResourceUniverse : IResourceUniverse
    {
        /// <inheritdoc />
        [JsonPropertyName("ids")]
        public string[] Ids { get; set; } = [];

        public Models.OAuth.ResourceUniverse ToResourceUniverseDto()
            => new()
            {
                Ids = Ids
            };
    }
}