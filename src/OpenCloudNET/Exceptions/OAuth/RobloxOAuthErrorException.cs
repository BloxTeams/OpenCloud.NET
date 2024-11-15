namespace BloxTeams.OpenCloud.Exceptions
{
    public class RobloxOAuthErrorException
    {
        public required string Error { get; set; }
        public string? Description { get; set; }
    }
}
