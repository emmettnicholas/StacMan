namespace StackExchange.StacMan.Badges
{
    /// <summary>
    /// sort
    /// </summary>
    public enum Sort
    {
        /// <summary>
        /// rank
        /// </summary>
        [Sort(SortType.BadgeRank)]
        Rank,

        /// <summary>
        /// name
        /// </summary>
        [Sort(SortType.String)]
        Name
    }
}
