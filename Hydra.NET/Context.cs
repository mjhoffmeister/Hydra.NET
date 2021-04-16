using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Hydra.NET
{
    [JsonConverter(typeof(ContextJsonConverter))]
    public class Context
    {
        /// <summary>
        /// Creates a context with a mappings dictionary that maps terms to IRIs.
        /// </summary>
        /// <param name="mappings">Mappings dictionary.</param>
        public Context(Dictionary<string, Uri> mappings) => Mappings = mappings;

        /// <summary>
        /// Creates a context with a reference to the context document.
        /// </summary>
        /// <param name="reference">Reference.</param>
        public Context(Uri reference) => Reference = reference;

        /// <summary>
        /// Mappings dictionary. Used instead of a context reference.
        /// </summary>
        public Dictionary<string, Uri>? Mappings { get; }

        /// <summary>
        /// Reference to the context document. Used instead of context mapping.
        /// </summary>
        public Uri? Reference { get; }

        /// <summary>
        /// Adds a context mapping.
        /// </summary>
        /// <param name="term">Term.</param>
        /// <param name="iri">IRI.</param>
        /// <returns>True if the mapping was added; false, otherwise.</returns>
        public bool TryAddMapping(string term, Uri iri)
        {
            if (Mappings == null || Mappings.ContainsKey(term))
                return false;

            Mappings.Add(term, iri);

            return true;
        }
    }
}
