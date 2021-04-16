using Newtonsoft.Json;
using System;

namespace Hydra.NET
{
    public class SupportedClassAttribute : Attribute
    {
        /// <summary>
        /// Includes the class in the API documentation.
        /// </summary>
        /// <param name="id">Id IRI. Relative to the API documentation id.</param>
        public SupportedClassAttribute(string id) => Id = new Uri(id);

        /// <summary>
        /// The class's description. Default: <see cref="null"/>.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The class's title. Default: <see cref="null"/>.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// The class's id.
        /// </summary>
        [JsonProperty(PropertyName = "@id", Order = 1)]
        public Uri Id { get; }

    }
}
