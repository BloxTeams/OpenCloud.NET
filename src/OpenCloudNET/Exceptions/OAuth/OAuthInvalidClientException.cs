﻿namespace OpenCloud.Exceptions.OAuth
{
    public class OAuthInvalidClientException(string? description) : BaseOAuthErrorException(description)
    {
    }
}
