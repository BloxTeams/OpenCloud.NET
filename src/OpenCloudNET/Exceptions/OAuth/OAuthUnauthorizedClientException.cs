namespace OpenCloud.Exceptions.OAuth
{
    public class OAuthUnauthorizedClientException(string? description) : BaseOAuthErrorException(description)
    {
    }
}
