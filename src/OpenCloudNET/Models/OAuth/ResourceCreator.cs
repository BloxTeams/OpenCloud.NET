namespace BloxTeams.OpenCloud.Models.OAuth
{
    public interface IResourceCreator
    {
        public string[] Ids { get; set; }
    }

    public class ResourceCreator : IResourceCreator
    {
        /// <inheritdoc />
        public required string[] Ids { get; set; }
    }
}
