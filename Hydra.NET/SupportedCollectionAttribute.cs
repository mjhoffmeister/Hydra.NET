using System;

namespace Hydra.NET
{
    public class SupportedCollectionAttribute : Attribute
    {
        public SupportedCollectionAttribute(string id) => Id = new Uri(id);

        public string? Description { get; set; }

        public Uri Id { get; }

        public string? Title { get; set; }
    }
}
