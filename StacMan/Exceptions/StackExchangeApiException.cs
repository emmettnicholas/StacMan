using System;

namespace StackExchange.StacMan.Exceptions
{
    /// <summary>
    /// An error encountered during a Stack Exchange API v2 method call.
    /// http://api.stackexchange.com/docs/error-handling
    /// </summary>
    [Serializable]
    public class StackExchangeApiException : Exception
    {
        /// <summary>
        /// Create a new StackExchangeApiException
        /// </summary>
        public StackExchangeApiException(int errorId, string errorName, string errorMessage)
            : base(String.Format("{0} - {1}: {2}", errorName, errorId, errorMessage))
        {
            ErrorId = errorId;
            ErrorName = errorName;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// error_id
        /// </summary>
        public readonly int ErrorId;

        /// <summary>
        /// error_name
        /// </summary>
        public readonly string ErrorName;

        /// <summary>
        /// error_message
        /// </summary>
        public readonly string ErrorMessage;
    }
}
