namespace OpenCloud.Exceptions.OAuth
{
    public class OAuthInvalidScopeException(string? description) : BaseOAuthErrorException(description)
    {
    }
}
