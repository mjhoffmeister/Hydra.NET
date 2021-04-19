using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    /// <summary>
    /// Custom JSON converter for <see cref="Context"/>.
    /// </summary>
    public class ContextJsonConverter : JsonConverter<Context>
    {
        public override Context? Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize a context reference
            if (reader.TokenType == JsonTokenType.String)
            {
                string? contextReferenceString = reader.GetString();

                if (contextReferenceString == null)
                    return null;

                return new Context(new Uri(contextReferenceString));
            }

            // Context mappings are expected at this point, so throw an exception if the context
            // isn't a JSON object
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Invalid JSON-LD context.");

            // Create a dictionary for contect mappings
            var mappings = new Dictionary<string, Uri>();

            // Deserialize context mappings
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return new Context(mappings);

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string? term = reader.GetString();

                    if (term != null)
                    {
                        reader.Read();
                        string? iriString = reader.GetString();

                        if (iriString != null)
                        {
                            var iri = new Uri(iriString);
                            mappings.Add(term, iri);
                        }
                    }
                }
            }

            return null;
        }

        public override void Write(
            Utf8JsonWriter writer, Context value, JsonSerializerOptions options)
        {
            // Serialize contect mappings
            if (value?.Mappings != null)
            {
                writer.WriteStartObject();

                foreach (KeyValuePair<string, Uri> mapping in value.Mappings)
                {
                    writer.WriteString(
                        JsonEncodedText.Encode(mapping.Key), mapping.Value.ToString());
                }

                writer.WriteEndObject();
            }
            // Serialize context reference
            else if (value?.Reference != null)
                writer.WriteStringValue(value.Reference.ToString());
        }
    }
}
