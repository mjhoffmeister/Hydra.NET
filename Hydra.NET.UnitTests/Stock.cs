using System;
using System.Text.Json.Serialization;

namespace Hydra.NET.UnitTests
{
    [SupportedClass("Stock", Title = "Stock", Description = "Represents a stock.")]
    [SupportedCollection("StockCollection", Title = "Stocks", Description = "Stock listing")]
    public class Stock
    {
        public Stock(Uri id, string symbol, double currentPrice, string? category = null)
        {
            Category = category;
            CurrentPrice = currentPrice;
            Id = id;
            Symbol = symbol;
        }

        [JsonPropertyName("@id")]
        public Uri Id { get; }

        [SupportedProperty(
            "Stock/symbol",
            Xsd.String,
            Title = "Stock symbol",
            IsWritable = false)]
        [JsonPropertyName("symbol")]
        public string Symbol { get; }

        [SupportedProperty(
            "Stock/currentPrice",
            Xsd.Decimal,
            Title = "Current price",
            Description = "The current price of the stock.")]
        [JsonPropertyName("currentPrice")]
        public double CurrentPrice { get; }

        [SupportedProperty(
            "Stock/category",
            Xsd.String,
            Title = "Category",
            IsRequired = false)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Category { get; }
    }
}
