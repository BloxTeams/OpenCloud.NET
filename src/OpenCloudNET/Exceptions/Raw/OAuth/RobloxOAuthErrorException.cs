using System.Text.Json.Serialization;

namespace BloxTeams.OpenCloud.Exceptions.Raw.OAuth
{
    internal class RobloxOAuthErrorException : Exception
    {
        public override string Message => "Roblox OAuth operation failed";

        [JsonPropertyName("error")]
        public required string Error { get; set; }

        [JsonPropertyName("error_description")]
        public string? Description { get; set; }

        public Exceptions.RobloxOAuthErrorException ToRobloxOAuthApiErrorDto()
            => new()
            {
                Error = Error,
                Description = Description
            };
    }
}
