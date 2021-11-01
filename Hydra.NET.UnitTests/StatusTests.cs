using System;
using System.IO;
using System.Text.Json;
using Xunit;

namespace Hydra.NET.UnitTests
{
    public static class StatusTests
    {
        [Fact]
        public static void Serialize_StatusWithContext_ReturnsExpectedJsonLD()
        {
            // Arrange

            string expectedJsonLD = File.ReadAllText("expected-status-with-context.jsonld");

            Status status = new(
                new Context(new Uri("http://www.w3.org/ns/hydra/context.jsonld")),
                401,
                "Unauthorized",
                "You are not authorized to edit stocks.");

            // Act

            string jsonLD = JsonSerializer.Serialize(status, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Assert

            Assert.Equal(expectedJsonLD, jsonLD);
        }

        [Fact]
        public static void Serialize_StatusWithoutContext_ReturnsExpectedJsonLD()
        {
            // Arrange

            string expectedJsonLD = File.ReadAllText("expected-status-without-context.jsonld");

            Status status = new(
                "hydra",
                401,
                "Unauthorized",
                "You are not authorized to edit stocks.");

            // Act

            string jsonLD = JsonSerializer.Serialize(status, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Assert

            Assert.Equal(expectedJsonLD, jsonLD);
        }
    }
}
