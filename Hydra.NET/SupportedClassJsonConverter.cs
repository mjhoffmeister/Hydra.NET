using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    public class SupportedClassJsonConverter : JsonConverter<SupportedClass>
    {
        private enum SupportedClassType
        {
            Class,
            Collection
        }

        public override SupportedClass? Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Throw an exception if the supported class isn't a JSON object
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Invalid SupportedClass.");

            // Parse a JsonDocument from the reader
            var jsonDocument = JsonDocument.ParseValue(ref reader);

            // Get the supported class type
            SupportedClassType type = GetSupportedClassType(jsonDocument);

            // Reference the root element of the supported class
            JsonElement rootElement = jsonDocument.RootElement;

            // Create the appropriate object based on JSON-LD type
            var supportedClass = type == SupportedClassType.Class ?
                new SupportedClass() :
                new SupportedCollection();

            // Set properties
            supportedClass.Id = rootElement.TryGetUriValue("@id");
            supportedClass.Title = rootElement.TryGetStringValue("title");
            supportedClass.Description = rootElement.TryGetStringValue("description");
            supportedClass.SupportedProperties = rootElement
                .TryDeserializeValue<IEnumerable<SupportedProperty>>("supportedProperty");
            supportedClass.SupportedOperations = rootElement
                .TryDeserializeValue<IEnumerable<Operation>>("supportedOperation");

            if (type == SupportedClassType.Collection)
            {
                ((SupportedCollection)supportedClass).MemberAssertion = rootElement
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
            writer.WriteString(JsonEncodedText.Encode("@type"), value.Type);
            WriteStringIfNotNull("title", value.Title);
            WriteStringIfNotNull("description", value.Description);

            // Member assertion serialization for collections
            if (value is SupportedCollection supportedCollection)
                WriteObjectIfNotNull("memberAssertion", supportedCollection.MemberAssertion);

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
        /// Gets the supported class type.
        /// </summary>
        /// <param name="reader">JSON reader.</param>
        /// <returns>"Class" or "Collection".</returns>
        private SupportedClassType GetSupportedClassType(JsonDocument jsonDocument)
        {
            // If the @type property is "Collection" return "Collection"
            if (jsonDocument.RootElement.TryGetProperty("@type", out JsonElement typeProperty))
            {
                string? supportedClassType = typeProperty.GetString();

                if (supportedClassType == "Collection")
                    return SupportedClassType.Collection;
            }

            // Otherwise, return "Class"
            // This makes "Class" the default if @type is anything besides "Collection:
            return SupportedClassType.Class;
        }
    }

    //public class SupportedClassJsonConverter : JsonConverter<SupportedClass>
    //{
    //    /// <summary>
    //    /// Set to false because no custom write logic is needed.
    //    /// </summary>
    //    public override bool CanWrite => false;

    //    public override SupportedClass ReadJson(
    //        JsonReader reader,
    //        Type objectType,
    //        [AllowNull] SupportedClass existingValue,
    //        bool hasExistingValue,
    //        JsonSerializer serializer)
    //    {
    //        // Load a JObject from the reader
    //        var jObject = JObject.Load(reader);

    //        // Populate a SupportedCollection object
    //        var supportedCollection = new SupportedCollection();
    //        serializer.Populate(jObject.CreateReader(), supportedCollection);

    //        // If the MemberAssertion property isn't null, it is a Collection, so return
    //        if (supportedCollection.MemberAssertion != null)
    //            return supportedCollection;

    //        // Otherwise, it is a Class, populate a SupportedClass and then return
    //        var supportedClass = new SupportedClass();
    //        serializer.Populate(jObject.CreateReader(), supportedClass);

    //        return supportedClass;
    //    }

    //    public override void WriteJson(
    //        JsonWriter writer, [AllowNull] SupportedClass value, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
