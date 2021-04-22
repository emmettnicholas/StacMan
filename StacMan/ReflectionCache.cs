using System.Collections.Generic;
using System.Linq;

namespace StackExchange.StacMan
{
    internal static class ReflectionCache
    {
        public static class SortsBySortType<TSort> where TSort : struct // proxy for "where TSort: enum"
        {
            static SortsBySortType()
            {
                Value = typeof(TSort).GetFields()
                    .Where(fi => fi.GetCustomAttributes(typeof(SortAttribute), false).Length == 1)
                    .GroupBy(fi => ((SortAttribute)fi.GetCustomAttributes(typeof(SortAttribute), false)[0]).SortType)
                    .ToDictionary(
                        grp => grp.Key,
                        grp => grp.Select(fi => (TSort)fi.GetValue(null)).ToList());
            }

            public static readonly IDictionary<SortType, List<TSort>> Value;
        }
    }
}
