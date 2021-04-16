using Newtonsoft.Json;

namespace Hydra.NET
{
    public class SupportedProperty
    {
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
        /// The property's description. Default: <see cref="null"/>.
        /// </summary>
        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "description",
            Order = 4)]
        public string? Description { get; set; }

        /// <summary>
        /// True if the property is readable; false, otherwise. Default: <see cref="true"/>.
        /// </summary>
        [JsonProperty(PropertyName = "readable", Order = 6)]
        public bool IsReadable { get; set; } = true;

        /// <summary>
        /// True if the property is required; false, otherwise. Default: <see cref="true"/>.
        /// </summary>
        [JsonProperty(PropertyName = "required", Order = 5)]
        public bool IsRequired { get; set; } = true;

        /// <summary>
        /// True if the property is writeable; false, otherwise. Default: <see cref="true"/>.
        /// </summary>
        [JsonProperty(PropertyName = "writable", Order = 6)]
        public bool IsWritable { get; set; } = true;

        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "property",
            Order = 7)]
        public Property? Property { get; set; }

        /// <summary>
        /// The property's title. Default: <see cref="null"/>.
        /// </summary>
        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "title",
            Order = 3)]
        public string? Title { get; set; }

        /// <summary>
        /// The property's type: SupportedProperty.
        /// </summary>
        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "@type",
            Order = 2)]
        public string Type => "SupportedProperty";
    }
}
