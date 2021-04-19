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
        }

        /// <summary>
        /// The collection's type: Collection.
        /// </summary>
        [JsonPropertyName("@type")]
        public override string Type => "Collection";

        /// <summary>
        /// Member assertion.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("memberAssertion")]
        public MemberAssertion? MemberAssertion { get; set; }
    }
}
