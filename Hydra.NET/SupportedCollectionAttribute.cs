using System;

namespace Hydra.NET
{
    /// <summary>
    /// Designates the class as having a Collection supported by the API.
    /// Note: "SupportedCollection" isn't part of the Hydra vocabulary; the name is used in this
    /// library as a convention.
    /// For more info, see https://www.hydra-cg.com/spec/latest/core/#collections.
    /// </summary>
    public class SupportedCollectionAttribute : Attribute
    {
        public SupportedCollectionAttribute(string id) => Id = new Uri(id);

        /// <summary>
        /// The collection's description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The collection's id.
        /// </summary>
        public Uri Id { get; }

        /// <summary>
        /// The collection's title.
        /// </summary>
        public string? Title { get; set; }
    }
}
