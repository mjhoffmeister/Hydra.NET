using System;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    /// <summary>
    /// Declares additional, implicit statements about the members of a collection.
    /// Note that according to the Hydra spec, two and only two of property, object, and subject
    /// should be used.
    /// For more info, see https://www.hydra-cg.com/spec/latest/core/#member-assertions.
    /// </summary>
    public class MemberAssertion
    {
        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public MemberAssertion() { }

        public MemberAssertion(Uri? property = null, Uri? @object = null, Uri? subject = null)
        {
            Property = property;
            Object = @object;
            Subject = subject;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("property")]
        public Uri? Property { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("object")]
        public Uri? Object { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("subject")]
        public Uri? Subject { get; set; }
    }
}
