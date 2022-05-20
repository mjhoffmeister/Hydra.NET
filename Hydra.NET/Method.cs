namespace Hydra.NET
{
    /// <summary>
    /// Constant string values for <see cref="Operation"/> methods.
    /// </summary>
    public static class Method
    {
        /// <summary>
        /// DELETE.
        /// </summary>
        public const string Delete = "DELETE";

        /// <summary>
        /// GET.
        /// </summary>
        public const string Get = "GET";

        /// <summary>
        /// PATCH.
        /// </summary>
        public const string Patch = "PATCH";

        /// <summary>
        /// POST.
        /// </summary>
        public const string Post = "POST";

        /// <summary>
        /// PUT.
        /// </summary>
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
