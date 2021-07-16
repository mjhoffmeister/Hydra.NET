﻿using System;

namespace Hydra.NET
{
    /// <summary>
    /// Designates the class as supported by the API.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]  
    public class SupportedClassAttribute : Attribute
    {
        /// <summary>
        /// Includes the class in the API documentation.
        /// </summary>
        /// <param name="id">Id IRI. Relative to the API documentation id.</param>
        public SupportedClassAttribute(string id) => Id = id;

        /// <summary>
        /// The class's id.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The class's description. Default: <see cref="null"/>.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The class's title. Default: <see cref="null"/>.
        /// </summary>
        public string? Title { get; set; }

    }
}
