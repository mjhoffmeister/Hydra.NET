using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Hydra.NET
{
    public class Collection<T>
    {
        private readonly Context _context = new Context(new Dictionary<string, Uri>()
        {
            { "hydra", new Uri("http://www.w3.org/ns/hydra/core#") },
            { "Collection", new Uri("hydra:Collection") },
            { "member", new Uri("hydra:member") }
        });

        public Collection() { }

        public Collection(Uri id, IEnumerable<T> members) => (Id, Members) = (id, members);

        [JsonProperty(PropertyName = "@context", Order = 1)]
        public Context Context => _context;

        [JsonProperty(PropertyName = "@id", Order = 2)]
        public Uri? Id { get; set; }

        [JsonProperty(PropertyName = "member", Order = 4)]
        public IEnumerable<T>? Members { get; set; }

        [JsonProperty(PropertyName = "@type", Order = 3)]
        public string Type => "Collection";
    }
}
