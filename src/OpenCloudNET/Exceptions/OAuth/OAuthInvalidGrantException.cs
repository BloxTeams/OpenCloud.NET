namespace BloxTeams.OpenCloud.Exceptions.OAuth
{
    public class OAuthInvalidGrantException(string? description) : BaseOAuthErrorException(description)
    {
    }
}
