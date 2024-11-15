namespace OpenCloud.Exceptions.OAuth
{
    public class OAuthInvalidTokenErrorException(string? description) : BaseOAuthErrorException(description)
    {
    }
}
