using System;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    public class Link
    {
        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public Link() { }

        /// <summary>
        /// Creates a new link.
        /// </summary>
        /// <param name="id">The link's id.</param>
        /// <param name="title">The link's title (optional.)</param>
        /// <param name="description">The link's description (optional.)</param>
        public Link(Uri? id, string? title = null, string? description = null)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        /// <summary>
        /// The link's id.
        /// </summary>
        [JsonPropertyName("@id")]
        public Uri? Id { get; set; }

        /// <summary>
        /// The link's type (Link.)
        /// </summary>
        [JsonPropertyName("@type")]
        public string Type => "Link";

        /// <summary>
        /// The link's title.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// The link's description.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
