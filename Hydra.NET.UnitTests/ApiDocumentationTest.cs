//using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace Hydra.NET.UnitTests
{
    public static class ApiDocumentationTest
    {
        [Fact]
        public static void Serialize_StockApiDocumentation_GeneratesExpectedJsonLD()
        {
            // Arrange

            string expectedJsonLD = File.ReadAllText(
                "expected-api-documentation-with-stock.jsonld");

            var apiDocumentation = new ApiDocumentation(new Uri("https://api.example.com/doc"));
            apiDocumentation.Context.TryAddMapping("doc", new Uri("https://api.example.com/doc#"));

            apiDocumentation.AddSupportedClass<Stock>();

            // Act

            string jsonLD = JsonSerializer.Serialize(apiDocumentation, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Assert

            Assert.Equal(expectedJsonLD, jsonLD);
        }

        [Fact]
        public static void Deserialize_StockApiDocumentation_CreatesApiDocumentationWithStockClass()
        {
            // Arrange

            string expectedSupportedClassId = "doc:Stock";

            string apiDocumentationWithStockClassJsonLD =
                File.ReadAllText("expected-api-documentation-with-stock.jsonld");

            // Act

            ApiDocumentation? apiDocumentation = JsonSerializer.Deserialize<ApiDocumentation>(
                apiDocumentationWithStockClassJsonLD);

            // Assert

            Assert.Equal(
                expectedSupportedClassId,
                apiDocumentation?.SupportedClasses?.First()?.Id?.ToString());
        }

        [Fact]
        public static void Serialize_StockWithShapeApiDocumentation_GeneratesExpectedJsonLD()
        {
            // Arrange

            string expectedJsonLD = File.ReadAllText(
                "expected-api-documentation-with-stock-shape.jsonld");

            var apiDocumentation = new ApiDocumentation(new Uri("https://api.example.com/doc"));
            apiDocumentation.Context.TryAddMapping("doc", new Uri("https://api.example.com/doc#"));

            var stockCategories = new string[]
            {
                "Blue chip",
                "Speculative",
                "Growth",
                "Value",
                "Income",
                "Penny",
                "Cyclical"
            };

            var stockShape = new NodeShape(
                new Uri("doc:Stock"),
                new PropertyShape(new Uri("doc:Stock/category"), stockCategories));

            apiDocumentation.AddSupportedClass<Stock>(stockShape);

            // Act

            string jsonLD = JsonSerializer.Serialize(apiDocumentation, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Assert

            Assert.Equal(expectedJsonLD, jsonLD);
        }
    }
}
