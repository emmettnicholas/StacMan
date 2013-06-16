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
    public class InfoMethodTests
    {
        [TestMethod]
        public void Info_get_test()
        {
            var mock = new Mock<StacManClient>(null, null);

            //http://api.stackexchange.com/2.0/info?site=english
            mock.FakeGET(response: @"{""items"":[{""total_questions"":14647,""total_unanswered"":15,""total_accepted"":10674,""total_answers"":41975,""questions_per_minute"":0.01,""answers_per_minute"":0.02,""total_comments"":112674,""total_votes"":251459,""total_badges"":41800,""badges_per_minute"":0.02,""total_users"":17118,""new_active_users"":1,""api_revision"":""2012.4.12.2100""}],""quota_remaining"":291,""quota_max"":300,""has_more"":false}");

            var client = mock.Object;

            var result = client.Info.Get("english").Result;
            Assert.IsTrue(result.Success);
            
            var info = result.Data.Items.Single();
            Assert.AreEqual(14647, info.TotalQuestions);
            Assert.AreEqual(15, info.TotalUnanswered);
            Assert.AreEqual(10674, info.TotalAccepted);
            Assert.AreEqual(41975, info.TotalAnswers);
            Assert.AreEqual(.01m, info.QuestionsPerMinute);
            Assert.AreEqual(.02m, info.AnswersPerMinute);
            Assert.AreEqual(112674, info.TotalComments);
            Assert.AreEqual(251459, info.TotalVotes);
            Assert.AreEqual(41800, info.TotalBadges);
            Assert.AreEqual(.02m, info.BadgesPerMinute);
            Assert.AreEqual(17118, info.TotalUsers);
            Assert.AreEqual(1, info.NewActiveUsers);
            Assert.AreEqual("2012.4.12.2100", info.ApiRevision);
            Assert.IsNull(info.Site);
        }
    }
}
