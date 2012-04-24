using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackExchange.StacMan
{
    public partial class StacManClient
    {
        private interface IApiRequest
        {
            void GetResponse(Action callback);
        }

        private class ApiRequest<T> : IApiRequest where T : StacManType
        {
            public ApiRequest(StacManClient client, ApiUrlBuilder ub, Filter filter)
            {
                Client = client;
                UrlBuilder = ub;
                Filter = filter;
            }

            private readonly StacManClient Client;
            private readonly ApiUrlBuilder UrlBuilder;
            private readonly Filter Filter;

            private readonly TaskCompletionSource<StacManResponse<T>> Tcs = new TaskCompletionSource<StacManResponse<T>>();

            public Task<StacManResponse<T>> Task
            {
                get { return Tcs.Task; }
            }

            public void GetResponse(Action callback)
            {
                try
                {
                    Client.GetApiResponse<T>(UrlBuilder, Filter, response =>
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
        }
    }
}
