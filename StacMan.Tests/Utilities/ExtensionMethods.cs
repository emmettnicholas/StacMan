using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;

namespace StackExchange.StacMan.Tests.Utilities
{
    public static class ExtensionMethods
    {
        private static readonly DateTime UnixEpochDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime ToDateTime(this long secondsSince1970)
        {
            return UnixEpochDateTimeUtc.AddSeconds(secondsSince1970);
        }

        public static void FakeFetch(this Mock<StacManClient> mock, string response = null, Exception throws = null)
        {
            mock.Setup(c => c.FetchApiResponse(It.IsAny<string>(), It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback<string, Action<string>, Action<Exception>>((url, success, error) =>
                    {
                        if (throws != null)
                            error(throws);
                        else
                            success(response);
                    });
        }

        public static void FakeFetchForRegex(this Mock<StacManClient> mock, string regex, string response = null, Exception throws = null)
        {
            mock.Setup(c => c.FetchApiResponse(It.IsRegex(regex), It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback<string, Action<string>, Action<Exception>>((url, success, error) =>
                    {
                        if (throws != null)
                            error(throws);
                        else
                            success(response);
                    });
        }
    }
}
