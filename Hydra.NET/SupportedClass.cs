using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Hydra.NET
{
    [JsonConverter(typeof(SupportedClassJsonConverter))]
    public class SupportedClass
    {
        public SupportedClass() {}

        internal SupportedClass(SupportedClassAttribute supportedClassAttribute)
        {
            Description = supportedClassAttribute.Description;
            Id = supportedClassAttribute.Id;
            Title = supportedClassAttribute.Title;
        }

        /// <summary>
        /// The class's description. Default: <see cref="null"/>.
        /// </summary>
        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "description",
            Order = 4)]
        public string? Description { get; set; }

        /// <summary>
        /// The class's id.
        /// </summary>
        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "@id",
            Order = 1)]
        public Uri? Id { get; set; }

        /// <summary>
        /// The class's supported properties.
        /// </summary>
        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "supportedProperty",
            Order = 99)]
        public IEnumerable<SupportedProperty>? SupportedProperties { get; set; }

        /// <summary>
        /// The class's supported operations.
        /// </summary>
        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "supportedOperation",
            Order = 100)]
        public IEnumerable<Operation>? SupportedOperations { get; set; }

        /// <summary>
        /// The class's title. Default: <see cref="null"/>.
        /// </summary>
        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "title",
            Order = 3)]
        public string? Title { get; set; }

        /// <summary>
        /// The class's type (Class.)
        /// </summary>
        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "@type",
            Order = 2)]
        public virtual string Type => "Class";
    }
}
