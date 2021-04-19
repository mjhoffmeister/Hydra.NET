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

            //string jsonLD = JsonConvert.SerializeObject(apiDocumentation, Formatting.Indented);
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

            //ApiDocumentation apiDocumentation = JsonConvert.DeserializeObject<ApiDocumentation>(
            //    apiDocumentationWithStockClassJsonLD);
            ApiDocumentation apiDocumentation = JsonSerializer.Deserialize<ApiDocumentation>(
                apiDocumentationWithStockClassJsonLD);

            // Assert

            Assert.Equal(
                expectedSupportedClassId,
                apiDocumentation.SupportedClasses.First().Id.ToString());
        }
    }
}
