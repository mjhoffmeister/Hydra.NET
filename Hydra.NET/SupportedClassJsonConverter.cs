using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Hydra.NET
{
    public class SupportedClassJsonConverter : JsonConverter<SupportedClass>
    {
        /// <summary>
        /// Set to false because no custom write logic is needed.
        /// </summary>
        public override bool CanWrite => false;

        public override SupportedClass ReadJson(
            JsonReader reader,
            Type objectType,
            [AllowNull] SupportedClass existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            // Load a JObject from the reader
            var jObject = JObject.Load(reader);

            // Populate a SupportedCollection object
            var supportedCollection = new SupportedCollection();
            serializer.Populate(jObject.CreateReader(), supportedCollection);

            // If the MemberAssertion property isn't null, it is a Collection, so return
            if (supportedCollection.MemberAssertion != null)
                return supportedCollection;

            // Otherwise, it is a Class, populate a SupportedClass and then return
            var supportedClass = new SupportedClass();
            serializer.Populate(jObject.CreateReader(), supportedClass);

            return supportedClass;
        }

        public override void WriteJson(
            JsonWriter writer, [AllowNull] SupportedClass value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
