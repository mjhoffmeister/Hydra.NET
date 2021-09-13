using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace Hydra.NET.UnitTests
{
    public class OperationTests
    {
        [Fact]
        public static void Deserialize_StocksWithOperations_CreatesExpectedStockOperations()
        {
            // Arrange

            string stocksWithOperationsJsonLD = 
                File.ReadAllText("expected-stocks-with-operations.jsonld");

            // Act

            IEnumerable<Stock> stocks = JsonSerializer.Deserialize<IEnumerable<Stock>>(
                stocksWithOperationsJsonLD)!;

            // Assert

            Assert.Collection(stocks,
                s => Assert.Equal(2, s.Operations?.Count()),
                s => Assert.Equal(1, s.Operations?.Count()),
                s => Assert.Null(s.Operations),
                s => Assert.Null(s.Operations),
                s => Assert.Null(s.Operations));
        }

        [Fact]
        public static void Serialize_StocksWithOperations_GeneratesExpectedJsonLD()
        {
            // Arrange

            string expectedJsonLD = File.ReadAllText("expected-stocks-with-operations.jsonld");

            IEnumerable<Stock> stocksWithOperations = GetStocksWithOperations();

            // Act

            string jsonLD = JsonSerializer.Serialize(stocksWithOperations, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Assert

            Assert.Equal(expectedJsonLD, jsonLD);
        }

        /// <summary>
        /// Gets a collection of stocks with operations.
        /// </summary>
        /// <returns><see cref="IEnumerable{Stock}"/></returns>
        public static IEnumerable<Stock> GetStocksWithOperations()
        {
            Context context = new(new Uri("https://api.example.com/doc#"));

            Operation deleteStock = new(Method.Delete);
            Operation updateStock = new(Method.Put);

            Operation[] deleteAndUpdateOperations = new[] { deleteStock, updateStock };
            Operation[] updateOperation = new[] { updateStock };

            return new[]
            {
                new Stock(
                    context,
                    new Uri("https://api.example.com/stocks/1"),
                    "ABC",
                    12.34,
                    operations: deleteAndUpdateOperations),
                new Stock(
                    context,
                    new Uri("https://api.example.com/stocks/2"),
                    "DEF",
                    0.09,
                    operations: updateOperation),
                new Stock(context, new Uri("https://api.example.com/stocks/3"), "GH", 1927.2),
                new Stock(context, new Uri("https://api.example.com/stocks/4"), "I", 386.92),
                new Stock(context, new Uri("https://api.example.com/stocks/5"), "JKL", 5.5),
            };
        }
    }
}
