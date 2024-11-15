using Microsoft.AspNetCore.Authentication;
using BloxTeams.OpenCloud.Enums;

namespace BloxTeams.OpenCloud.AspNetCore.Extensions
{
    public static class OpenCloudAuthenticationPropertiesExtensions
    {
        public static void RequestAdditionalScopes(this AuthenticationProperties authenticationProperties, RobloxOAuthScope scope)
        {
            authenticationProperties.Items.Add("AdditionalScopes", scope.ToString());
        }
    }
}
