using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenCloud.JsonConverters
{
    /// <summary>
    /// Converts unix timestamps to <see cref="DateTimeOffset"/>.
    /// </summary>
    public class DateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            long unix = reader.GetInt64();
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unix);

            return dateTimeOffset;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
