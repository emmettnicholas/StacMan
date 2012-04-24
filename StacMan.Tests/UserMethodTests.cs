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
    public class UserMethodTests
    {
        [TestMethod]
        public void Users_get_all_test()
        {
            var mock = new Mock<StacManClient>(FilterBehavior.Strict, null);

            //http://api.stackexchange.com/2.0/users?pagesize=1&order=desc&min=1&max=1000&sort=reputation&inname=doug&site=webapps
            mock.FakeFetch(response: @"{""items"":[{""user_id"":183,""user_type"":""registered"",""creation_date"":1277932837,""display_name"":""Doug Harris"",""profile_image"":""http://www.gravatar.com/avatar/731e7de87c241fce562d03b23770b5cf?d=identicon&r=PG"",""reputation"":545,""reputation_change_day"":0,""reputation_change_week"":0,""reputation_change_month"":0,""reputation_change_quarter"":0,""reputation_change_year"":15,""age"":92,""last_access_date"":1332536617,""last_modified_date"":1332297406,""is_employee"":false,""link"":""http://webapps.stackexchange.com/users/183/doug-harris"",""website_url"":""http://delicious.com/dharris"",""location"":""Washington, DC"",""account_id"":46903,""badge_counts"":{""gold"":1,""silver"":4,""bronze"":8},""accept_rate"":25}],""quota_remaining"":-210785,""quota_max"":300,""has_more"":true}");

            var client = mock.Object;

            var result = client.Users.GetAll("webapps", pagesize: 1, order: Order.Desc, min: 1, max: 1000, sort: Users.Sort.Reputation, inname: "doug").Result;
            Assert.IsTrue(result.Success);

            var user = result.Data.Items.Single();
            Assert.AreEqual(183, user.UserId);
            Assert.AreEqual(Users.UserType.Registered, user.UserType);
            Assert.AreEqual("Doug Harris", user.DisplayName);
            Assert.AreEqual("http://www.gravatar.com/avatar/731e7de87c241fce562d03b23770b5cf?d=identicon&r=PG", user.ProfileImage);
            Assert.AreEqual(545, user.Reputation);
            Assert.AreEqual(0, user.ReputationChangeDay);
            Assert.AreEqual(0, user.ReputationChangeWeek);
            Assert.AreEqual(0, user.ReputationChangeMonth);
            Assert.AreEqual(0, user.ReputationChangeQuarter);
            Assert.AreEqual(15, user.ReputationChangeYear);
            Assert.AreEqual(92, user.Age);
            Assert.AreEqual(1332536617L.ToDateTime(), user.LastAccessDate);
            Assert.AreEqual(1332297406L.ToDateTime(), user.LastModifiedDate);
            Assert.IsFalse(user.IsEmployee);
            Assert.AreEqual("http://webapps.stackexchange.com/users/183/doug-harris", user.Link);
            Assert.AreEqual("http://delicious.com/dharris", user.WebsiteUrl);
            Assert.AreEqual("Washington, DC", user.Location);
            Assert.AreEqual(46903, user.AccountId);
            Assert.AreEqual(1, user.BadgeCounts.Gold);
            Assert.AreEqual(4, user.BadgeCounts.Silver);
            Assert.AreEqual(8, user.BadgeCounts.Bronze);
            Assert.AreEqual(25, user.AcceptRate);
            Assert.IsNull(user.TimedPenaltyDate);
        }

        [TestMethod]
        public void Users_get_associated_test()
        {
            var mock = new Mock<StacManClient>(FilterBehavior.Strict, null);

            //http://api.stackexchange.com/2.0/users/1998/associated?pagesize=2
            mock.FakeFetch(response: @"{""items"":[{""site_name"":""Stack Overflow"",""site_url"":""http://stackoverflow.com"",""user_id"":2749,""reputation"":4365,""account_id"":1998,""creation_date"":1219613204,""badge_counts"":{""gold"":1,""silver"":13,""bronze"":26},""last_access_date"":1334611626,""answer_count"":144,""question_count"":20},{""site_name"":""Server Fault"",""site_url"":""http://serverfault.com"",""user_id"":31532,""reputation"":101,""account_id"":1998,""creation_date"":1263334764,""badge_counts"":{""gold"":0,""silver"":0,""bronze"":3},""last_access_date"":1334610908,""answer_count"":0,""question_count"":0}],""quota_remaining"":-212787,""quota_max"":300,""has_more"":true}");

            var client = mock.Object;

            var result = client.Users.GetAssociated(new int[] { 1998 }, pagesize: 2).Result;
            Assert.IsTrue(result.Success);

            var networkUser = result.Data.Items.First();
            Assert.AreEqual("Stack Overflow", networkUser.SiteName);
            Assert.AreEqual("http://stackoverflow.com", networkUser.SiteUrl);
            Assert.AreEqual(2749, networkUser.UserId);
            Assert.AreEqual(4365, networkUser.Reputation);
            Assert.AreEqual(1998, networkUser.AccountId);
            Assert.AreEqual(1219613204L.ToDateTime(), networkUser.CreationDate);
            Assert.AreEqual(1, networkUser.BadgeCounts.Gold);
            Assert.AreEqual(13, networkUser.BadgeCounts.Silver);
            Assert.AreEqual(26, networkUser.BadgeCounts.Bronze);
            Assert.AreEqual(1334611626L.ToDateTime(), networkUser.LastAccessDate);
            Assert.AreEqual(144, networkUser.AnswerCount);
            Assert.AreEqual(20, networkUser.QuestionCount);
            Assert2.Throws<Exceptions.FilterException>(() => { var t = networkUser.UserType; });
        }
    }
}
