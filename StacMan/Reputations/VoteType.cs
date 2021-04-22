using System.Text.Json.Serialization;

namespace StackExchange.StacMan.Reputations
{
    /// <summary>
    /// vote_type
    /// </summary>
    public enum VoteType
    {
        /// <summary>
        /// accepts
        /// </summary>
        [JsonPropertyName("accepts")]
        Accepts,

        /// <summary>
        /// up_votes
        /// </summary>
        [JsonPropertyName("up_votes")]
        UpVotes,

        /// <summary>
        /// down_votes
        /// </summary>
        [JsonPropertyName("down_votes")]
        DownVotes,

        /// <summary>
        /// bounties_offered
        /// </summary>
        [JsonPropertyName("bounties_offered")]
        BountiesOffered,

        /// <summary>
        /// bounties_won
        /// </summary>
        [JsonPropertyName("bounties_won")]
        BountiesWon,

        /// <summary>
        /// spam
        /// </summary>
        [JsonPropertyName("spam")]
        Spam,

        /// <summary>
        /// suggested_edits
        /// </summary>
        [JsonPropertyName("suggested_edits")]
        SuggestedEdits
    }
}