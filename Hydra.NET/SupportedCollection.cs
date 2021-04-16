using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra.NET
{
    public class SupportedCollection : SupportedClass
    {
        public SupportedCollection() { }

        internal SupportedCollection(
            Uri memberId, SupportedCollectionAttribute supportedCollectionAttribute)
        {
            Description = supportedCollectionAttribute.Description;
            Id = supportedCollectionAttribute.Id;
            MemberAssertion = new MemberAssertion(@object: memberId, property: new Uri(Rdf.Type));
            Title = supportedCollectionAttribute.Title;
        }

        /// <summary>
        /// Member assertion.
        /// </summary>
        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "memberAssertion",
            Order = 5)]
        public MemberAssertion? MemberAssertion { get; set; }

        /// <summary>
        /// The collection's type: Collection.
        /// </summary>
        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "@type",
            Order = 2)]
        public override string Type => "Collection";
    }
}
