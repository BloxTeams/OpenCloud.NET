using BloxTeams.OpenCloud.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BloxTeams.OpenCloud.JsonConverters
{
    public class ResourceOwnerTypeJsonConverter : JsonConverter<ResourceOwnerType>
    {
        public override ResourceOwnerType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string rawType = reader.GetString()!;
            Enum.TryParse(rawType, true, out ResourceOwnerType type);

            return type;
        }

        public override void Write(Utf8JsonWriter writer, ResourceOwnerType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().ToLowerInvariant());
        }
    }
}
