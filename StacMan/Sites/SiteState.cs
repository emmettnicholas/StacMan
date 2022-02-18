using System.Text.Json.Serialization;

namespace StackExchange.StacMan.Sites
{
    /// <summary>
    /// site_state
    /// </summary>
    public enum SiteState
    {
        /// <summary>
        /// normal
        /// </summary>
        [JsonPropertyName("normal")]
        Normal,
        
        /// <summary>
        /// closed_beta
        /// </summary>
        [JsonPropertyName("closed_beta")]
        ClosedBeta,

        /// <summary>
        /// open_beta
        /// </summary>
        [JsonPropertyName("open_beta")]
        OpenBeta,

        /// <summary>
        /// linked_meta
        /// </summary>
        [JsonPropertyName("linked_meta")]
        LinkedMeta
    }
}