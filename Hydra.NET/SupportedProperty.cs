using System;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    /// <summary>
    /// A property known to be supported by a class.
    /// Rather than manually creating objects of this type, it's recommended to decorate your
    /// properties with <see cref="SupportedPropertyAttribute"/>.
    /// </summary>
    public class SupportedProperty
    {
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public SupportedProperty() { }

        internal SupportedProperty(
            SupportedPropertyAttribute supportedPropertyAttribute,
            string contextPrefix)
        {
            Description = supportedPropertyAttribute.Description;
            IsReadable = supportedPropertyAttribute.IsReadable;
            IsRequired = supportedPropertyAttribute.IsRequired;
            IsWritable = supportedPropertyAttribute.IsWritable;
            Property = GetProperty(
                contextPrefix,
                supportedPropertyAttribute.Id,
                supportedPropertyAttribute.Range,
                supportedPropertyAttribute.AddApiDocumentationPrefixToRange);
            Title = supportedPropertyAttribute.Title;
        }

        /// <summary>
        /// The property's type: SupportedProperty.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@type")]
        public string Type => "SupportedProperty";

        /// <summary>
        /// The property's title.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// The property's description.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// True if the property is required; false, otherwise. Default: <see cref="true"/>.
        /// </summary>
        [JsonPropertyName("required")]
        public bool IsRequired { get; set; } = true;

        /// <summary>
        /// True if the property is readable; false, otherwise. Default: <see cref="true"/>.
        /// </summary>
        [JsonPropertyName("readable")]
        public bool IsReadable { get; set; } = true;

        /// <summary>
        /// True if the property is writeable; false, otherwise. Default: <see cref="true"/>.
        /// </summary>
        [JsonPropertyName("writable")]
        public bool IsWritable { get; set; } = true;

        /// <summary>
        /// The property's definition.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("property")]
        public Property? Property { get; set; }

        /// <summary>
        /// Gets the RDF property for the supported property.
        /// </summary>
        /// <param name="contextPrefix">Context prefix.</param>
        /// <param name="id">Id.</param>
        /// <param name="range">Range</param>
        /// <param name="addApiDocumentationPrefixToRange">
        /// Indicator for adding the API documentation prefix to the property's range.
        /// </param>
        /// <returns><see cref="Property"/></returns>
        private Property GetProperty(
            string contextPrefix, string id, string range, bool addApiDocumentationPrefixToRange)
        {
            // Add the API documention prefix to range, if specified
            range = addApiDocumentationPrefixToRange ?
                $"{contextPrefix}:{range}" : range;

            // Return the property
            return new Property(new Uri($"{contextPrefix}:{id}"), range);
        }
    }
}
