using System.Text.Json.Serialization;

namespace StackExchange.StacMan.ReputationHistories
{
    /// <summary>
    /// reputation_history_type
    /// </summary>
    public enum ReputationHistoryType
    {
        /// <summary>
        /// asker_accepts_answer
        /// </summary>
        [JsonPropertyName("asker_accepts_answer")]
        AskerAcceptsAnswer,

        /// <summary>
        /// askwer_unaccept_answer
        /// </summary>
        [JsonPropertyName("asker_unaccept_answer")]
        AskerUnacceptAnswer,

        /// <summary>
        /// answer_accepted
        /// </summary>
        [JsonPropertyName("answer_accepted")]
        AnswerAccepted,

        /// <summary>
        /// answer_unaccepted
        /// </summary>
        [JsonPropertyName("answer_unaccepted")]
        AnswerUnaccepted,

        /// <summary>
        /// voter_downvotes
        /// </summary>
        [JsonPropertyName("voter_downvotes")]
        VoterDownvotes,

        /// <summary>
        /// voter_undownvotes
        /// </summary>
        [JsonPropertyName("voter_undownvotes")]
        VoterUndownvotes,

        /// <summary>
        /// post_downvoted
        /// </summary>
        [JsonPropertyName("post_downvoted")]
        PostDownvoted,

        /// <summary>
        /// post_undownvoted
        /// </summary>
        [JsonPropertyName("post_undownvoted")]
        PostUndownvoted,

        /// <summary>
        /// post_upvoted
        /// </summary>
        [JsonPropertyName("post_upvoted")]
        PostUpvoted,

        /// <summary>
        /// post_unupvoted
        /// </summary>
        [JsonPropertyName("post_unupvoted")]
        PostUnupvoted,

        /// <summary>
        /// suggested_edit_approval_received
        /// </summary>
        [JsonPropertyName("suggested_edit_approval_received")]
        SuggestedEditApprovalReceived,

        /// <summary>
        /// post_flagged_as_spam
        /// </summary>
        [JsonPropertyName("post_flagged_as_spam")]
        PostFlaggedAsSpam,

        /// <summary>
        /// post_flagged_as_offensive
        /// </summary>
        [JsonPropertyName("post_flagged_as_offensive")]
        PostFlaggedAsOffensive,

        /// <summary>
        /// bounty_given
        /// </summary>
        [JsonPropertyName("bounty_given")]
        BountyGiven,

        /// <summary>
        /// bounty_earned
        /// </summary>
        [JsonPropertyName("bounty_earned")]
        BountyEarned,

        /// <summary>
        /// bounty_cancelled
        /// </summary>
        [JsonPropertyName("bounty_cancelled")]
        BountyCancelled,

        /// <summary>
        /// post_deleted
        /// </summary>
        [JsonPropertyName("post_deleted")]
        PostDeleted,

        /// <summary>
        /// post_undeleted
        /// </summary>
        [JsonPropertyName("post_undeleted")]
        PostUndeleted,

        /// <summary>
        /// association_bonus
        /// </summary>
        [JsonPropertyName("association_bonus")]
        AssociationBonus,

        /// <summary>
        /// arbitrary_reputation_change
        /// </summary>
        [JsonPropertyName("arbitrary_reputation_change")]
        ArbitraryReputationChange,

        /// <summary>
        /// vote_fraud_reversal
        /// </summary>
        [JsonPropertyName("vote_fraud_reversal")]
        VoteFraudReversal,

        /// <summary>
        /// post_migrated
        /// </summary>
        [JsonPropertyName("post_migrated")]
        PostMigrated,

        /// <summary>
        /// user_deleted
        /// </summary>
        [JsonPropertyName("user_deleted")]
        UserDeleted
    }
}
