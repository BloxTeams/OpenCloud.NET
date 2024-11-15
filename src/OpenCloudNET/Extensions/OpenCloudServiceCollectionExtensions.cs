using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BloxTeams.OpenCloud.Models;

namespace BloxTeams.OpenCloud.Extensions
{
    public static class OpenCloudServiceCollectionExtensions
    {
        public static IServiceCollection AddRobloxOpenCloud(this IServiceCollection services, Action<ConfigureOpenCloudOptions> configureOptions)
        {
            services
                .AddHttpClient("OpenCloud.OAuth", client =>
                {
                    client.BaseAddress = new Uri("https://apis.roblox.com/oauth");
                });

            ConfigureOpenCloudOptions options = new();
            configureOptions(options);

            services.AddSingleton(options);

            services.AddScoped(serviceProvider =>
            {
                IHttpClientFactory httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                ILogger<OpenCloudClient> logger = serviceProvider.GetRequiredService<ILogger<OpenCloudClient>>();

                return new OpenCloudClient(options, httpClientFactory, logger);
            });

            return services;
        }
    }
}
