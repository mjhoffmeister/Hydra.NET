using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    public class SupportedClassJsonConverter : JsonConverter<SupportedClass>
    {
        public override SupportedClass? Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Throw an exception if the supported class isn't a JSON object
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Invalid SupportedClass.");

            // Parse a JsonDocument from the reader
            var jsonDocument = JsonDocument.ParseValue(ref reader);

            // Reference the root element of the supported class
            JsonElement rootElement = jsonDocument.RootElement;

            // Deserialize types
            IEnumerable<string> types = DeserializeTypes(rootElement);

            // Create the appropriate object based on JSON-LD type
            var supportedClass = types.First() != "Collection" ?
                new SupportedClass() :
                new SupportedCollection();

            // Set properties
            supportedClass.Id = rootElement.TryGetUriValue("@id");
            supportedClass.Types = types;
            supportedClass.Title = rootElement.TryGetStringValue("title");
            supportedClass.Description = rootElement.TryGetStringValue("description");
            supportedClass.PropertyShapes = rootElement
                .TryDeserializeValue<IEnumerable<PropertyShape>>("propertyShape");
            supportedClass.SupportedProperties = rootElement
                .TryDeserializeValue<IEnumerable<SupportedProperty>>("supportedProperty");
            supportedClass.SupportedOperations = rootElement
                .TryDeserializeValue<IEnumerable<Operation>>("supportedOperation");

            if (supportedClass is SupportedCollection supportedCollection)
            {
                supportedCollection.MemberAssertion = rootElement
                    .TryDeserializeValue<MemberAssertion>("memberAssertion");
            }

            // Return the supported class
            return supportedClass;
        }

        public override void Write(
            Utf8JsonWriter writer, SupportedClass value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            WriteStringIfNotNull("@id", value.Id?.ToString());

            // Type is written as a string if there's only one; otherwise, an array
            if (value.Types?.Count() == 1)
                WriteStringIfNotNull("@type", value.Types.First());
            else
                WriteObjectIfNotNull("@type", value.Types);

            WriteStringIfNotNull("title", value.Title);
            WriteStringIfNotNull("description", value.Description);

            // Member assertion serialization for collections
            if (value is SupportedCollection supportedCollection)
                WriteObjectIfNotNull("memberAssertion", supportedCollection.MemberAssertion);

            WriteObjectIfNotNull("propertyShape", value.PropertyShapes);
            WriteObjectIfNotNull("supportedProperty", value.SupportedProperties);
            WriteObjectIfNotNull("supportedOperation", value.SupportedOperations);

            void WriteObjectIfNotNull<T>(string propertyName, T value)
            {
                if (value != null)
                {
                    writer.WritePropertyName(propertyName);
                    string valueJson = JsonSerializer.Serialize(value);
                    using JsonDocument document = JsonDocument.Parse(valueJson);
                    document.RootElement.WriteTo(writer);
                }
            }

            void WriteStringIfNotNull(string propertyName, string? value)
            {
                if (value != null)
                    writer.WriteString(JsonEncodedText.Encode(propertyName), value);
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Deserializes type (@type.)
        /// </summary>
        /// <param name="rootElement">The root element.</param>
        /// <returns>The deserialized type.</returns>
        private IEnumerable<string> DeserializeTypes(JsonElement rootElement)
        {
            // Check if type is a collection
            IEnumerable<string>? types = rootElement
                .TryDeserializeEnumerable<string>("@type");

            // If it is, return the collection
            if (types != null)
                return types;
            
            // Check if type is a single value
            string? type = rootElement.TryGetStringValue("@type");

            // If it is, return the single value as a member of a collection
            if (type != null)
                return new[] { type };

            // Use "Class" as the default type
            return new[] { "Class" };
        }
    }
}
