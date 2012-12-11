namespace StackExchange.StacMan.Questions
{
    /// <summary>
    /// sort
    /// </summary>
    public enum AllSort
    {
        /// <summary>
        /// last_activity_date
        /// </summary>
        [Sort(SortType.DateTime)]
        Activity,

        /// <summary>
        /// score
        /// </summary>
        [Sort(SortType.Integer)]
        Votes,

        /// <summary>
        /// creation_date
        /// </summary>
        [Sort(SortType.DateTime)]
        Creation,

        /// <summary>
        /// by the formula ordering the hot tab
        /// </summary>
        Hot,

        /// <summary>
        /// by the formula ordering the week tab
        /// </summary>
        Week,

        /// <summary>
        /// by the formula ordering the month tab
        /// </summary>
        Month
    }
}
