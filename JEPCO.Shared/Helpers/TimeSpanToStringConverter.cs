﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace JEPCO.Shared.Helpers
{
    public class TimeSpanToStringConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeSpan.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
