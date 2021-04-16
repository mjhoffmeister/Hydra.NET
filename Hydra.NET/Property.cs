using Newtonsoft.Json;
using System;

namespace Hydra.NET
{
    public class Property
    {
        public Property() { }

        /// <summary>
        /// Property.
        /// </summary>
        /// <param name="id">Id of the property.</param>
        /// <param name="range">Range of the property.</param>
        internal Property(string id, string range) => (Id, Range) = (new Uri(id), new Uri(range));

        [JsonProperty(PropertyName = "@id")]
        public Uri? Id { get; set; }

        [JsonProperty(PropertyName = "range")]
        public Uri? Range { get; set; } 
    }
}
