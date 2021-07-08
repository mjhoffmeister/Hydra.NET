using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Hydra.NET
{
    /// <summary>
    /// Describes a web API.
    /// For more info, see https://www.hydra-cg.com/spec/latest/core/#documenting-a-web-api.
    /// </summary>
    public class ApiDocumentation
    {
        // Cached operation attributes
        private ILookup<Type, OperationAttribute>? _cachedOperationAttributes;

        public ApiDocumentation() { }

        public ApiDocumentation(Uri id) => Id = id;

        // TODO: make this more dynamic
        [JsonPropertyName("@context")]
        public Context Context { get; set; } = new Context(new Dictionary<string, Uri>()
        {
            { "sh", new Uri("http://www.w3.org/ns/shacl#") },
            { "hydra", new Uri("https://www.w3.org/ns/hydra/core#") },
            { "rdf", new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#") },
            { "rdfs", new Uri("http://www.w3.org/2000/01/rdf-schema#") },
            { "xsd", new Uri("http://www.w3.org/2001/XMLSchema#") },
            { "ApiDocumentation", new Uri("hydra:ApiDocumentation") },
            { "Class", new Uri("hydra:Class") },
            { "Collection", new Uri("hydra:Collection") },
            { "description", new Uri("hydra:description") },
            { "extension", new Uri("hydra:extension") },
            { "in", new Uri("sh:in") },
            { "memberAssertion", new Uri("hydra:memberAssertion") },
            { "NodeShape", new Uri("sh:NodeShape") },
            { "object", new Uri("hydra:object") },
            { "Operation", new Uri("hydra:Operation") },
            { "path", new Uri("sh:path") },
            { "property", new Uri("hydra:property") },
            { "propertyShape", new Uri("sh:property") },
            { "PropertyShape", new Uri("sh:PropertyShape") },
            { "range", new Uri("rdfs:range") },
            { "readable", new Uri("hydra:readable") },
            { "required", new Uri("hydra:required") },
            { "supportedClass", new Uri("hydra:supportedClass") },
            { "supportedOperation", new Uri("hydra:supportedOperation") },
            { "supportedProperty", new Uri("hydra:supportedProperty") },
            { "SupportedProperty", new Uri("hydra:SupportedProperty") },
            { "targetClass", new Uri("sh:targetClass") },
            { "title", new Uri("hydra:title") },
            { "writable", new Uri("hydra:writable") }
        });

        /// <summary>
        /// The API documentation's id.
        /// </summary>
        [JsonPropertyName("@id")]
        public Uri? Id { get; set; }

        /// <summary>
        /// The API documentation's type: ApiDocumentation.
        /// </summary>
        [JsonPropertyName("@type")]
        public string Type => "ApiDocumentation";

        /// <summary>
        /// The extensions used by the API.
        /// TODO: make this more dynamic.
        /// </summary>
        [JsonPropertyName("extension")]
        public string Extension => "http://www.w3.org/ns/shacl#";

        /// <summary>
        /// Classes supported by the API.
        /// </summary>
        [JsonPropertyName("supportedClass")]
        public List<SupportedClass> SupportedClasses { get; set; } = new List<SupportedClass>();

        /// <summary>
        /// Adds a supported class.
        /// </summary>
        /// <typeparam name="T">The type of a supported class.</typeparam>
        /// <param name="nodeShape">
        /// A <see cref="NodeShape"/> dictating constraints on the supported class.</param>
        /// <returns>The <see cref="ApiDocumentation"/> with the added class.</returns>
        public ApiDocumentation AddSupportedClass<T>(NodeShape? nodeShape = null)
        {
            // Get the type of the supported class to be added
            Type type = typeof(T);

            // Get the class's SupportedClassAttribute
            SupportedClassAttribute? supportedClassAttribute = 
                type.GetCustomAttribute<SupportedClassAttribute>();

            // If the class doesn't have a SupportedClassAttribute, throw an ArgumentException
            if (supportedClassAttribute == null)
            {
                throw new ArgumentException($"{type.Name} cannot be added to API documentation " +
                    $"because it's not decorated with {nameof(SupportedClassAttribute)}.");
            }

            // Create a supported class from the attribute
            var supportedClass = new SupportedClass(supportedClassAttribute, nodeShape);

            // Get supported property attributes
            IEnumerable<SupportedPropertyAttribute> supportedPropertyAttributes =
                 type.GetProperties()
                    .Select(p => p.GetCustomAttribute<SupportedPropertyAttribute>())
                    .Where(a => a != null);

            // Create supported properties from the attributes
            if (supportedPropertyAttributes.Any())
            {
                supportedClass.SupportedProperties = supportedPropertyAttributes.Select(
                    a => new SupportedProperty(a));
            }

            // Add supported operations
            supportedClass.SupportedOperations = GetSupportedOperations(type);

            // Add the supported class
            SupportedClasses.Add(supportedClass);

            // Add a collection for the class, if specified
            TryAddSupportedCollection<T>(supportedClass.Id!, type);

            // Return the object for fluent-style functionality
            return this;
        }

        /// <summary>
        /// Adds a supported collection for the type, if specified.
        /// </summary>
        /// <param name="memberId">The id of collection member type.</param>
        /// <param name="type">Type.</param>
        /// <returns>True if collection documentation was added; false, otherwise.</returns>
        private bool TryAddSupportedCollection<T>(Uri memberId, Type type)
        {
            // Get the class's SupportedCollectionAttribute
            SupportedCollectionAttribute? supportedCollectionAttribute =
                type.GetCustomAttribute<SupportedCollectionAttribute>();

            // Return false if the type doesn't have a collection specified
            if (supportedCollectionAttribute == null)
                return false;

            // Create a SupportedCollection object from the attribute
            var supportedCollection = new SupportedCollection(
                memberId, supportedCollectionAttribute)
            {
                SupportedOperations = GetSupportedOperations(
                    type, typeof(Collection<T>))
            };

            // Add the collection to supported classes
            SupportedClasses.Add(supportedCollection);
            return true;
        }

        /// <summary>
        /// Gets supported operations for a type.
        /// </summary>
        /// <param name="type">The type for which to get supported operations.</param>
        /// <param name="collectionType">
        /// The collection type for which to get supported operations.
        /// </param>
        /// <returns>Supported operations if any were found; null, otherwise.</returns>
        private IEnumerable<Operation>? GetSupportedOperations(
            Type type, Type? collectionType = null)
        {
            if (_cachedOperationAttributes == null)
            {
                _cachedOperationAttributes = Assembly
                    .GetAssembly(type)
                    .GetTypes()
                    .SelectMany(t => t.GetMethods())
                    .SelectMany(m => m.GetCustomAttributes<OperationAttribute>())
                    .ToLookup(a => a.SupportedClassType);
            }

            Type searchType = collectionType ?? type;

            if (!_cachedOperationAttributes.Contains(searchType))
                return null;

            return _cachedOperationAttributes[searchType].Select(a => new Operation(a));
        }
    }
}