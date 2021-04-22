using System.Text.Json.Serialization;

namespace StackExchange.StacMan.Filters
{
    /// <summary>
    /// filter_type
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// safe
        /// </summary>
        [JsonPropertyName("safe")]
        Safe,

        /// <summary>
        /// unsafe
        /// </summary>
        [JsonPropertyName("unsafe")]
        Unsafe,

        /// <summary>
        /// invalid
        /// </summary>
        [JsonPropertyName("invalid")]
        Invalid
    }
}