namespace StackExchange.StacMan.Users
{
    /// <summary>
    /// sort
    /// </summary>
    public enum Sort
    {
        /// <summary>
        /// reputation
        /// </summary>
        [Sort(SortType.Integer)]
        Reputation,

        /// <summary>
        /// creation_date
        /// </summary>
        [Sort(SortType.DateTime)]
        Creation,

        /// <summary>
        /// display_name
        /// </summary>
        [Sort(SortType.String)]
        Name,

        /// <summary>
        /// last_modified_date
        /// </summary>
        [Sort(SortType.DateTime)]
        Modified
    }
}