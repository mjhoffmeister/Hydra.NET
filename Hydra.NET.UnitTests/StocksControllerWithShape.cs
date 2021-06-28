namespace Hydra.NET.UnitTests
{
    public class StocksControllerWithShape
    {
        [Operation(
            typeof(Stock),
            Title = "Add stock",
            Method = Method.Post)
        ]
        public void Post()
        {
            // Intentionally left blank
        }
    }
}
