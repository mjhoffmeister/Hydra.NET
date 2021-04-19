using System;
using System.IO;
using System.Text.Json;
using Xunit;

namespace Hydra.NET.UnitTests
{
    public static class CollectionTest
    {
        [Fact]
        public static void Serialize_StockCollectionNoOperations_ReturnsExpectedJson()
        {
            // Arrange

            string expectedJsonLD = File.ReadAllText("expected-stock-collection.jsonld");

            // Create stocks
            var stocks = new Stock[]
            {
                new Stock(new Uri("https://api.example.com/stocks/1"), "ABC", 0.32),
                new Stock(new Uri("https://api.example.com/stocks/2"), "XYZ", 351.74)
            };

            // Create a stock collection
            var stockCollection = new Collection<Stock>(
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
    }
}
