﻿using BloxTeams.OpenCloud.Models.OAuth;
using System.Text.Json.Serialization;

namespace BloxTeams.OpenCloud.Models.Responses.OAuth
{
    internal class ResourceCreator : IResourceCreator
    {
        [JsonPropertyName("ids")]
        public string[] Ids { get; set; } = [];

        public Models.OAuth.ResourceCreator ToResourceCreatorDto()
            => new()
            {
                Ids = Ids
            };
    }
}
