using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackExchange.StacMan
{
    internal enum SortType
    {
        DateTime,
        Integer,
        String,
        BadgeRank,
        BadgeType
    }

    [AttributeUsage(AttributeTargets.Field)]
    internal class SortAttribute : Attribute
    {
        public SortAttribute(SortType sortType)
        {
            SortType = sortType;
        }

        public readonly SortType SortType;
    }
}
