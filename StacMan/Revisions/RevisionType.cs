using System.Text.Json.Serialization;

namespace StackExchange.StacMan.Revisions
{
    /// <summary>
    /// revision_type
    /// </summary>
    public enum RevisionType
    {
        /// <summary>
        /// single_user
        /// </summary>
        [JsonPropertyName("single_user")]
        SingleUser,

        /// <summary>
        /// vote_based
        /// </summary>
        [JsonPropertyName("vote_based")]
        VoteBased
    }
}