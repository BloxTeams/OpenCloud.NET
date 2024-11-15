using OpenCloud.Enums;
using OpenCloud.JsonConverters;
using System.Text.Json.Serialization;

namespace OpenCloud.Models.OAuth
{
    public interface IResourceOwner
    {
        /// <summary>
        /// Roblox ID of the entity owning the resource.
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// Type of the resouce owner.
        /// </summary>
        public ResourceOwnerType Type { get; set; }
    }

    public class ResourceOwner : IResourceOwner
    {
        /// <inheritdoc />
        public ulong Id { get; set; }

        /// <inheritdoc />
        [JsonConverter(typeof(ResourceOwnerTypeJsonConverter))]
        public ResourceOwnerType Type { get; set; }
    }
}
