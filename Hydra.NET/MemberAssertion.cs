using Newtonsoft.Json;
using System;

namespace Hydra.NET
{
    /// <summary>
    /// Declares additional, implicit statements about the members of a collection.
    /// </summary>
    public class MemberAssertion
    {
        /// <summary>
        /// Note that according to the Hydra spec, two and only two of property, object, and subject
        /// should be used. See https://www.hydra-cg.com/spec/latest/core/#member-assertions.
        /// </summary>
        public MemberAssertion() { }

        /// <summary>
        /// Note that according to the Hydra spec, two and only two of property, object, and subject
        /// should be used. See https://www.hydra-cg.com/spec/latest/core/#member-assertions.
        /// </summary>
        public MemberAssertion(Uri? property = null, Uri? @object = null, Uri? subject = null)
        {
            Property = property;
            Object = @object;
            Subject = subject;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "property")]
        public Uri? Property { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "object")]
        public Uri? Object { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "subject")]
        public Uri? Subject { get; set; }
    }
}
