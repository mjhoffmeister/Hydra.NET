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

        internal SupportedProperty(SupportedPropertyAttribute supportedPropertyAttribute)
        {
            Description = supportedPropertyAttribute.Description;
            IsReadable = supportedPropertyAttribute.IsReadable;
            IsRequired = supportedPropertyAttribute.IsRequired;
            IsWritable = supportedPropertyAttribute.IsWritable;
            Property = supportedPropertyAttribute.Property;
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

        

        
    }
}
