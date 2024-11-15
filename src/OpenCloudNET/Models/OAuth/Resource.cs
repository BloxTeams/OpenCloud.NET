namespace BloxTeams.OpenCloud.Models.OAuth
{
    public interface IResource
    {
        public ResourceCreator? Creator { get; set; }
        public ResourceUniverse? Universe { get; set; }
    }

    public class Resource : IResource
    {
        /// <inheritdoc />
        public ResourceCreator? Creator { get; set; }

        /// <inheritdoc />
        public ResourceUniverse? Universe { get; set; }
    }
}
