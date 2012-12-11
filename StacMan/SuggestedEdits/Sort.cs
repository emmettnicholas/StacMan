namespace StackExchange.StacMan.SuggestedEdits
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
        /// approval_date (does not return unapproved suggested_edits)
        /// </summary>
        [Sort(SortType.DateTime)]
        Approval,

        /// <summary>
        /// rejection_date (does not return unrejected suggested_edits)
        /// </summary>
        [Sort(SortType.DateTime)]
        Rejection
    }
}
