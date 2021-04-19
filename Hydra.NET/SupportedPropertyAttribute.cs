using System;

namespace Hydra.NET
{
    /// <summary>
    /// Designates the property as supported by a class.
    /// </summary>
    public class SupportedPropertyAttribute : Attribute
    {
        /// <summary>
        /// Supported property attribute.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="range">Range.</param>
        public SupportedPropertyAttribute(string id, string range) => 
            Property = new Property(id, range);

        /// <summary>
        /// The property's description. Default: <see cref="null"/>.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// True if the property is readable; false, otherwise. Default: <see cref="true"/>.
        /// </summary>
        public bool IsReadable { get; set; } = true;

        /// <summary>
        /// True if the property is required; false, otherwise. Default: <see cref="true"/>.
        /// </summary>
        public bool IsRequired { get; set; } = true;

        /// <summary>
        /// True if the property is writeable; false, otherwise. Default: <see cref="true"/>.
        /// </summary>
        public bool IsWritable { get; set; } = true;

        public Property Property { get; }

        /// <summary>
        /// The property's title. Default: <see cref="null"/>.
        /// </summary>
        public string? Title { get; set; }
    }
}
