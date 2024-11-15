namespace OpenCloud.Exceptions.OAuth
{
    public class OAuthInvalidRequestException(string? description) : BaseOAuthErrorException(description)
    {
    }
}
