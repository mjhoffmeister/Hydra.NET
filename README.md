# Hydra.NET

Hydra.NET is a simple library for ASP.NET that provides the building blocks necessary for creating hypermedia-driven Web APIs with the [Hydra specification](https://www.hydra-cg.com/spec/latest/core/).

## Quickstart

This quickstart will show you how to create a basic Hello World ASP.NET Hydra Service with the following endpoints:
| Resource   | Operation      | Description                       |
|------------|----------------|-----------------------------------|
| EntryPoint | GET /api       | Get info for Entry Point resource |
| ApiDoc     | GET /api/doc   | Get info for Api Doc resource     |
| Hello      | GET /api/hello | Get info for Hello resource       |

Full code for the finished quickstart is available at [Hydra.NET.Examples/HelloHydraService](https://github.com/lambdakris/Hydra.NET.Examples/tree/main/HelloHydraService)

### Pre-requisites
- Git
- .NET 5 SDK
- VS Code, VS IDE, or Rider

### Create an ASP.NET application

In a terminal, execute the following to create an ASP.NET project using the Web template:
```
dotnet new web --name HelloHydraService
```

Execute the following to change into the project directory to work within the project:
```
cd HelloHydraService
```

### Install package dependencies

Execute the following to install the Hydra.NET library (currently only available as a prerelease) which we will use to generate the Hydra Api Doc:
```
dotnet add package Hydra.NET --prerelease
```

Execute the following to install the JsonLd.Entities library which we will use to generate the JSON-LD Context (Hydra builds on top of JSON-LD):
```
dotnet add package JsonLd.Entities
```

Execute the following to install the Newtonsoft.Json library which is a dependency of JsonLd.Entities (there is a plan to update this to System.Text.Json):
```
dotnet add package Newtonsoft.Json
```

### Configure Startup

Execute the following to open the project in VS Code so we can start browsing and editing the source:
```
code .
```

Open the 'Startup.cs' file and replace the contents with the following:
```csharp
namespace HelloHydraService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection svc)
        {
            svc.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(ept =>
            {
                ept.MapControllers();
            });
        }
    }
}
```

Now go ahead and create a new folder named 'Resources' where we will place the source for each of the Resources managed by our API


### Define the ApiDoc resource

The first resource we are going to define is the ApiDoc resource, which is specified by Hydra to serve as a reference to the rest of the API for the benefit of clients. For the most part, the ApiDoc is automatically generated by Hydra.NET, so let's go ahead and implement the resource to see what the output looks like.

Under 'Resources', create a new file called 'ApiDoc.cs' and enter the following:
```csharp
namespace HelloHydraService.Resources.Help
{
  using System;
  using Microsoft.AspNetCore.Mvc;
  using Hydra.NET;

  [ApiController]
  public class HelpInfoController
  {
    [HttpGet("/api/doc")]
    public IActionResult GetInfo()
    {
      var info = new ApiDocumentation(new Uri("/api/doc", UriKind.RelativeOrAbsolute));

      return new OkObjectResult(info);
    }
  }
}
```

Launch the debugger and in the browser, navigate to `http://localhost:5000/api/doc` to verify that the output looks like:
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
    "writable": "hydra:writable"
  },
  "@id": "/api/doc",
  "@type": "ApiDocumentation",
  "supportedClass": []
}
```

You now have a working ApiDoc resource.

### Define the EntryPoint resource

The second resource we are going to define is the EntryPoint resource, which is the root of our API and contains links to all the other top level API resources. The info for the EntryPoint resource should be represented like this:
```json
{
  "@id": "/api",
  "@type": "EntryPoint",
  "@context": "/api/doc",
  "helloInfo": ""
}
```

Under 'Resources/', create a new file called 'EntryPoint.cs' and enter the following:
```csharp
namespace HelloHydraService.Resources.EntryPoint
{
  using System;
  using System.Dynamic;
  using System.Threading.Tasks;
  using System.Collections.Generic;
  using System.Text.Json;
  using System.Text.Json.Serialization;
  using Microsoft.AspNetCore.Mvc;
  using JsonLD.Entities.Context;
  using Hydra.NET;

  [SupportedClass("doc:EntryPoint", Title = "EntryPoint", Description = "Represents the EntryPoint Info")]
  public class EntryPointInfo
  {
    [JsonPropertyName("@id")]
    public Uri Id { get; set; }

    [JsonPropertyName("@type")]
    public string Type { get; set; }  

    [JsonPropertyName("@context")]
    public Object Context { get; set; }

    public string HelloInfo { get; set; }
  }

  [ApiController]
  public class EntryPointController
  {
    [HttpGet("/api")]
    [Operation(typeof(EntryPointInfo), Title = "Query EntryPoint Info", Method = Method.Get)]
    public IActionResult QueryInfo()
    {
      var info = new EntryPointInfo
      {
        Id = new Uri("/api", UriKind.RelativeOrAbsolute),
        Type = "EntryPoint",
        Context = new VocabContext<EntryPointInfo>("/api/doc#").ToObject<ExpandoObject>(),
        HelloInfo = "/api/hello"
      };

      return new OkObjectResult(info);
    }
  }
}
```

Launch the debugger and in the browser, navigate to `http://localhost:5000/api` to verify that the output looks like:
```json
{
  "@id": "/api",
  "@type": "EntryPoint",
  "@context": {
    "helloInfo": "/api/doc#helloInfo"
  },
  "helloInfo": "/api/hello"
}
```

You now have a way to query to info for the EntryPoint resource, but in order to ensure that the API is discoverable, we still need to register the EntryPoint resource with the ApiDoc resource. Update the 'ApiDoc.cs' file to match the following:
```csharp
namespace HelloHydraService.Resources.ApiDoc
{
  using System;
  using Microsoft.AspNetCore.Mvc;
  using Hydra.NET;

  using Resources.EntryPoint;

  [ApiController]
  public class HelpInfoController
  {
    [HttpGet("/api/doc")]
    public IActionResult GetInfo()
    {
      var info = new ApiDocumentation(new Uri("/api/doc", UriKind.RelativeOrAbsolute));

      info.AddSupportedClass<EntryPointInfo>();

      return new OkObjectResult(info);
    }
  }
}
```

Launch the debugger and in the browser, navigate to 'http://localhost:5000/api/doc' to verify that the output looks like:
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
    "writable": "hydra:writable"
  },
  "@id": "/api/doc",
  "@type": "ApiDocumentation",
  "supportedClass": [
    {
      "@id": "doc:EntryPoint",
      "@type": "Class",
      "title": "EntryPoint",
      "description": "Represents the EntryPoint Info",
      "supportedOperation": [
        {
          "@type": "Operation",
          "title": "Query EntryPoint Info",
          "method": "GET"
        }
      ]
    }
  ]
}
```

You now have a way to query the info for the EntryPoint resource aswell as a way to discover the EntryPoint resource.

### Define the Hello resource

The last resource we will define in this quickstart is the Hello resource. The Hello resource is simply a toy example resource that is responsible for greeting a user with a 'Hello' message.

Under 'Resources/', create a file called 'Hello.cs' and enter the following:
```csharp
namespace HelloHydraService.Resources.Hello
{
  using System;
  using System.Dynamic;
  using System.Threading.Tasks;
  using System.Collections.Generic;
  using System.Text.Json;
  using System.Text.Json.Serialization;
  using Microsoft.AspNetCore.Mvc;
  using JsonLD.Entities.Context;
  using Hydra.NET;

  [SupportedClass("doc:Hello")]
  public class HelloInfo
  {
    [JsonPropertyName("@id")]
    public Uri Id { get; set; }
    [JsonPropertyName("@type")]
    public string Type { get; set; }
    [JsonPropertyName("@context")]
    public object Context { get; set; }
    
    public string Message { get; set; }
  }

  [ApiController]
  public class HelloController
  {
    [HttpGet("/api/hello")]
    [Operation(typeof(HelloInfo), Title = "Query Hello Info", Method = Method.Get)]
    public IActionResult QueryHelloInfo()
    {
      var info = new HelloInfo 
      {
        Id = new Uri("/api/hello", UriKind.RelativeOrAbsolute),
        Type = "Class",
        Context = new VocabContext<HelloInfo>("/api/doc#").ToObject<ExpandoObject>(),
        Message = "Hello"
      };

      return new OkObjectResult(info);
    }
  }
}
```

Launch the debugger and in the browser, navigate to `http://localhost:5000/api/hello` to verify the output:
```json
{
  "@id": "/api/hello",
  "@type": "Class",
  "@context": {
    "message": "/api/doc#message"
  },
  "message": "Hello"
}
```

You now have a way to query the info for the Home resource, but to ensure that it is discoverable, we need to register the Hello resource with the Api Doc resource. Update the 'ApiDoc.cs' file to match the following:
```csharp
namespace Resources.ApiDoc
{
  using System;
  using Microsoft.AspNetCore.Mvc;
  using Hydra.NET;

  using Resources.EntryPoint;
  using Resources.Hello;

  [ApiController]
  public class HelpInfoController
  {
    [HttpGet("/api/doc")]
    public IActionResult GetInfo()
    {
      var info = new ApiDocumentation(new Uri("/api/doc", UriKind.RelativeOrAbsolute));

      info.AddSupportedClass<EntryPointInfo>();
      info.AddSupportedClass<HelloInfo>();

      return new OkObjectResult(info);
    }
  }
}
```

Launch the debugger and in the browser, navigate to `http://localhost:5000/api/doc` to verify the output:
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
    "writable": "hydra:writable"
  },
  "@id": "/api/doc",
  "@type": "ApiDocumentation",
  "supportedClass": [
    {
      "@id": "doc:EntryPoint",
      "@type": "Class",
      "supportedOperation": [
        {
          "@type": "Operation",
          "title": "Query EntryPoint Info",
          "method": "GET"
        }
      ]
    },
    {
      "@id": "doc:Hello",
      "@type": "Class",
      "supportedOperation": [
        {
          "@type": "Operation",
          "title": "Query Hello Info",
          "method": "GET"
        }
      ]
    }
  ]
}
```

You now have a way to query the info for the Hello resource as well as a way to discover the Hello resource.
## Reference

### API Resources and the Hydra Class

To describe a Hydra Supported Class, decorate the model type that represents a resource with the `[SupportedClass]` attribute. Describe the supported properties with the `[SupportedProperty]` attribute.

```csharp
[SupportedClass("Stock", Title = "Stock", Description = "Represents a stock.")]
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
}
```
If you'd like to add your API documentation context prefix to a supported property's range, set `AddApiDocumentationPrefixToRange` to `true`.

```csharp
[SupportedClass("Star")]
public class Star
{
    [SupportedProperty("Star/name", Xsd.String)]
    public string? Name { get; set; }

    [SupportedProperty(
        "Star/classification",
        "StellarClassification",
        AddApiDocumentationPrefixToRange = true
    )]
    public string? Classification { get; set; }
}
```

### API Collections and the Hydra Collection

To describe a Hydra Collection, decorate the class that represents an item in the collection with the `[SupportedCollection]` attribute.

```csharp
[SupportedClass("Stock", Title = "Stock", Description = "Represents a stock.")]
[SupportedCollection("StockCollection", Title = "Stocks", Description = "Stock listing")]
public class Stock
{
    // Class code here
}
```
Note that "SupportedCollection" isn't a part of the Hydra core vocabulary. It's used as a convention in Hydra.NET.

### API Operations and the Hydra Operation

To describe Hydra Supported Operations, decorate the controller method that represents the operation with the `[Operation]` attribute.

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
While the above is sufficient for static documentation of operations, you can use the `Operation` class in your resource objects to add dynamic links.

```csharp
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
[JsonPropertyName("operation")]
public IEnumerable<Operation>? Operations { get; set; }
```

### API Documentation and the Hydra ApiDocumentation

The `ApiDocumentation` class is the central documentation source for a Hydra Web API. Add your supported classes to it via the `AddSupportedClass<T>()` method.

```csharp
// Create a new API documentation
var apiDocumentation = new ApiDocumentation(new Uri("https://api.example.com/doc"), "doc");

// Add Stock as a supported class
apiDocumentation.AddSupportedClass<Stock>();
```
The [context](https://www.w3.org/2018/jsonld-cg-reports/json-ld/#the-context) of `ApiDocumentation` instances is initialized with Hydra, RDF, RDFS, and XSD mappings. Future versions of Hydra.NET may make this more dynamic. Nevertheless, you can add your own context mappings:

```csharp
apiDocumentation.Context.TryAddMapping("schema", new Uri("https://schema.org/"));
```
The `contextPrefix` parameter in the `ApiDocumentation` constructor sets the API documentation context prefix. It will be applied to all added supported classes, properties, and collections automatically.

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
### Shapes Constraints Language (SHACL) Support

Hydra allows for SHACL support through [extensions](https://www.hydra-cg.com/spec/latest/core/#extensions). `Hydra.NET` adds SHACL as an extension by default, though this may be made configurable in later versions. The primary motivation is to specify allowed values for properties via SHACL's ["in" constraint](https://www.w3.org/TR/shacl/#InConstraintComponent). You can add this constraint to your API documentation by including a `NodeShape` and `PropertyShape`s with a `SupportedClass`.

```csharp
var apiDocumentation = new ApiDocumentation(new Uri("https://api.example.com/doc"), "doc");

// These categories would come from the API
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
    "Stock",
    new PropertyShape("Stock/category", stockCategories));

apiDocumentation.AddSupportedClass<Stock>(stockShape);
```
The above example will generate the following JSON-LD for the `Stock` class.

```json
{
  "@id": "doc:Stock",
  "@type": [
    "Class",
    "NodeShape"
  ],
  "title": "Stock",
  "description": "Represents a stock.",
  "propertyShape": [
    {
      "@type": "PropertyShape",
      "path": "doc:Stock/category",
      "in": [
        "Blue chip",
        "Speculative",
        "Growth",
        "Value",
        "Income",
        "Penny",
        "Cyclical"
      ]
    }
  ],
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
    },
    {
      "@type": "SupportedProperty",
      "title": "Category",
      "required": false,
      "readable": true,
      "writable": true,
      "property": {
        "@id": "doc:Stock/category",
        "range": "xsd:string"
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
```

## Contributing

Hydra.NET is a project I created for use in my personal projects without much expectation that others would be interested. Nevertheless, pull requests and issues are welcome. I've only implemented the parts of the Hydra spec that I've needed, and my understanding of Hydra and JSON-LD is certainly incomplete.



