using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    /// <summary>
    /// Status definition.
    /// </summary>
    public class Status
    {
        private Status(
            Context? context, string type, int statusCode, string title, string descripion)
        {
            Context = context;
            Description = descripion;
            StatusCode = statusCode;
            Title = title;
            Type = type;
        }

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public Status() { }

        /// <summary>
        /// Creates a new status with a context and the default type ("Status");
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="statusCode">Status code.</param>
        /// <param name="title">Title.</param>
        /// <param name="description">Description.</param>
        public Status(Context context, int statusCode, string title, string description)
            : this(context, "Status", statusCode, title, description) { }

        /// <summary>
        /// Creates a new status without a context and a prefixed type.
        /// </summary>
        /// <param name="typePrefix">Type prefix.</param>
        /// <param name="statusCode">Status code.</param>
        /// <param name="title">Title.</param>
        /// <param name="description">Description.</param>
        public Status(string typePrefix, int statusCode, string title, string description)
            : this(null, $"{typePrefix}:Status", statusCode, title, description) { }

        /// <summary>
        /// Context.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@context")]
        public Context? Context { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        [JsonPropertyName("@type")]
        public string? Type { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Status code.
        /// </summary>
        [JsonPropertyName("statusCode")]
        public int? StatusCode { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}
