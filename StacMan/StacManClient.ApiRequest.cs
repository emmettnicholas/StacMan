using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackExchange.StacMan
{
    public partial class StacManClient
    {
        private enum HttpMethod
        {
            GET,
            POST
        }

        private interface IApiRequest
        {
            void GetResponse(Action callback);
            string BackoffKey { get; }
        }

        private class ApiRequest<T> : IApiRequest where T : StacManType
        {
            public ApiRequest(StacManClient client, ApiUrlBuilder ub, HttpMethod httpMethod, string backoffKey)
            {
                Client = client;
                UrlBuilder = ub;
                HttpMethod = httpMethod;
                BackoffKey = backoffKey;
            }

            private readonly StacManClient Client;
            private readonly ApiUrlBuilder UrlBuilder;
            private readonly HttpMethod HttpMethod;

            private readonly TaskCompletionSource<StacManResponse<T>> Tcs = new TaskCompletionSource<StacManResponse<T>>();

            public Task<StacManResponse<T>> Task
            {
                get { return Tcs.Task; }
            }

            public void GetResponse(Action callback)
            {
                try
                {
                    Client.GetApiResponse<T>(UrlBuilder, HttpMethod, BackoffKey, response =>
                        {
                            try
                            {
                                callback();
                                Tcs.SetResult(response);
                            }
                            catch (Exception ex)
                            {
                                Tcs.SetException(ex);
                            }
                        });
                }
                catch (Exception ex)
                {
                    callback();
                    Tcs.SetException(ex);
                }
            }

            public string BackoffKey { get; private set; }
        }
    }
}
