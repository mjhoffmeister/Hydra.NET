using Newtonsoft.Json;

namespace Hydra.NET
{
    public class Operation
    {
        public Operation() { }

        internal Operation(OperationAttribute operationAttribute)
        {
            Method = operationAttribute.Method;
            Title = operationAttribute.Title;
        }

        [JsonProperty(PropertyName = "method", Order = 3)]
        public string? Method { get; set; }

        [JsonProperty(PropertyName = "title", Order = 2)]
        public string? Title { get; set; }

        [JsonProperty(PropertyName = "@type", Order = 1)]
        public string Type => "Operation";
    }
}
