using Microsoft.AspNetCore.Authentication;
using OpenCloud.Enums;

namespace OpenCloud.AspNetCore.Extensions
{
    public static class OpenCloudAuthenticationPropertiesExtensions
    {
        public static void RequestAdditionalScopes(this AuthenticationProperties authenticationProperties, RobloxOAuthScope scope)
        {
            authenticationProperties.Items.Add("AdditionalScopes", scope.ToString());
        }
    }
}
