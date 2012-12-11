namespace StackExchange.StacMan.Questions
{
    /// <summary>
    /// sort
    /// </summary>
    public enum SearchSort
    {
        /// <summary>
        /// last_activity_date
        /// </summary>
        [Sort(SortType.DateTime)]
        Activity,

        /// <summary>
        /// creation_date
        /// </summary>
        [Sort(SortType.DateTime)]
        Creation,

        /// <summary>
        /// score
        /// </summary>
        [Sort(SortType.Integer)]
        Votes,

        /// <summary>
        /// matches the relevance tab on the site itself
        /// </summary>
        Relevance
    }
}
