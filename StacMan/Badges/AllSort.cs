namespace StackExchange.StacMan.Badges
{
    public enum AllSort
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
        Name,

        /// <summary>
        /// badge_type
        /// </summary>
        [Sort(SortType.BadgeType)]
        Type
    }
}
