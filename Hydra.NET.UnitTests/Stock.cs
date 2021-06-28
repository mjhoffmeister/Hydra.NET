using System;
using System.Text.Json.Serialization;

namespace Hydra.NET.UnitTests
{
    [SupportedClass("doc:Stock", Title = "Stock", Description = "Represents a stock.")]
    [SupportedCollection("doc:StockCollection", Title = "Stocks", Description = "Stock listing")]
    public class Stock
    {
        public Stock(Uri id, string symbol, double currentPrice, string category)
        {
            Category = category;
            CurrentPrice = currentPrice;
            Id = id;
            Symbol = symbol;
        }

        [JsonPropertyName("@id")]
        public Uri Id { get; }

        [SupportedProperty(
            "doc:Stock/symbol",
            Xsd.String,
            Title = "Stock symbol",
            IsWritable = false)]
        [JsonPropertyName("symbol")]
        public string Symbol { get; }

        [SupportedProperty(
            "doc:Stock/currentPrice",
            Xsd.Decimal,
            Title = "Current price",
            Description = "The current price of the stock.")]
        [JsonPropertyName("currentPrice")]
        public double CurrentPrice { get; }

        [SupportedProperty(
            "doc:Stock/category",
            Xsd.String,
            Title = "Category",
            IsRequired = false)]
        public string? Category { get; }
    }
}
