using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenCloud.AspNetCore.Defaults;
using OpenCloud.AspNetCore.Models;

namespace OpenCloud.AspNetCore.Extensions
{
    public static class OpenCloudWebApplicationExtensions
    {
        /// <summary>
        /// Adds routing for OpenCloud endpoints.
        /// </summary>
        /// <param name="app"></param>
        public static void UseRobloxOpenCloudRouting(this WebApplication app)
        {
            ConfigureRobloxAuthenticationOptions authOptions = app.Services.GetRequiredService<ConfigureRobloxAuthenticationOptions>();

            if (authOptions.LogInPath != null)
            {
                app.MapGet(authOptions.LogInPath, async context =>
                {
                    // Prevent authenticated users from logging-in again unless otherwise specified
                    if (context.User.Identity?.IsAuthenticated == true && !authOptions.AllowLogInWhenAuthenticated)
                    {
                        context.Response.Redirect(authOptions.ReturnPath ?? "/");
                        return;
                    }

                    await context.ChallengeAsync(OpenCloudRobloxAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties
                    {
                        RedirectUri = authOptions.ReturnPath
                    });
                });
            }
        }
    }
}
