namespace StackExchange.StacMan
{
    public enum FilterBehavior
    {
        /// <summary>
        /// Getting a property not included in the filter throws a FilterException.
        /// <para>All filters must be "registered" (with the RegisterFilters method) prior to being used.</para>
        /// <remarks>IMPORTANT: RegisterFilters incurs one API call (per 20 unregistered filters) each time it's called, so for best performance,
        /// it should be called sparingly and at most once per filter, e.g. once only when your app starts.</remarks>
        /// </summary>
        Strict,

        /// <summary>
        /// Getting a property not included in the filter returns that property type's default value.
        /// <para>Filters do not need to be "registered".</para>
        /// </summary>
        Loose
    }
}