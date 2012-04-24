namespace StackExchange.StacMan.Comments
{
    public enum Sort
    {
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
