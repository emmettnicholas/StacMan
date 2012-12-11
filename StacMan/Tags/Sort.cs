namespace StackExchange.StacMan.Tags
{
    /// <summary>
    /// sort
    /// </summary>
    public enum Sort
    {
        /// <summary>
        /// count
        /// </summary>
        [Sort(SortType.Integer)]
        Popular,

        /// <summary>
        /// the creation_date of the last question asked with the tag
        /// </summary>
        [Sort(SortType.DateTime)]
        Activity,

        /// <summary>
        /// name
        /// </summary>
        [Sort(SortType.String)]
        Name
    }
}
