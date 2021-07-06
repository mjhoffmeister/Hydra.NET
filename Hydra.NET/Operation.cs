using System.Text.Json.Serialization;

namespace Hydra.NET
{
    /// <summary>
    /// Operation definition supported by instances of a class.
    /// Rather than manually creating objects of this type, it's recommended to decorate your
    /// controller methods with <see cref="OperationAttribute"/>.
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public Operation() { }

        internal Operation(OperationAttribute operationAttribute, NodeShape? expects = null)
        {
            Method = operationAttribute.Method;
            Expects = expects;
            Title = operationAttribute.Title;
        }

        /// <summary>
        /// The operation's type: Operation.
        /// </summary>
        [JsonPropertyName("@type")]
        public string Type => "Operation";

        /// <summary>
        /// The operation's title.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// The operation's HTTP method.
        /// </summary>
        [JsonPropertyName("method")]
        public string? Method { get; set; }
        
        /// <summary>
        /// Specifies what the server expects from the client in the form of a SHACL node shape.
        /// TODO: add an "expects" property for Hydra classes, and make it mutually exclusive with 
        /// this property.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("expects")]
        public NodeShape? Expects { get; set; }
    }
}
