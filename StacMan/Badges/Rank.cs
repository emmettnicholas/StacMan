using System.Text.Json.Serialization;

namespace StackExchange.StacMan.Badges
{
    /// <summary>
    /// rank
    /// </summary>
    public enum Rank
    {
        /// <summary>
        /// gold
        /// </summary>
        [JsonPropertyName("gold")]
        Gold,

        /// <summary>
        /// silver
        /// </summary>
        [JsonPropertyName("silver")]
        Silver,

        /// <summary>
        /// bronze
        /// </summary>
        [JsonPropertyName("bronze")]
        Bronze
    }
}
