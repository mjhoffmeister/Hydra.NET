using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Hydra.NET
{
    internal static class JsonElementExtensions
    {
        public static IEnumerable<T>? TryDeserializeEnumerable<T>(
            this JsonElement jsonElement, string propertyName)
        {
            if (!jsonElement.TryGetProperty(propertyName, out JsonElement value))
                return null;

            string json = value.GetRawText();

            if (json[0] == '[' && json[^1] == ']')
                return JsonSerializer.Deserialize<IEnumerable<T>>(json);

            return null;
        }

        /// <summary>
        /// Tries to deserialize the value of a property in a <see cref="JsonElement"/>.
        /// </summary>
        /// <typeparam name="T">Derserialization type.</typeparam>
        /// <param name="jsonElement"><see cref="JsonElement"/>.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>The deserialized JSON if successful; null, otherwise.</returns>
        public static T? TryDeserializeValue<T>(
            this JsonElement jsonElement, string propertyName) where T : class
        {
            if (!jsonElement.TryGetProperty(propertyName, out JsonElement value))
                return null;

            string json = value.GetRawText();

            return JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// Tries to get a the value of a property in a <see cref="JsonElement"/> as a 
        /// <see cref="string"/>.
        /// </summary>
        /// <param name="jsonElement"><see cref="JsonElement"/>.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>The <see cref="string"/> value if successful; null, otherwise.</returns>
        public static string? TryGetStringValue(this JsonElement jsonElement, string propertyName)
        {
            if (!jsonElement.TryGetProperty(propertyName, out JsonElement value))
                return null;

            return value.GetString();
        }

        /// <summary>
        /// Tries to get the value of a property in a <see cref="JsonElement"/> as a 
        /// <see cref="Uri/>.
        /// </summary>
        /// <param name="jsonElement"><see cref="JsonElement"/>.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>The <see cref="Uri"/> value if successful; null, otherwise.</returns>
        public static Uri? TryGetUriValue(this JsonElement jsonElement, string propertyName)
        {
            string? uriString = jsonElement.TryGetStringValue(propertyName);

            return uriString == null ? null : new Uri(uriString);
        }
    }
}
