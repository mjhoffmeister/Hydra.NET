using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hydra.NET.UnitTests
{
    [SupportedClass("Stock", Title = "Stock", Description = "Represents a stock.")]
    [SupportedCollection("StockCollection", Title = "Stocks", Description = "Stock listing")]
    public class Stock
    {
        public Stock() { }

        public Stock(
            Context? context,
            Uri id,
            string symbol,
            double currentPrice,
            string? category = null,
            IEnumerable<Operation>? operations = null)
        {
            Context = context;
            Category = category;
            CurrentPrice = currentPrice;
            Id = id;
            Operations = operations;
            Symbol = symbol;
        }

        public Stock(Uri id, string symbol, double currentPrice, string? category = null)
            : this(null, id, symbol, currentPrice, category) { }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@context")]
        public Context? Context { get; set; }

        [JsonPropertyName("@id")]
        public Uri? Id { get; set; }

        [SupportedProperty(
            "Stock/symbol",
            Xsd.String,
            Title = "Stock symbol",
            IsWritable = false)]
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [SupportedProperty(
            "Stock/currentPrice",
            Xsd.Decimal,
            Title = "Current price",
            Description = "The current price of the stock.")]
        [JsonPropertyName("currentPrice")]
        public double CurrentPrice { get; set; }

        [SupportedProperty(
            "Stock/category",
            Xsd.String,
            Title = "Category",
            IsRequired = false)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Category { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("operation")]
        public IEnumerable<Operation>? Operations { get; set; }
    }
}
