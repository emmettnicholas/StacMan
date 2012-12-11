namespace StackExchange.StacMan.TagSynonyms
{
    /// <summary>
    /// sort
    /// </summary>
    public enum Sort
    {
        /// <summary>
        /// creation_date
        /// </summary>
        [Sort(SortType.DateTime)]
        Creation,

        /// <summary>
        /// applied_count
        /// </summary>
        [Sort(SortType.Integer)]
        Applied,

        /// <summary>
        /// last_applied_date
        /// </summary>
        [Sort(SortType.DateTime)]
        Activity
    }
}
