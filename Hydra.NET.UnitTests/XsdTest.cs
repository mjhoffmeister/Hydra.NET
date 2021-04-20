using Xunit;

namespace Hydra.NET.UnitTests
{
    public static class XsdTest
    {
        [Theory]
        [InlineData(Xsd.Boolean, "xsd:boolean")]
        [InlineData(Xsd.DateTime, "xsd:dateTime")]
        [InlineData(Xsd.Decimal, "xsd:decimal")]
        [InlineData(Xsd.Double, "xsd:double")]
        [InlineData(Xsd.Float, "xsd:float")]
        [InlineData(Xsd.Integer, "xsd:integer")]
        [InlineData(Xsd.String, "xsd:string")]
        public static void AccessField_Multiple_ReturnsExpectedValue(
            string fieldValue, string expectedValue)
        {
            // Assert

            Assert.Equal(expectedValue, fieldValue);
        }
    }
}
