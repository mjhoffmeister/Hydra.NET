using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    /// <summary>
    /// Used to reference a set of resources.
    /// For more info, see https://www.hydra-cg.com/spec/latest/core/#collections.
    /// </summary>
    /// <typeparam name="T">Member type.</typeparam>
    public class Collection<T>
    {
        private readonly Context _context = new Context(new Dictionary<string, Uri>()
        {
            { "hydra", new Uri("http://www.w3.org/ns/hydra/core#") },
            { "Collection", new Uri("hydra:Collection") },
            { "member", new Uri("hydra:member") }
        });

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public Collection() { }

        public Collection(Uri id, IEnumerable<T> members) => (Id, Members) = (id, members);

        /// <summary>
        /// The collection's context.
        /// </summary>
        [JsonPropertyName("@context")]
        public Context Context => _context;

        /// <summary>
        /// The collection's id.
        /// </summary>
        [JsonPropertyName("@id")]
        public Uri? Id { get; set; }

        /// <summary>
        /// The collection's type: Collection.
        /// </summary>
        [JsonPropertyName("@type")]
        public string Type => "Collection";

        /// <summary>
        /// The member items of the collection.
        /// </summary>
        [JsonPropertyName("member")]
        public IEnumerable<T>? Members { get; set; }
    }
}
