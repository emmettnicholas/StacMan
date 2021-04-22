using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StackExchange.StacMan
{
    internal class UnixEpochDateConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var epoch = reader.GetInt64();
            return DateTimeOffset.FromUnixTimeSeconds(epoch).UtcDateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var date = new DateTimeOffset(value);
            var epoch = date.ToUnixTimeSeconds();
            writer.WriteNumberValue(epoch);
        }
    }
}