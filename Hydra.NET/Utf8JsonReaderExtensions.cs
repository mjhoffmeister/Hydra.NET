using System;
using System.Text.Json;

namespace Hydra.NET
{
    internal static class Utf8JsonReaderExtensions
    {
        /// <summary>
        /// Tries to get a <see cref="MemberAssertion"/> from the current position in a 
        /// <see cref="Utf8JsonReader"/>.
        /// </summary>
        /// <param name="reader"><see cref="Utf8JsonReader"/>.</param>
        /// <returns>
        /// A <see cref="MemberAssertion"/> if it could be deserialized; null, otherwise.
        /// </returns>
        public static MemberAssertion? TryGetMemberAssertion(this Utf8JsonReader reader)
        {
            string? memberAssertionJson = reader.GetString();
            return memberAssertionJson != null ?
                JsonSerializer.Deserialize<MemberAssertion>(memberAssertionJson) :
                null;
        }

        /// <summary>
        /// Tries to get a <see cref="Uri"/> from the current position in a 
        /// <see cref="Utf8JsonReader"/>.
        /// </summary>
        /// <param name="reader"><see cref="Utf8JsonReader"/>.</param>
        /// <returns>
        /// A <see cref="Uri"/> if it could be deserialized; null, otherwise.
        /// </returns>
        public static Uri? TryGetUri(this Utf8JsonReader reader)
        {
            string? uriString = reader.GetString();
            return uriString != null ? new Uri(uriString) : null;
        }
    }
}
