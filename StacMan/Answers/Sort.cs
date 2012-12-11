namespace StackExchange.StacMan.Answers
{
    /// <summary>
    /// sort
    /// </summary>
    public enum Sort
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
        Votes
    }
}
