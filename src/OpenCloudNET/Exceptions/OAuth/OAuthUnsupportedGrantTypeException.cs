﻿namespace OpenCloud.Exceptions.OAuth
{
    public class OAuthUnsupportedGrantTypeException(string? description) : BaseOAuthErrorException(description)
    {
    }
}