using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using StackExchange.StacMan.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace StackExchange.StacMan.Tests
{
    [TestClass]
    public class BackoffTests
    {
        [TestMethod]
        public void Backoff_test()
        {
            var mockSlow = new Mock<StacManClient>(null, null);
            var mockFast = new Mock<StacManClient>(null, null);

            //http://api.stackexchange.com/2.0/users?pagesize=1&site=stackoverflow
            mockSlow.FakeGET(response: @"{""backoff"":1,""items"":[{""user_id"":22656,""user_type"":""registered"",""creation_date"":1222430705,""display_name"":""Jon Skeet"",""profile_image"":""http://www.gravatar.com/avatar/6d8ebb117e8d83d74ea95fbdd0f87e13?d=identicon&r=PG"",""reputation"":431980,""reputation_change_day"":365,""reputation_change_week"":2286,""reputation_change_month"":9666,""reputation_change_quarter"":9666,""reputation_change_year"":44922,""age"":35,""last_access_date"":1335550327,""last_modified_date"":1335533187,""is_employee"":false,""link"":""http://stackoverflow.com/users/22656/jon-skeet"",""website_url"":""http://csharpindepth.com"",""location"":""Reading, United Kingdom"",""account_id"":11683,""badge_counts"":{""gold"":124,""silver"":1896,""bronze"":3221},""accept_rate"":95}],""quota_remaining"":109,""quota_max"":300,""has_more"":true}");
            mockFast.FakeGET(response: @"{""items"":[{""user_id"":22656,""user_type"":""registered"",""creation_date"":1222430705,""display_name"":""Jon Skeet"",""profile_image"":""http://www.gravatar.com/avatar/6d8ebb117e8d83d74ea95fbdd0f87e13?d=identicon&r=PG"",""reputation"":431980,""reputation_change_day"":365,""reputation_change_week"":2286,""reputation_change_month"":9666,""reputation_change_quarter"":9666,""reputation_change_year"":44922,""age"":35,""last_access_date"":1335550327,""last_modified_date"":1335533187,""is_employee"":false,""link"":""http://stackoverflow.com/users/22656/jon-skeet"",""website_url"":""http://csharpindepth.com"",""location"":""Reading, United Kingdom"",""account_id"":11683,""badge_counts"":{""gold"":124,""silver"":1896,""bronze"":3221},""accept_rate"":95}],""quota_remaining"":109,""quota_max"":300,""has_more"":true}");

            var clientSlow = mockSlow.Object;
            var clientFast = mockFast.Object;

            Action<StacManClient, Action<long>> measure = (client, verifyElapsedMs) =>
            {
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                var result = client.Users.GetAll("stackoverflow", pagesize: 1).Result;
                var result2 = client.Users.GetAll("stackoverflow", pagesize: 1).Result;
                verifyElapsedMs(stopwatch.ElapsedMilliseconds);
            };

            measure(clientSlow, elapsedMs => Assert.IsTrue(elapsedMs >= 1000));
            measure(clientFast, elapsedMs => Assert.IsTrue(elapsedMs < 1000));

            clientSlow.RespectBackoffs = false;
            measure(clientSlow, elapsedMs => Assert.IsTrue(elapsedMs < 1000));
        }
    }
}
