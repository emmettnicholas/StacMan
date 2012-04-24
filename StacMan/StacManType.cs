using System;
using System.Linq;

namespace StackExchange.StacMan
{
    public abstract class StacManType
    {
        protected StacManType(FilterBehavior filterBehavior, Filter filter, string fieldPrefix)
        {
            FilterBehavior = filterBehavior;
            Filter = filter;
            FieldPrefix = fieldPrefix;
        }

        private readonly FilterBehavior FilterBehavior;
        private readonly Filter Filter;
        private readonly string FieldPrefix;

        protected void EnsureFilterContainsField(string fieldName)
        {
            if (FilterBehavior == FilterBehavior.Strict && Filter != null)
            {
                var field = String.Format("{0}.{1}", FieldPrefix, fieldName);

                // "error_*" fields are never excluded in the error case
                if (!Filter.IncludedFields.Contains(field) && !field.StartsWith(".error_"))
                    throw new Exceptions.FilterException("\"{0}\" filter doesn't contain \"{1}\" field", Filter.Filter, field);
            }
        }
    }
}
