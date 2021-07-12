using System;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    /// <summary>
    /// Property definition.
    /// TODO: specify property types such as Link, etc.
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public Property() { }

        /// <summary>
        /// Property.
        /// </summary>
        /// <param name="id">Id of the property.</param>
        /// <param name="range">Range of the property.</param>
        internal Property(string id, string range) => (Id, Range) = (new Uri(id), range);

        [JsonPropertyName("@id")]
        public Uri? Id { get; set; }

        [JsonPropertyName("range")]
        public string? Range { get; set; } 
    }
}
