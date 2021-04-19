using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    /// <summary>
    /// A class known to be supported by the API.
    /// Rather than manually creating objects of this type, it's recommended to decorate your
    /// classes with <see cref="SupportedClassAttribute"/> and add them to your API documentation
    /// via <see cref="ApiDocumentation.AddSupportedClass{T}"/>.
    /// </summary>
    [JsonConverter(typeof(SupportedClassJsonConverter))]
    public class SupportedClass
    {
        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public SupportedClass() {}

        internal SupportedClass(SupportedClassAttribute supportedClassAttribute)
        {
            Description = supportedClassAttribute.Description;
            Id = supportedClassAttribute.Id;
            Title = supportedClassAttribute.Title;
        }

        /// <summary>
        /// The class's id.
        /// </summary>
        [JsonPropertyName("@id")]
        public Uri? Id { get; set; }

        /// <summary>
        /// The class's type: Class
        /// </summary>
        [JsonPropertyName("@type")]
        public virtual string Type => "Class";

        /// <summary>
        /// The class's title.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// The class's description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// The class's supported properties.
        /// </summary>
        [JsonPropertyName("supportedProperty")]
        public IEnumerable<SupportedProperty>? SupportedProperties { get; set; }

        /// <summary>
        /// The class's supported operations.
        /// </summary>
        //[JsonProperty(
        [JsonPropertyName("supportedOperation")]
        public IEnumerable<Operation>? SupportedOperations { get; set; }
    }
}
