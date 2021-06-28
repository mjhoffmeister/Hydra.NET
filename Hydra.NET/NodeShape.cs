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
        public NodeShape(Uri targetClass, params PropertyShape[] propertyShapes)
        {
            PropertyShapes = propertyShapes;
            TargetClass = targetClass;
        }

        /// <summary>
        /// The target class of the node shape.
        /// </summary>
        [JsonPropertyName("targetClass")]
        public Uri TargetClass { get; }

        /// <summary>
        /// The node shape's <see cref="PropertyShape"/>s.
        /// </summary>
        [JsonPropertyName("property")]
        public IEnumerable<PropertyShape> PropertyShapes { get; }
    }
}
