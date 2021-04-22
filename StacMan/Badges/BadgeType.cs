using System.Text.Json.Serialization;

namespace StackExchange.StacMan.Badges
{
    /// <summary>
    /// badge_type
    /// </summary>
    public enum BadgeType
    {
        /// <summary>
        /// name
        /// </summary>
        [JsonPropertyName("name")]
        Named,

        /// <summary>
        /// tag_based
        /// </summary>
        [JsonPropertyName("tag_based")]
        TagBased
    }
}
