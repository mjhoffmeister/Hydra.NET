using System;
using System.IO;
using System.Text.Json;
using Xunit;

namespace Hydra.NET.UnitTests
{
    public static class LinkTests
    {
        
        [Fact]
        public static void Deserialize_ValidLinkJsonLD_CreatesLinkObject()
        {
            // Arrange

            string linkJsonLD = File.ReadAllText("expected-link.jsonld");

            // Act

            Link? link = JsonSerializer.Deserialize<Link>(linkJsonLD);

            // Assert

            Assert.NotNull(link);
        }

        [Fact]
        public static void Serialize_ValidLink_ReturnsExpectedJsonLD()
        {
            // Arrange

            string expectedJsonLD = File.ReadAllText("expected-link.jsonld");

            Link link = new(
                new Uri("https://api.example.com/stocks"),
                "Stocks",
                "A link to the stocks collection.");

            // Act

            string jsonLD = JsonSerializer.Serialize(link, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Assert

            Assert.Equal(expectedJsonLD, jsonLD);
        }
    }
}
