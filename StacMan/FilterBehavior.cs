namespace StackExchange.StacMan
{
    public enum FilterBehavior
    {
        /// <summary>
        /// [RECOMMENDED] All filters must be registered prior to being used, and getters for filtered-out properties throw FilterExceptions.
        /// </summary>
        Strict,

        /// <summary>
        /// Filters don't need to be registered, and getters for filtered-out properties just return default values.
        /// </summary>
        Loose
    }
}