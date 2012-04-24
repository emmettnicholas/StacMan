namespace StackExchange.StacMan.Questions
{
    public enum Sort
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
        Creation
    }
}
