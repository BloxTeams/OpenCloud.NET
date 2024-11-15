namespace BloxTeams.OpenCloud.Models.OAuth
{
    public interface IResourceInfo
    {
        public ResourceOwner Owner { get; set; }
        public Resource Resources { get; set; }
    }

    public class ResourceInfo : IResourceInfo
    {
        /// <inheritdoc />
        public required ResourceOwner Owner { get; set; }

        /// <inheritdoc />
        public required Resource Resources { get; set; }
    }
}
