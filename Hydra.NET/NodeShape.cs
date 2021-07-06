using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    /// <summary>
    /// A shape adding constraints to a node (class.)
    /// See https://www.w3.org/TR/shacl/#node-shapes for more info.
    /// </summary>
    public class NodeShape
    {
        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public NodeShape() { }

        public NodeShape(Uri targetClass, params PropertyShape[] propertyShapes)
        {
            PropertyShapes = propertyShapes;
            TargetClass = targetClass;
        }

        [JsonPropertyName("@type")]
        public string Type => "NodeShape";

        /// <summary>
        /// The target class of the node shape.
        /// </summary>
        [JsonPropertyName("targetClass")]
        public Uri? TargetClass { get; set; }

        /// <summary>
        /// The node shape's <see cref="PropertyShape"/>s.
        /// </summary>
        [JsonPropertyName("propertyShape")]
        public IEnumerable<PropertyShape>? PropertyShapes { get; set; }
    }
}
