namespace OpenCloud.Exceptions.OAuth
{
    public class BaseOAuthErrorException(string? description) : Exception
    {
        public string? Description { get; set; } = description;
    }
}
