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
        Question,

        /// <summary>
        /// answer
        /// </summary>
        Answer,

        /// <summary>
        /// comment
        /// </summary>
        Comment,

        /// <summary>
        /// unaccepted_answer
        /// </summary>
        UnacceptedAnswer,

        /// <summary>
        /// accepted_answer
        /// </summary>
        AcceptedAnswer,

        /// <summary>
        /// vote_aggregate
        /// </summary>
        VoteAggregate,

        /// <summary>
        /// revision
        /// </summary>
        Revision,

        /// <summary>
        /// post_state_changed
        /// </summary>
        PostStateChanged
    }
}