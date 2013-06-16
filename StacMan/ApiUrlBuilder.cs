using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace StackExchange.StacMan
{
    internal class ApiUrlBuilder
    {
        public ApiUrlBuilder(string apiVersion, string relativeUrl, bool useHttps = false)
        {
            BaseUrl = String.Format("{0}://api.stackexchange.com/{1}{2}{3}", useHttps ? "https" : "http", apiVersion, relativeUrl.StartsWith("/") ? "" : "/", relativeUrl);
            QueryStringParameters = new NameValueCollection();
        }

        public readonly string BaseUrl;
        private readonly NameValueCollection QueryStringParameters;
        
        public void AddParameter(string name, object value)
        {
            if (value != null)
                QueryStringParameters.Add(name, value.ToString());
        }

        public void AddParameter(string name, DateTime? dt)
        {
            if (dt.HasValue)
                AddParameter(name, dt.Value.ToUnixTime());
        }

        public void AddParameter(string name, IEnumerable<string> values)
        {
            if (values != null && values.Any())
                AddParameter(name, String.Join(";", values));
        }

        public string QueryString()
        {
            return String.Join("&", QueryStringParameters.AllKeys.Select(key => String.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(QueryStringParameters[key]))));
        }

        public override string ToString()
        {
            var url = BaseUrl;

            if (QueryStringParameters.Count > 0)
                url += "?" + QueryString();

            return url;
        }
    }
}
