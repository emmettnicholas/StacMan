using System;
using System.Collections.Generic;
using System.Linq;

namespace StackExchange.StacMan
{
    public partial class StacManClient
    {
        private void ValidateMinApiVersion(string minApiVersion)
        {
            double clientApiVersion;
            double methodApiVersion;

            if (Double.TryParse(Version, out clientApiVersion) && Double.TryParse(minApiVersion, out methodApiVersion) && methodApiVersion > clientApiVersion)
                throw new InvalidOperationException("method introduced in API version " + minApiVersion);
        }

        private void ValidatePaging(int? page, int? pagesize)
        {
            if (page.HasValue && page.Value < 1)
                throw new ArgumentException("page must be positive", "page");

            if (pagesize.HasValue && pagesize.Value < 0)
                throw new ArgumentException("pagesize cannot be negative", "pagesize");
        }

        private void ValidateString(string value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);

            if (value == "")
                throw new ArgumentException(paramName + " cannot be empty", paramName);
        }

        private void ValidateEnumerable<T>(IEnumerable<T> values, string paramName)
        {
            if (values == null)
                throw new ArgumentNullException(paramName);

            if (!values.Any())
                throw new ArgumentException(paramName + " cannot be empty", paramName);
        }

        private void ValidateSortMinMax<TSort>(TSort? sort,
            int? min = null, int? max = null,
            DateTime? mindate = null, DateTime? maxdate = null,
            string minname = null, string maxname = null,
            Badges.Rank? minrank = null, Badges.Rank? maxrank = null,
            Badges.BadgeType? mintype = null, Badges.BadgeType? maxtype = null)
            where TSort : struct // proxy for "where TSort: enum"
        {
            if (!typeof(TSort).IsEnum)
                throw new ArgumentException("sort must be a nullable enumeration", "sort");

            var sortsBySortType = ReflectionCache.SortsBySortType<TSort>.Value;
            var sortActual = sort ?? default(TSort);

            if (!sortsBySortType.ContainsKey(SortType.DateTime) || !sortsBySortType[SortType.DateTime].Contains(sortActual))
            {
                if (mindate.HasValue)
                    throw new ArgumentException("mindate must be null when sort=" + sortActual.ToString().ToLower(), "mindate");
                if (maxdate.HasValue)
                    throw new ArgumentException("maxdate must be null when sort=" + sortActual.ToString().ToLower(), "maxdate");
            }

            if (!sortsBySortType.ContainsKey(SortType.Integer) || !sortsBySortType[SortType.Integer].Contains(sortActual))
            {
                if (min.HasValue)
                    throw new ArgumentException("min must be null when sort=" + sortActual.ToString().ToLower(), "min");
                if (max.HasValue)
                    throw new ArgumentException("min must be null when sort=" + sortActual.ToString().ToLower(), "max");
            }

            if (!sortsBySortType.ContainsKey(SortType.String) || !sortsBySortType[SortType.String].Contains(sortActual))
            {
                if (minname != null)
                    throw new ArgumentException("minname must be null when sort=" + sortActual.ToString().ToLower(), "minname");
                if (maxname != null)
                    throw new ArgumentException("maxname must be null when sort=" + sortActual.ToString().ToLower(), "maxname");
            }

            if (!sortsBySortType.ContainsKey(SortType.BadgeRank) || !sortsBySortType[SortType.BadgeRank].Contains(sortActual))
            {
                if (minrank.HasValue)
                    throw new ArgumentException("minrank must be null when sort=" + sortActual.ToString().ToLower(), "minrank");
                if (maxrank.HasValue)
                    throw new ArgumentException("maxrank must be null when sort=" + sortActual.ToString().ToLower(), "maxrank");
            }

            if (!sortsBySortType.ContainsKey(SortType.BadgeType) || !sortsBySortType[SortType.BadgeType].Contains(sortActual))
            {
                if (mintype.HasValue)
                    throw new ArgumentException("mintype must be null when sort=" + sortActual.ToString().ToLower(), "mintype");
                if (maxtype.HasValue)
                    throw new ArgumentException("maxtype must be null when sort=" + sortActual.ToString().ToLower(), "maxtype");
            }
        }
    }
}
