namespace Hydra.NET.UnitTests
{
    [SupportedClass("Star")]
    public class Star
    {
        [SupportedProperty("Star/name", Xsd.String)]
        public string? Name { get; set; }

        [SupportedProperty(
            "Star/classification",
            "StellarClassification",
            AddApiDocumentationPrefixToRange = true
        )]
        public string? Classification { get; set; }
    }
}
