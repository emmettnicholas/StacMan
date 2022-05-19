using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StackExchange.StacMan
{
    /// <summary>
    /// Client for Stack Exchange API v2
    /// </summary>
    public partial class StacManClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StacManClient"/> class.
        /// </summary>
        /// <param name="key">Your app's Stack Exchange API V2 key (optional)</param>
        /// <param name="version">Stack Exchange API version, e.g. "2.0" or "2.1"</param>
        public StacManClient(string key = null, string version = "2.0")
        {
            Key = key;
            Version = version;
            ApiTimeoutMs = 5000;
            MaxSimultaneousRequests = 10;
            RespectBackoffs = true;
        }

        /// <summary>
        /// Provide a custom url inspector and manipulator to be applied to all requests
        /// </summary>
        public void SetUrlManager(Func<string, string> urlManager)
        {
            this.urlManager = urlManager;
        }
        private Func<string, string> urlManager;

        private readonly string Key;
        private readonly string Version;

        private readonly IDictionary<string, DateTime> BackoffUntil = new Dictionary<string, DateTime>();

        private int ActiveRequests = 0;
        private readonly Queue<IApiRequest> QueuedRequests = new Queue<IApiRequest>();

        /// <summary>
        /// Gets or sets the number of milliseconds to wait before a request to the Stack Exchange API times out.
        /// <para>Default is 5000.</para>
        /// </summary>
        public int ApiTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of simultaneous requests to the Stack Exchange API that can be active at any given time.
        /// Additional requests are queued up.
        /// <para>Default is 10.</para>
        /// </summary>
        public int MaxSimultaneousRequests { get; set; }

        /// <summary>
        /// Gets or sets whether StacMan should automatically abide by the "backoff" returned by the API (http://api.stackexchange.com/docs/throttle).
        /// <para>Default is true.</para>
        /// </summary>
        public bool RespectBackoffs { get; set; }
        
        private Task<StacManResponse<T>> CreateApiTask<T>(ApiUrlBuilder ub, HttpMethod httpMethod, string backoffKey) where T : StacManType
        {
            var request = new ApiRequest<T>(this, ub, httpMethod, backoffKey);

            if (ActiveRequests >= MaxSimultaneousRequests || QueuedRequests.Count > 0)
            {
                lock (QueuedRequests)
                {
                    QueuedRequests.Enqueue(request);
                }
                ProcessQueue();
            }
            else
            {
                SendRequest(request);
            }

            return request.Task;
        }

        private void ProcessQueue()
        {
            if (ActiveRequests < MaxSimultaneousRequests && QueuedRequests.Count > 0)
            {
                lock (QueuedRequests)
                {
                    while (ActiveRequests < MaxSimultaneousRequests && QueuedRequests.Count > 0)
                    {
                        SendRequest(QueuedRequests.Dequeue());
                    }
                }
            }
        }

        private void SendRequest(IApiRequest request)
        {
            Interlocked.Increment(ref ActiveRequests);

            Action send = () =>
            {
                request.GetResponse(() =>
                {
                    Interlocked.Decrement(ref ActiveRequests);
                    ProcessQueue();
                });
            };

            if (RespectBackoffs && BackoffUntil.ContainsKey(request.BackoffKey))
            {
                lock (BackoffUntil)
                {
                    if (BackoffUntil.ContainsKey(request.BackoffKey))
                    {
                        var until = BackoffUntil[request.BackoffKey];
                        var seconds = (until - DateTime.Now).TotalSeconds;

                        if (seconds > 0)
                        {
                            var timer = new System.Timers.Timer(seconds * 1000);

                            timer.AutoReset = false;
                            timer.Elapsed += (sender, e) => send();
                            timer.Start();
                        }
                        else
                        {
                            BackoffUntil.Remove(request.BackoffKey);
                            send();
                        }
                    }
                    else
                    {
                        send();
                    }
                }
            }
            else
            {
                send();
            }
        }

        private void GetApiResponse<T>(ApiUrlBuilder ub, HttpMethod httpMethod, string backoffKey, Action<StacManResponse<T>> callback) where T : StacManType
        {
            var response = new StacManResponse<T>();

            ub.AddParameter("key", Key);

            Action<string> successCallback = rawData =>
                {
                    try
                    {
                        response.RawData = rawData;
                        response.Data = ParseApiResponse<Wrapper<T>>(JsonConvert.DeserializeObject<Dictionary<string, object>>(response.RawData), backoffKey);

                        if (response.Data.ErrorId.HasValue)
                            throw new Exceptions.StackExchangeApiException(response.Data.ErrorId.Value, response.Data.ErrorName, response.Data.ErrorMessage);

                        response.Success = true;
                    }
                    catch (Exception ex)
                    {
                        response.Success = false;
                        response.Error = ex;
                    }

                    callback(response);
                };

            Action<Exception> errorCallback = ex =>
                {
                    response.Success = false;
                    response.Error = ex;
                    callback(response);
                };


            var urlManager = this.urlManager; // snapshot
            if (httpMethod == HttpMethod.POST)
            {
                var url = ub.BaseUrl;
                if (urlManager != null) url = urlManager(url);
                response.ApiUrl = url;
                FetchApiResponseWithPOST(url, ub.QueryString(), successCallback, errorCallback);
            }
            else
            {
                var url = ub.ToString();
                if (urlManager != null) url = urlManager(url);
                response.ApiUrl = url;
                FetchApiResponseWithGET(url, successCallback, errorCallback);
            }
        }

        /// <summary>
        /// Gets or sets the user-agent to use from this client (if not set, StacMan is used)
        /// </summary>
        public string UserAgent { get; set; }

        private string GetUserAgent()
        {
            var ua = UserAgent;
            return string.IsNullOrWhiteSpace(ua) ? "StacMan" : ua;
        }

        /// <summary>
        /// this is "internal protected virtual" so it can be mocked in unit tests
        /// </summary>
        internal protected virtual void FetchApiResponseWithGET(string url, Action<string> success, Action<Exception> error)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = ApiTimeoutMs;
            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            request.UserAgent = GetUserAgent();

            request.BeginGetResponse(
                asyncResult =>
                {
                    try
                    {
                        using (var response = ((HttpWebRequest)asyncResult.AsyncState).EndGetResponse(asyncResult))
                        using (var stream = response.GetResponseStream())
                        using (var reader = new StreamReader(stream))
                        {
                            success(reader.ReadToEnd());
                        }
                    }
                    catch (WebException webex)
                    {
                        if (webex.Status == WebExceptionStatus.ProtocolError)
                        {
                            var response = webex.Response as HttpWebResponse;
                            if (response != null && response.StatusCode == HttpStatusCode.BadRequest)
                            {
                                using (var stream = response.GetResponseStream())
                                using (var reader = new StreamReader(stream))
                                {
                                    success(reader.ReadToEnd());
                                    return;
                                }
                            }
                        }

                        error(webex);
                    }
                    catch (Exception ex)
                    {
                        error(ex);
                    }
                },
                request);
        }

        /// <summary>
        /// this is "internal protected virtual" so it can be mocked in unit tests
        /// </summary>
        internal protected virtual void FetchApiResponseWithPOST(string url, string data, Action<string> success, Action<Exception> error)
        {
            var postData = System.Text.Encoding.UTF8.GetBytes(data);

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = ApiTimeoutMs;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            request.UserAgent = GetUserAgent();
            
            request.BeginGetRequestStream(
                asyncResult =>
                {
                    try
                    {
                        var req = (HttpWebRequest)asyncResult.AsyncState;

                        using (var requestStream = req.EndGetRequestStream(asyncResult))
                        {
                            requestStream.Write(postData, 0, postData.Length);
                        }

                        req.BeginGetResponse(
                            asyncResult2 =>
                            {
                                try
                                {
                                    var req2 = (HttpWebRequest)asyncResult2.AsyncState;

                                    using (var response = (HttpWebResponse)req2.EndGetResponse(asyncResult2))
                                    using (var stream = response.GetResponseStream())
                                    using (var reader = new StreamReader(stream))
                                    {
                                        success(reader.ReadToEnd());
                                    }
                                }
                                catch (WebException webex)
                                {
                                    if (webex.Status == WebExceptionStatus.ProtocolError)
                                    {
                                        var response = webex.Response as HttpWebResponse;
                                        if (response != null && response.StatusCode == HttpStatusCode.BadRequest)
                                        {
                                            using (var stream = response.GetResponseStream())
                                            using (var reader = new StreamReader(stream))
                                            {
                                                success(reader.ReadToEnd());
                                                return;
                                            }
                                        }
                                    }

                                    error(webex);
                                }
                                catch (Exception ex)
                                {
                                    error(ex);
                                }
                            },
                            req);
                    }
                    catch (Exception ex)
                    {
                        error(ex);
                    }
                },
                request);
        }

        private T ParseApiResponse<T>(Dictionary<string, object> jsonObject, string backoffKey) where T : StacManType
        {
            var ret = (T)Activator.CreateInstance(typeof(T));
            var apiFieldsByName = ReflectionCache.ApiFieldsByName<T>.Value;

            foreach (var fieldName in jsonObject.Keys)
            {
                if (!apiFieldsByName.ContainsKey(fieldName))
                    throw new Exception(String.Format("\"{0}\" field is unrecognized", fieldName));

                var property = apiFieldsByName[fieldName];
                object value;

                if (property.PropertyType == typeof(DateTime) || Nullable.GetUnderlyingType(property.PropertyType) == typeof(DateTime))
                {
                    value = Convert.ToInt64(jsonObject[fieldName]).ToDateTime();
                }
                else if (property.PropertyType == typeof(Guid) || Nullable.GetUnderlyingType(property.PropertyType) == typeof(Guid))
                {
                    value = Guid.Parse((string)jsonObject[fieldName]);
                }
                else if (property.PropertyType.IsEnum)
                {
                    value = Enum.Parse(property.PropertyType, ((string)jsonObject[fieldName]).Replace("_", String.Empty), true);
                }
                else if (property.PropertyType.BaseType == typeof(StacManType))
                {
                    value = ReflectionCache.StacManClientParseApiResponse
                        .MakeGenericMethod(property.PropertyType)
                        .Invoke(this, new object[] { ((JObject)jsonObject[fieldName]).ToObject<Dictionary<string, object>>(), backoffKey });
                }
                else if (property.PropertyType.IsArray)
                {
                    var elementType = property.PropertyType.GetElementType();

                    Func<object, object> selector;
                    if (elementType.BaseType == typeof(StacManType))
                        selector = o => ReflectionCache.StacManClientParseApiResponse.MakeGenericMethod(elementType).Invoke(this, new object[] { o, backoffKey });
                    else
                        selector = o => Convert.ChangeType(o, elementType.BaseType);

                    var objArr = ((JArray)jsonObject[fieldName]).Select(o=> o.ToObject<Dictionary<string, object>>()).Cast<object>().Select(selector).ToArray();

                    value = Array.CreateInstance(elementType, objArr.Length);
                    Array.Copy(objArr, (Array)value, objArr.Length);
                }
                else
                {
                    value = Convert.ChangeType(jsonObject[fieldName], Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                }

                if (fieldName == "backoff")
                {
                    lock (BackoffUntil)
                    {
                        BackoffUntil[backoffKey] = DateTime.Now.AddSeconds((int)value);
                    }
                }

                property.SetValue(ret, value, null);
            }

            return ret;
        }
    }
}
