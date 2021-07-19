using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Xunit;

namespace Hydra.NET.UnitTests
{
    public static class CollectionTests
    {
        [Fact]
        public static void Serialize_StockCollectionNoOperations_ReturnsExpectedJson()
        {
            // Arrange

            string expectedJsonLD = File.ReadAllText("expected-stock-collection.jsonld");

            // Create context
            Context context = new(new Dictionary<string, Uri>()
            {
                { "hydra", new Uri("http://www.w3.org/ns/hydra/core#") },
                { "Collection", new Uri("hydra:Collection") },
                { "member", new Uri("hydra:member") }
            });

            // Create stocks
            var stocks = new Stock[]
            {
                new Stock(new Uri("https://api.example.com/stocks/1"), "ABC", 0.32),
                new Stock(new Uri("https://api.example.com/stocks/2"), "XYZ", 351.74)
            };

            // Create a stock collection
            var stockCollection = new Collection<Stock>(
                context,
                new Uri("https://api.example.com/stocks"),
                stocks);

            // Act

            string jsonLD = JsonSerializer.Serialize(stockCollection, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Assert

            Assert.Equal(expectedJsonLD, jsonLD);
        }

        [Fact]
        public static void Serialize_StockCollectionWithMemberAssertion_ReturnsExpectedJsonLD()
        {
            // Arrange

            string expectedJsonLD = File.ReadAllText(
                "expected-stock-collection-with-member-assertion.jsonld");

            // Create context
            Context context = new(new Dictionary<string, Uri>()
            {
                { "hydra", new Uri("http://www.w3.org/ns/hydra/core#") },
                { "Collection", new Uri("hydra:Collection") },
                { "member", new Uri("hydra:member") }
            });

            // Create stocks
            var stocks = new Stock[]
            {
                new Stock(new Uri("https://api.example.com/stocks/1"), "ABC", 0.32),
                new Stock(new Uri("https://api.example.com/stocks/2"), "XYZ", 351.74)
            };

            // Create member assertion
            var memberAssertion = new MemberAssertion(new Uri(Rdf.Type), new Uri("doc:Stock"));

            // Create a stock collection
            var stockCollection = new Collection<Stock>(
                context,
                new Uri("https://api.example.com/stocks"),
                memberAssertion,
                stocks);

            // Act

            string jsonLD = JsonSerializer.Serialize(stockCollection, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Assert

            Assert.Equal(expectedJsonLD, jsonLD);
        }
    }
}
