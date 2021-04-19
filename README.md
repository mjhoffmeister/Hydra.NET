# Hydra.NET

Hydra.NET is a simple library that provides building blocks for creating hypermedia-driven web APIs with the [Hydra specification](https://www.hydra-cg.com/spec/latest/core/).



## Usage

### Supported classes

Designate classes supported by the API by decorating them with `[SupportedClass]`. Specify their supported properties with `[SupportedProperty]`.

```csharp
[SupportedClass("doc:Stock", Title = "Stock", Description = "Represents a stock.")]
public class Stock
{
    public Stock(Uri id, string symbol, double currentPrice)
    {
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
}
```
### Collections

If you'd like to document a collection for a supported class, you can do so by additionally decorating it with `[SupportedCollection]`.

```csharp
[SupportedClass("doc:Stock", Title = "Stock", Description = "Represents a stock.")]
[SupportedCollection("doc:StockCollection", Title = "Stocks", Description = "Stock listing")]
public class Stock
{
    // Class code here
}
```
### Supported operations

Supported operations for a class are designated by decorating methods with `[Operation]`.

```csharp
public class StocksController
{
    [Operation(typeof(Collection<Stock>), Title = "List stocks", Method = Method.Get)]
    public void Get()
    {
        // Method code here
    }

    [Operation(typeof(Stock), Title = "Update stock", Method = Method.Put)]
    public void Put()
    {
        // Method code here
    }
}
```
### API documentation

The `ApiDocumentation` class is the central documentation source for a Hydra web API. Add your supported classes to it via the `AddSupportedClass<T>()` method.

```csharp
// Create a new API documentation
var apiDocumentation = new ApiDocumentation(new Uri("https://api.example.com/doc"));

// Add Stock as a supported class
apiDocumentation.AddSupportedClass<Stock>();
```

The [context](https://www.w3.org/2018/jsonld-cg-reports/json-ld/#the-context) of `ApiDocumentation` instances is initialized with with Hydra, RDF, RDFS, and XSD mappings. Future versions of Hydra.NET may make this more dynamic. Nevertheless, you can add your own context mappings:
```csharp
apiDocumentation.Context.TryAddMapping("doc", new Uri("https://api.example.com/doc#"));
```
Operations are automatically discovered for their associated types. Given the above examples, the result of serializing `apiDocumentation` will be the following JSON-LD:
```json
{
  "@context": {
    "hydra": "https://www.w3.org/ns/hydra/core#",
    "rdf": "http://www.w3.org/1999/02/22-rdf-syntax-ns#",
    "rdfs": "http://www.w3.org/2000/01/rdf-schema#",
    "xsd": "http://www.w3.org/2001/XMLSchema#",
    "ApiDocumentation": "hydra:ApiDocumentation",
    "Class": "hydra:Class",
    "Collection": "hydra:Collection",
    "description": "hydra:description",
    "memberAssertion": "hydra:memberAssertion",
    "object": "hydra:object",
    "Operation": "hydra:Operation",
    "property": "hydra:property",
    "range": "rdfs:range",
    "readable": "hydra:readable",
    "required": "hydra:required",
    "supportedClass": "hydra:supportedClass",
    "supportedOperation": "hydra:supportedOperation",
    "supportedProperty": "hydra:supportedProperty",
    "SupportedProperty": "hydra:SupportedProperty",
    "title": "hydra:title",
    "writable": "hydra:writable",
    "doc": "https://api.example.com/doc#"
  },
  "@id": "https://api.example.com/doc",
  "@type": "ApiDocumentation",
  "supportedClass": [
    {
      "@id": "doc:Stock",
      "@type": "Class",
      "title": "Stock",
      "description": "Represents a stock.",
      "supportedProperty": [
        {
          "@type": "SupportedProperty",
          "title": "Stock symbol",
          "required": true,
          "readable": true,
          "writable": false,
          "property": {
            "@id": "doc:Stock/symbol",
            "range": "xsd:string"
          }
        },
        {
          "@type": "SupportedProperty",
          "title": "Current price",
          "description": "The current price of the stock.",
          "required": true,
          "readable": true,
          "writable": true,
          "property": {
            "@id": "doc:Stock/currentPrice",
            "range": "xsd:decimal"
          }
        }
      ],
      "supportedOperation": [
        {
          "@type": "Operation",
          "title": "Update stock",
          "method": "PUT"
        }
      ]
    },
    {
      "@id": "doc:StockCollection",
      "@type": "Collection",
      "title": "Stocks",
      "description": "Stock listing",
      "memberAssertion": {
        "property": "rdf:type",
        "object": "doc:Stock"
      },
      "supportedOperation": [
        {
          "@type": "Operation",
          "title": "List stocks",
          "method": "GET"
        }
      ]
    }
  ]
}
```
# Contribute

Hydra.NET is a project I created for use in my personal projects without much expectation that others would be interested. Nevertheless, pull requests and issues are welcome. I've only implemented the parts of the Hydra spec that I've needed, and my understanding of Hydra and JSON-LD is certainly incomplete.

