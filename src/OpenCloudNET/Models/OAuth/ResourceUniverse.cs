namespace BloxTeams.OpenCloud.Models.OAuth
{
    public interface IResourceUniverse
    {
        public string[] Ids { get; set; }
    }

    public class ResourceUniverse : IResourceUniverse
    {
        /// <inheritdoc />
        public string[] Ids { get; set; } = [];
    }
}
