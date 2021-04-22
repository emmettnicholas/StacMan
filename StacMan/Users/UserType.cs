using System.Text.Json.Serialization;

namespace StackExchange.StacMan.Users
{
    /// <summary>
    /// user_type
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// unregistered
        /// </summary>
        [JsonPropertyName("unregistered")]
        Unregistered,

        /// <summary>
        /// registered
        /// </summary>
        [JsonPropertyName("registered")]
        Registered,

        /// <summary>
        /// moderator
        /// </summary>
        [JsonPropertyName("moderator")]
        Moderator,

        /// <summary>
        /// does_not_exist
        /// </summary>
        [JsonPropertyName("does_not_exist")]
        DoesNotExist
    }
}