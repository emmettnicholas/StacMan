using System.Text.Json.Serialization;

namespace StackExchange.StacMan.QuestionTimelines
{
    /// <summary>
    /// timeline_type
    /// </summary>
    public enum TimelineType
    {
        /// <summary>
        /// question
        /// </summary>
        [JsonPropertyName("question")]
        Question,

        /// <summary>
        /// answer
        /// </summary>
        [JsonPropertyName("answer")]
        Answer,

        /// <summary>
        /// comment
        /// </summary>
        [JsonPropertyName("comment")]
        Comment,

        /// <summary>
        /// unaccepted_answer
        /// </summary>
        [JsonPropertyName("unaccepted_answer")]
        UnacceptedAnswer,

        /// <summary>
        /// accepted_answer
        /// </summary>
        [JsonPropertyName("accepted_answer")]
        AcceptedAnswer,

        /// <summary>
        /// vote_aggregate
        /// </summary>
        [JsonPropertyName("vote_aggregate")]
        VoteAggregate,

        /// <summary>
        /// revision
        /// </summary>
        [JsonPropertyName("revision")]
        Revision,

        /// <summary>
        /// post_state_changed
        /// </summary>
        [JsonPropertyName("post_state_changed")]
        PostStateChanged
    }
}