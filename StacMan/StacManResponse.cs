using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackExchange.StacMan
{
    /// <summary>
    /// StacMan API response
    /// </summary>
    public class StacManResponse<T> where T : StacManType
    {
        /// <summary>
        /// True if the API call was successful.
        /// </summary>
        public bool Success { get; internal set; }

        /// <summary>
        /// Data returned by the Stack Exchange API method.
        /// </summary>
        public Wrapper<T> Data { get; internal set; }
        
        /// <summary>
        /// Non-null whenever Success is false.
        /// </summary>
        public Exception Error { get; internal set; }
        
        /// <summary>
        /// True if the underlying Stack Exchange API method responded, regardless of success.
        /// </summary>
        public bool ReceivedApiResponse
        {
            get { return Data != null; }
        }

        #region Properties for debugging

        /// <summary>
        /// The url of the underlying Stack Exchange API method.
        /// Useful for debugging.
        /// </summary>
        public string ApiUrl { get; internal set; }

        /// <summary>
        /// Response of the request made to the underlying Stack Exchange API method.
        /// Useful for debugging.
        /// </summary>
        public string RawData { get; internal set; }

        #endregion
    }
}
