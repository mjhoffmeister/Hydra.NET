using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    /// <summary>
    /// A shape adding constraints to a property. 
    /// See https://www.w3.org/TR/shacl/#property-shapes for more info.
    /// </summary>
    public class PropertyShape
    {
        /// <summary>
        /// Creates a new property shape.
        /// </summary>
        /// <param name="path">The path of the property to which the shape applies.</param>
        /// <param name="allowedValues">The allowed values for the property.</param>
        public PropertyShape(Uri path, IEnumerable<string>? allowedValues = null)
        {
            AllowedValues = allowedValues;
            Path = path;
        }

        /// <summary>
        /// The path of the property to which the shape applies.
        /// </summary>
        [JsonPropertyName("path")]
        public Uri Path { get; }

        /// <summary>
        /// The allowed values for the property.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("in")]
        public IEnumerable<string>? AllowedValues { get; }
    }
}
