using System.Text.Json.Serialization;

namespace StackExchange.StacMan.UserTimelines
{
    /// <summary>
    /// timeline_type
    /// </summary>
    public enum TimelineType
    {
        /// <summary>
        /// commented
        /// </summary>
        [JsonPropertyName("commented")]
        Commented,

        /// <summary>
        /// asked
        /// </summary>
        [JsonPropertyName("asked")]
        Asked,

        /// <summary>
        /// answered
        /// </summary>
        [JsonPropertyName("answered")]
        Answered,

        /// <summary>
        /// badge
        /// </summary>
        [JsonPropertyName("badge")]
        Badge,

        /// <summary>
        /// revision
        /// </summary>
        [JsonPropertyName("revision")]
        Revision,

        /// <summary>
        /// accepted
        /// </summary>
        [JsonPropertyName("accepted")]
        Accepted,

        /// <summary>
        /// reviewed
        /// </summary>
        [JsonPropertyName("reviewed")]
        Reviewed,

        /// <summary>
        /// suggested
        /// </summary>
        [JsonPropertyName("suggested")]
        Suggested
    }
}
