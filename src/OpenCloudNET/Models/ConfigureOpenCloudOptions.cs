using System.Diagnostics.CodeAnalysis;

namespace OpenCloud.Models
{
    [method:SetsRequiredMembers]
    public partial class ConfigureOpenCloudOptions()
    {
        public required string ClientId { get; set; } = string.Empty;
        public required string ClientSecret { get; set; } = string.Empty;
    }
}
