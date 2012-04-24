using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackExchange.StacMan
{
    public class StacManResponse<T> where T : StacManType
    {
        public bool Success { get; internal set; }

        public Wrapper<T> Data { get; internal set; }
        
        public Exception Error { get; internal set; }
        
        public bool ReceivedApiResponse
        {
            get { return Data != null; }
        }

        #region Properties for debugging
        public string ApiUrl { get; internal set; }
        public string RawData { get; internal set; }
        #endregion
    }
}
