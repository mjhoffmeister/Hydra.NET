using Newtonsoft.Json;
using System;

namespace Hydra.NET.UnitTests
{
    [SupportedClass("doc:Stock", Title = "Stock", Description = "Represents a stock.")]
    [SupportedCollection("doc:StockCollection", Title = "Stocks", Description = "Stock listing")]
    public class Stock
    {
        public Stock(Uri id, string symbol, double currentPrice)
        {
            CurrentPrice = currentPrice;
            Id = id;
            Symbol = symbol;
        }

        [SupportedProperty(
            "doc:Stock/currentPrice",
            Xsd.Decimal,
            Title = "Current price",
            Description = "The current price of the stock.")]
        [JsonProperty(PropertyName = "currentPrice", Order = 3)]
        public double CurrentPrice { get; }

        [JsonProperty(PropertyName = "@id", Order = 1)]
        public Uri Id { get; }

        [SupportedProperty(
            "doc:Stock/symbol",
            Xsd.String,
            Title = "Stock symbol",
            IsWritable = false)]
        [JsonProperty(PropertyName = "symbol", Order = 2)]
        public string Symbol { get; }
    }
}
