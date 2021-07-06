namespace Hydra.NET
{
    /// <summary>
    /// Constant string values for <see cref="Operation"/> methods.
    /// </summary>
    public static class Method
    {
        public const string Delete = "DELETE";

        public const string Get = "GET";

        public const string Patch = "PATCH";

        public const string Post = "POST";

        public const string Put = "PUT";

        /// <summary>
        /// Determines whether a method is a method that updates a resource.
        /// </summary>
        /// <param name="method">The method to evaluate.</param>
        /// <returns>
        /// <see cref="true"/> if the method updates a resource; <see cref="false"/>, otherwise.
        /// </returns>
        public static bool IsUpdateMethod(string? method) =>
            method != null && (method == Patch || method == Post || method == Put);
    }
}
