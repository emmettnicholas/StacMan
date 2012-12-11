namespace StackExchange.StacMan.Questions
{
    /// <summary>
    /// sort
    /// </summary>
    public enum FavoriteSort
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
        /// when the user favorited the question
        /// </summary>
        [Sort(SortType.DateTime)]
        Added
    }
}
