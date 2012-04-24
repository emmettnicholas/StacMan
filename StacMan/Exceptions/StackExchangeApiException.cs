using System;

namespace StackExchange.StacMan.Exceptions
{
    public class StackExchangeApiException : Exception
    {
        public StackExchangeApiException(int errorId, string errorName, string errorMessage)
            : base(String.Format("{0} - {1}: {2}", errorName, errorId, errorMessage))
        {
            ErrorId = errorId;
            ErrorName = errorName;
            ErrorMessage = errorMessage;
        }

        public readonly int ErrorId;
        public readonly string ErrorName;
        public readonly string ErrorMessage;
    }
}
