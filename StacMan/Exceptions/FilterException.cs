using System;

namespace StackExchange.StacMan.Exceptions
{
    public class FilterException : Exception
    {
        public FilterException(string message, params object[] args) : base(String.Format(message, args)) { }
    }
}
