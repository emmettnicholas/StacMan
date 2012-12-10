using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StackExchange.StacMan
{
    internal static class ReflectionCache
    {
        public static readonly MethodInfo StacManClientParseApiResponse = typeof(StacManClient).GetMethod("ParseApiResponse", BindingFlags.NonPublic | BindingFlags.Instance);

        public static class ApiFieldsByName<T> where T : StacManType
        {
            static ApiFieldsByName()
            {
                Value = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(pi => pi.GetCustomAttributes(typeof(FieldAttribute), false).Length == 1)
                    .ToDictionary(
                        pi => ((FieldAttribute)pi.GetCustomAttributes(typeof(FieldAttribute), false)[0]).FieldName,
                        pi => pi);
            }

            public static readonly IDictionary<string, PropertyInfo> Value;
        }

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
