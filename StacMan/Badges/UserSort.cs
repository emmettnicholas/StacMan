namespace StackExchange.StacMan.Badges
{
    /// <summary>
    /// sort
    /// </summary>
    public enum UserSort
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
        Type,

        /// <summary>
        /// badge award date
        /// </summary>
        [Sort(SortType.DateTime)]
        Awarded
    }
}
