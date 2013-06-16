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
        AskerAcceptsAnswer,

        /// <summary>
        /// askwer_unaccept_answer
        /// </summary>
        AskerUnacceptAnswer,

        /// <summary>
        /// answer_accepted
        /// </summary>
        AnswerAccepted,

        /// <summary>
        /// answer_unaccepted
        /// </summary>
        AnswerUnaccepted,

        /// <summary>
        /// voter_downvotes
        /// </summary>
        VoterDownvotes,

        /// <summary>
        /// voter_undownvotes
        /// </summary>
        VoterUndownvotes,

        /// <summary>
        /// post_downvoted
        /// </summary>
        PostDownvoted,

        /// <summary>
        /// post_undownvoted
        /// </summary>
        PostUndownvoted,

        /// <summary>
        /// post_upvoted
        /// </summary>
        PostUpvoted,

        /// <summary>
        /// post_unupvoted
        /// </summary>
        PostUnupvoted,

        /// <summary>
        /// suggested_edit_approval_received
        /// </summary>
        SuggestedEditApprovalReceived,

        /// <summary>
        /// post_flagged_as_spam
        /// </summary>
        PostFlaggedAsSpam,

        /// <summary>
        /// post_flagged_as_offensive
        /// </summary>
        PostFlaggedAsOffensive,

        /// <summary>
        /// bounty_given
        /// </summary>
        BountyGiven,

        /// <summary>
        /// bounty_earned
        /// </summary>
        BountyEarned,

        /// <summary>
        /// bounty_cancelled
        /// </summary>
        BountyCancelled,

        /// <summary>
        /// post_deleted
        /// </summary>
        PostDeleted,

        /// <summary>
        /// post_undeleted
        /// </summary>
        PostUndeleted,

        /// <summary>
        /// association_bonus
        /// </summary>
        AssociationBonus,

        /// <summary>
        /// arbitrary_reputation_change
        /// </summary>
        ArbitraryReputationChange,

        /// <summary>
        /// vote_fraud_reversal
        /// </summary>
        VoteFraudReversal,

        /// <summary>
        /// post_migrated
        /// </summary>
        PostMigrated,

        /// <summary>
        /// user_deleted
        /// </summary>
        UserDeleted
    }
}
