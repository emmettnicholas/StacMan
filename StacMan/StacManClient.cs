using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

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

        private readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            Converters =
            {
                new JsonStringEnumMemberConverter(namingPolicy: null, allowIntegerValues: false),
                new UnixEpochDateConverter(),
            }
        };

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
                        response.Data = JsonSerializer.Deserialize<Wrapper<T>>(rawData, JsonSerializerOptions);
                        if (response.Data is null)
                        {
                            throw new JsonException($"Failed to deserialize response from {ub} as {typeof(Wrapper<T>).Name}.");
                        }
                        var backoff = response.Data.Backoff;
                        if (backoff.HasValue)
                        {
                            lock (BackoffUntil)
                            {
                                BackoffUntil[backoffKey] = DateTime.Now.AddSeconds(backoff.Value);
                            }
                        }

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

            if (httpMethod == HttpMethod.POST)
            {
                response.ApiUrl = ub.BaseUrl;
                FetchApiResponseWithPOST(response.ApiUrl, ub.QueryString(), successCallback, errorCallback);
            }
            else
            {
                response.ApiUrl = ub.ToString();
                FetchApiResponseWithGET(response.ApiUrl, successCallback, errorCallback);
            }
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
            request.UserAgent = "StacMan";

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
            request.UserAgent = "StacMan";
            
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


    }
}
