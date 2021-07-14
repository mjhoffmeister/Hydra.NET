using System;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    /// <summary>
    /// A Collection class supported by the API.
    /// Rather than manually creating objects of this type, it's recommended to decorate your
    /// classes with <see cref="SupportedCollectionAttribute"/> and add them to your API
    /// documentation via <see cref="ApiDocumentation.AddSupportedClass{T}"/>.
    /// </summary>
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
            Types = new[] { "Collection" };
        }

        /// <summary>
        /// Member assertion.
        /// TODO: should member assertions be included in API documentation?
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("memberAssertion")]
        public MemberAssertion? MemberAssertion { get; set; }
    }
}
