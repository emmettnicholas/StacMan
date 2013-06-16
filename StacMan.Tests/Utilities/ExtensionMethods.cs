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

        public static void FakeGET(this Mock<StacManClient> mock, string response = null, Exception throws = null)
        {
            mock.Setup(c => c.FetchApiResponseWithGET(It.IsAny<string>(), It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback<string, Action<string>, Action<Exception>>((url, success, error) =>
                    {
                        if (throws != null)
                            error(throws);
                        else
                            success(response);
                    });
        }

        public static void FakeGETForUrlPattern(this Mock<StacManClient> mock, string regex, string response = null, Exception throws = null)
        {
            mock.Setup(c => c.FetchApiResponseWithGET(It.IsRegex(regex), It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback<string, Action<string>, Action<Exception>>((url, success, error) =>
                    {
                        if (throws != null)
                            error(throws);
                        else
                            success(response);
                    });
        }

        public static void FakePOST(this Mock<StacManClient> mock, string response = null, Exception throws = null)
        {
            mock.Setup(c => c.FetchApiResponseWithPOST(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback<string, string, Action<string>, Action<Exception>>((url, data, success, error) =>
                {
                    if (throws != null)
                        error(throws);
                    else
                        success(response);
                });
        }
    }
}
