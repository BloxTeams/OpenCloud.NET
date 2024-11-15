using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;
using OpenCloud.Enums;

namespace OpenCloud.AspNetCore.Models
{
    public class ConfigureRobloxAuthenticationOptions
    {
        /// <summary>
        /// Path which will redirect users to the Roblox authorization page.
        /// </summary>
        public PathString? LogInPath { get; set; } = null;

        /// <summary>
        /// Path where users will be returned by Roblox.
        /// </summary>
        public PathString CallbackPath { get; set; }

        /// <summary>
        /// Path where users will be returned after authorization is complete.
        /// </summary>
        public PathString? ReturnPath { get; set; } = new("/");
        
        /// <summary>
        /// Controls whether or not authenticated users can access the log-in endpoint. Defaults to <c>false</c>.
        /// </summary>
        public bool AllowLogInWhenAuthenticated { get; set; }

        /// <summary>
        /// Controls whether or not tokens are saved in the authentication ticket.
        /// </summary>
        public bool SaveTokens { get; set; }

        public RobloxOAuthScope Scope { get; set; } = RobloxOAuthScope.OpenId |
            RobloxOAuthScope.Profile;

        public bool UsePkce { get; set; }

        public CookieBuilder CorrelationCookie { get; set; } = new()
        {
            Name = "Correlation.Roblox.",
            IsEssential = true,
            HttpOnly = true,
            SameSite = SameSiteMode.Lax
        };

        public OAuthEvents Events { get; set; } = new();

        public ClaimActionCollection ClaimActions { get; } = [];
    }
}
