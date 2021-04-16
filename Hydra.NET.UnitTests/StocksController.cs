namespace Hydra.NET.UnitTests
{
    /// <summary>
    /// Used to test <see cref="OperationAttribute"/>.
    /// </summary>
    public class StocksController
    {
        [Operation(typeof(Collection<Stock>), Title = "List stocks", Method = Method.Get)]
        public void Get()
        {
            // Intentionally left blank
        }

        [Operation(typeof(Stock), Title = "Update stock", Method = Method.Put)]
        public void Put()
        {
            // Intentionally left blank
        }
    }
}
