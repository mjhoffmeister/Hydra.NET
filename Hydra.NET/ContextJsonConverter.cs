using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Hydra.NET
{
    public class ContextJsonConverter : JsonConverter<Context>
    {
        public override Context ReadJson(
            JsonReader reader, 
            Type objectType, 
            [AllowNull] Context existingValue, 
            bool hasExistingValue, 
            JsonSerializer serializer)
        {
            // TODO: find a way to conditionally deserialize without relying on exceptions
            try
            {
                // Deserialize context mappings

                var mappings = serializer.Deserialize<Dictionary<string, Uri>>(reader);

                if (mappings != null)
                    return new Context(mappings);
            }
            catch (JsonSerializationException)
            {
                // Deserialize context reference

                var reference = serializer.Deserialize<Uri>(reader);

                if (reference != null)
                    return new Context(reference);
            }

            throw new ArgumentException(
                "Cannot deserialize API documentation context from provided JSON-LD.");
        }

        public override void WriteJson(
            JsonWriter writer, [AllowNull] Context value, JsonSerializer serializer)
        {
            // Write context mappings
            if (value?.Mappings != null)
            {
                writer.WriteStartObject();

                foreach (KeyValuePair<string, Uri> mapping in value.Mappings)
                {
                    writer.WritePropertyName(mapping.Key);
                    writer.WriteValue(mapping.Value.ToString());
                }

                writer.WriteEndObject();
            }
            // Write context reference
            else if (value?.Reference != null)
                writer.WriteValue(value.Reference.ToString());
        }
    }
}
