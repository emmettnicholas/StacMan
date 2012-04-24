using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using ServiceStack.Text;

namespace StackExchange.StacMan
{
    public partial class StacManClient
    {
        public StacManClient(FilterBehavior filterBehavior, string key = null)
        {   
            FilterBehavior = filterBehavior;
            Key = key;
            ApiTimeoutMs = 5000;
            MaxSimultaneousRequests = 10;
        }

        private readonly FilterBehavior FilterBehavior;
        private readonly string Key;

        private readonly IDictionary<string, Filter> RegisteredFilters = new Dictionary<string, Filter> { { Filter.Default.FilterName, Filter.Default } };

        private int ActiveRequests = 0;
        private readonly Queue<IApiRequest> QueuedRequests = new Queue<IApiRequest>();

        /// <summary>
        /// Gets or sets the number of milliseconds to wait before a request to the Stack Exchange API times out. Default is 5000.
        /// </summary>
        public int ApiTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of simultaneous requests to the Stack Exchange API that can be active at any given time.
        /// Additional requests are queued up. Default is 10.
        /// </summary>
        public int MaxSimultaneousRequests { get; set; }

        public bool IsFilterRegistered(string filter)
        {
            return RegisteredFilters.ContainsKey(filter);
        }

        // TODO async version of RegisterFilters?
        public void RegisterFilters(params string[] filters)
        {
            if (FilterBehavior != FilterBehavior.Strict)
                throw new Exceptions.FilterException("Filters can only be registered when FilterBehavior=Strict");

            var tasks = new List<Task<StacManResponse<Filter>>>(filters.Length);
            const int batchSize = 20; // the "/filters/{filters}" API method takes max 20 filters
            
            while (filters.Any())
            {
                var batch = filters.Take(batchSize);
                tasks.Add(Filters.Read(batch));
                filters = filters.Skip(batchSize).ToArray();
            }

            Task.WaitAll(tasks.ToArray());

            var items = tasks.Where(t => t.Result.Success).SelectMany(t => t.Result.Data.Items);

            foreach (var filter in items.Where(f => f.FilterType != StacMan.Filters.FilterType.Invalid))
            {
                if (!IsFilterRegistered(filter.FilterName))
                    RegisteredFilters.Add(filter.FilterName, filter);
            }

            if (tasks.Any(t => !t.Result.Success))
                throw tasks.First(t => !t.Result.Success).Result.Error;

            if (items.Any(f => f.FilterType == StacMan.Filters.FilterType.Invalid))
                throw new Exceptions.FilterException("Failed to register invalid filters: {0}", String.Join(", ", items.Where(f => f.FilterType == StacMan.Filters.FilterType.Invalid).Select(f => f.FilterName)));
        }

        private Task<StacManResponse<T>> CreateApiTask<T>(ApiUrlBuilder ub, Filter filter) where T : StacManType
        {
            var request = new ApiRequest<T>(this, ub, filter);

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

            request.GetResponse(() =>
            {
                Interlocked.Decrement(ref ActiveRequests);
                ProcessQueue();
            });
        }

        private void GetApiResponse<T>(ApiUrlBuilder ub, Filter filter, Action<StacManResponse<T>> callback) where T : StacManType
        {
            var response = new StacManResponse<T>();

            ub.AddParameter("key", Key);

            response.ApiUrl = ub.ToString();

            FetchApiResponse(response.ApiUrl,
                rawData =>
                {
                    try
                    {
                        response.RawData = rawData;
                        response.Data = ParseApiResponse<Wrapper<T>>(JsonObject.Parse(response.RawData), filter);

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
                },
                ex =>
                {
                    response.Success = false;
                    response.Error = ex;
                    callback(response);
                });
        }

        // this is "internal protected virtual" so it can be mocked in unit tests
        internal protected virtual void FetchApiResponse(string url, Action<string> success, Action<Exception> error)
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
                    catch (Exception ex)
                    {
                        error(ex);
                    }
                },
                request);
        }

        private T ParseApiResponse<T>(JsonObject jsonObject, Filter filter) where T : StacManType
        {
            var ret = (T)Activator.CreateInstance(typeof(T), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { FilterBehavior, filter }, null);
            var apiFieldsByName = ReflectionCache.ApiFieldsByName<T>.Value;

            foreach (var fieldName in jsonObject.Keys)
            {
                if (!apiFieldsByName.ContainsKey(fieldName))
                    throw new Exception(String.Format("\"{0}\" field is unrecognized", fieldName));

                var property = apiFieldsByName[fieldName];
                object value;

                if (property.PropertyType == typeof(DateTime))
                {
                    value = jsonObject.Get<long>(fieldName).ToDateTime();
                }
                else if (Nullable.GetUnderlyingType(property.PropertyType) == typeof(DateTime))
                {
                    value = jsonObject.Get<long?>(fieldName).ToNullableDateTime();
                }
                else if (property.PropertyType.IsEnum)
                {
                    value = Enum.Parse(property.PropertyType, jsonObject.Get(fieldName).Replace("_", String.Empty), true);
                }
                else if (property.PropertyType.BaseType == typeof(StacManType))
                {
                    value = ReflectionCache.StacManClientParseApiResponse
                        .MakeGenericMethod(property.PropertyType)
                        .Invoke(this, new object[] { jsonObject.Object(fieldName), filter });
                }
                else if (property.PropertyType.IsArray && property.PropertyType.GetElementType().BaseType == typeof(StacManType))
                {
                    var elementType = property.PropertyType.GetElementType();

                    var objArr = jsonObject
                        .ArrayObjects(fieldName)
                        .ConvertAll(i => ReflectionCache.StacManClientParseApiResponse
                            .MakeGenericMethod(elementType)
                            .Invoke(this, new object[] { i, filter }))
                        .ToArray();

                    value = Array.CreateInstance(elementType, objArr.Length);
                    Array.Copy(objArr, (Array)value, objArr.Length);
                }
                else
                {
                    value = ReflectionCache.JsonObjectGet
                        .MakeGenericMethod(property.PropertyType)
                        .Invoke(null, new object[] { jsonObject, fieldName });
                }

                property.SetValue(ret, value, null);
            }

            return ret;
        }
    }
}
