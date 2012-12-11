namespace StackExchange.StacMan.Questions
{
    /// <summary>
    /// sort
    /// </summary>
    public enum RelatedSort
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
        /// a priority sort by site applies, subject to change at any time
        /// </summary>
        Rank
    }
}
