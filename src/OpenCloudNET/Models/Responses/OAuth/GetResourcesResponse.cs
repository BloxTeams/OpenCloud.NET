using System.Text.Json.Serialization;

namespace OpenCloud.Models.Responses.OAuth
{
    internal class GetResourcesResponse
    {
        [JsonPropertyName("resource_infos")]
        public required ResourceInfo[] Resources { get; set; } = [];

        public List<Models.OAuth.ResourceInfo> ToResourcesListDto()
            => Resources.Aggregate(new List<Models.OAuth.ResourceInfo>(), (acc, current) =>
            {
                acc.Add(new Models.OAuth.ResourceInfo
                {
                    Owner = new()
                    {
                        Id = current.Owner.Id,
                        Type = current.Owner.Type,
                    },
                    Resources = current.Resources.ToResourceDto()
                });

                return acc;
            });
    }
}
