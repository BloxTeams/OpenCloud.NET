using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenCloud.AspNetCore.Defaults;
using OpenCloud.AspNetCore.Models;
using OpenCloud.Enums;
using OpenCloud.Exceptions.OAuth;
using OpenCloud.JsonConverters;
using OpenCloud.Models;
using System.Collections.Specialized;
using System.Text.Json;
using System.Web;
using OpenCloud.Extensions;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace OpenCloud.AspNetCore.Extensions
{
    public static class OpenCloudAuthenticationBuilderExtensions
    {
        /// <summary>
        /// Adds Roblox OAuth as an authentication scheme. 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddRoblox(this AuthenticationBuilder builder, Action<ConfigureRobloxAuthenticationOptions> configureOptions)
        {
            ConfigureRobloxAuthenticationOptions authOptions = new();
            configureOptions(authOptions);

            // Add auth options to the DI container so they can be used by other methods
            builder.Services.AddSingleton(authOptions);

            IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
            ConfigureOpenCloudOptions openCloudOptions = serviceProvider.GetRequiredService<ConfigureOpenCloudOptions>();
            ILogger<OpenCloudClient> logger = serviceProvider.GetRequiredService<ILogger<OpenCloudClient>>();

            builder.AddOAuth(OpenCloudRobloxAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.AuthorizationEndpoint = "https://apis.roblox.com/oauth/v1/authorize";
                options.CallbackPath = authOptions.CallbackPath;
                options.TokenEndpoint = "https://apis.roblox.com/oauth/v1/token";
                options.UserInformationEndpoint = "https://apis.roblox.com/oauth/v1/userinfo";

                options.UsePkce = authOptions.UsePkce;

                string[] scopeArray = GetScopesAsStringArray(authOptions.Scope);

                for (int i = 0; i < scopeArray.Length; i++)
                {
                    options.Scope.Add(scopeArray[i]);
                }

                options.ClientId = openCloudOptions.ClientId;
                options.ClientSecret = openCloudOptions.ClientSecret;

                options.CorrelationCookie = authOptions.CorrelationCookie;
                options.SaveTokens = authOptions.SaveTokens;

                foreach (ClaimAction action in authOptions.ClaimActions)
                {
                    options.ClaimActions.Add(action);
                }

                Func<RedirectContext<OAuthOptions>, Task> onRedirectToAuthorizationEndpointCallback = authOptions.Events.OnRedirectToAuthorizationEndpoint.Equals(new OAuthOptions().Events.OnRedirectToAuthorizationEndpoint) ? OnRedirectToAuthorizationEndpointDefault : authOptions.Events.OnRedirectToAuthorizationEndpoint;

                options.Events = new()
                {
                    OnCreatingTicket = async context =>
                    {
                        OpenCloudClient client = context.HttpContext.RequestServices.GetRequiredService<OpenCloudClient>();

                        HttpRequestMessage req = new(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);

                        HttpResponseMessage res = await client.OAuthHttpClient.SendAsync(req);

                        try
                        {
                            res.EnsureSuccessStatusCode();
                        }
                        catch (HttpRequestException ex)
                        {
                            if (ex.CouldBeOAuthFail())
                            {
                                await Helpers.ErrorHandlingHelper.HandleOAuthError(res);
                            }
                        }

                        JsonElement user = JsonDocument.Parse(await res.Content.ReadAsStringAsync()).RootElement;

                        context.RunClaimActions(user);

                        await authOptions.Events.OnCreatingTicket.Invoke(context);
                    },
                    OnTicketReceived = authOptions.Events.OnTicketReceived,
                    OnAccessDenied = authOptions.Events.OnAccessDenied,
                    OnRemoteFailure = authOptions.Events.OnRemoteFailure,
                    OnRedirectToAuthorizationEndpoint = onRedirectToAuthorizationEndpointCallback
                };
            });

            return builder;
        }

        private static readonly Func<RedirectContext<OAuthOptions>, Task> OnRedirectToAuthorizationEndpointDefault = context =>
        {
            // Set default scopes

            ConfigureRobloxAuthenticationOptions authOptions = context.HttpContext.RequestServices.GetRequiredService<ConfigureRobloxAuthenticationOptions>();

            // Ensure additional scopes are valid if any

            bool additionalScopesProvided = context.Properties.Items.TryGetValue("AdditionalScopes", out string? additionalScopes);

            RobloxOAuthScope scope = authOptions.Scope;

            if (additionalScopesProvided && additionalScopes != null)
            {
                bool parseSucceeded = Enum.TryParse(additionalScopes, false, out RobloxOAuthScope parsedScope);

                if (!parseSucceeded) throw new OAuthInvalidScopeException("Challenge was provided invalid scopes.");

                // Add default scopes to the updated flags enum
                foreach (RobloxOAuthScope s in Enum.GetValues<RobloxOAuthScope>())
                {
                    if (authOptions.Scope.HasFlag(s) && !parsedScope.HasFlag(s))
                    {
                        parsedScope |= s;
                    }
                }

                // Replace the default scopes with the updated ones
                scope = parsedScope;
            }

            string[] scopeArray = GetScopesAsStringArray(scope);

            // We're rebuilding the scope query param from scratch because context.RedirectUri returns inconsistent results (doesn't include updated scopes)

            Uri redirectUri = new(context.RedirectUri);
            NameValueCollection query = HttpUtility.ParseQueryString(redirectUri.Query);
            query.Set("scope", string.Join(' ', scopeArray));

            context.Response.Redirect($"{context.Options.AuthorizationEndpoint}?{query}");

            return Task.CompletedTask;
        };

        private static string[] GetScopesAsStringArray(RobloxOAuthScope scope)
        {
            JsonSerializerOptions jsonSerializerOptions = new();
            jsonSerializerOptions.Converters.Add(new RobloxOAuthScopeJsonConverter());

            // Serialize the flags enum to a raw Roblox scopes array and then deserialize to a string array
            string[] scopes = JsonSerializer.Deserialize<string[]>(JsonSerializer.Serialize(scope, jsonSerializerOptions)) ??
                throw new Exception("Could not parse JSON");

            return scopes;
        }
    }
}
