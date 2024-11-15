using Microsoft.Extensions.Logging;

namespace OpenCloud.Models
{
    /// <summary>
    /// Represents the client to interact with Roblox Open Cloud endpoints
    /// </summary>
    public class OpenCloudClient(ConfigureOpenCloudOptions options, IHttpClientFactory httpClientFactory, ILogger<OpenCloudClient> logger)
    {
        private string ClientId { get; set; } = options.ClientId;
        private string ClientSecret { get; set; } = options.ClientSecret;

        public HttpClient OAuthHttpClient { get; private set; } = httpClientFactory.CreateClient("OpenCloud.OAuth");
        public ILogger<OpenCloudClient> Logger = logger;

        public string GetClientId()
            => ClientId;

        public string GetClientSecret()
            => ClientSecret;
    }
}
