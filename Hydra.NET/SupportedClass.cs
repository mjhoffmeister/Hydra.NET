﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        internal SupportedClass(
            SupportedClassAttribute supportedClassAttribute,
            string contextPrefix,
            NodeShape? nodeShape = null)
        {
            Description = supportedClassAttribute.Description;
            Id = new Uri($"{contextPrefix}:{supportedClassAttribute.Id}");
            PropertyShapes = nodeShape?.WithContextPrefix(contextPrefix).PropertyShapes;
            Title = supportedClassAttribute.Title;
            Types = nodeShape == null ? new[] { "Class" } : new[] { "Class", "NodeShape" };
        }

        public SupportedClass(
            Uri id,
            string? title = null,
            string? description = null,
            IEnumerable<PropertyShape>? propertyShapes = null,
            IEnumerable<SupportedProperty>? supportedProperties = null,
            IEnumerable<Operation>? supportedOperations = null)
        {
            IEnumerable<string> types = propertyShapes?.Any() == true ? 
                new[] { "Class" } : new[] { "Class", "NodeShape" };

            Id = id;
            Types = types;
            Title = title;
            Description = description;
            PropertyShapes = propertyShapes;
            SupportedProperties = supportedProperties;
            SupportedOperations = supportedOperations;
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
        public IEnumerable<string>? Types { get; set; }

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
        /// The class's property shapes. When set, the SHACL extension is implicity used.
        /// </summary>
        [JsonPropertyName("propertyShape")]
        public IEnumerable<PropertyShape>? PropertyShapes { get; set; }

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
