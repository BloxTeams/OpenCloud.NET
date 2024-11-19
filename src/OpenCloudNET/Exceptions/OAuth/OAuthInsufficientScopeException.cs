namespace BloxTeams.OpenCloud.Exceptions.OAuth
{
    public class OAuthInsufficientScopeException(string? description) : BaseOAuthErrorException(description)
    {
    }
}
