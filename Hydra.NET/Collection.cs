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
        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public Collection() { }

        public Collection(Context? context, Uri id, IEnumerable<T> members) => 
            (Context, Id, Members) = (context, id, members);

        /// <summary>
        /// The collection's context.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@context")]
        public Context? Context { get; set; }

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
