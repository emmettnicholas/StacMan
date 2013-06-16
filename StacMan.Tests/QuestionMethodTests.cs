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
    public class QuestionMethodTests
    {
        [TestMethod]
        public void Questions_get_all_test()
        {
            var mock = new Mock<StacManClient>(null, null);

            //http://api.stackexchange.com/2.0/questions?pagesize=1&fromdate=1328054400&order=desc&sort=activity&tagged=starcraft-2&site=gaming
            mock.FakeGET(response: @"{""items"":[{""question_id"":62531,""last_edit_date"":1334545016,""creation_date"":1334446664,""last_activity_date"":1334566818,""score"":1,""answer_count"":2,""title"":""Why does the &#39;favored&#39; rating change from start of game to end of game in Starcraft 2?"",""tags"":[""starcraft-2"",""multiplayer""],""view_count"":96,""owner"":{""user_id"":2030,""display_name"":""Kelsey"",""reputation"":273,""user_type"":""registered"",""profile_image"":""http://www.gravatar.com/avatar/395b66642a372ee82bbc821bdc2697a4?d=identicon&r=PG"",""link"":""http://gaming.stackexchange.com/users/2030/kelsey""},""link"":""http://gaming.stackexchange.com/questions/62531/why-does-the-favored-rating-change-from-start-of-game-to-end-of-game-in-starcr"",""is_answered"":true}],""quota_remaining"":-81147,""quota_max"":300,""has_more"":true}");

            var client = mock.Object;

            var result = client.Questions.GetAll("gaming", pagesize: 1, fromdate: new DateTime(2012, 2, 1, 0, 0, 0, DateTimeKind.Utc), order: Order.Desc, sort: Questions.AllSort.Activity, tagged: "starcraft-2").Result;
            Assert.IsTrue(result.Success);

            var question = result.Data.Items.Single();
            Assert.AreEqual(1334545016L.ToDateTime(), question.LastEditDate);
            Assert.AreEqual(1334446664L.ToDateTime(), question.CreationDate);
            Assert.AreEqual(1334566818L.ToDateTime(), question.LastActivityDate);
            Assert.AreEqual(1, question.Score);
            Assert.AreEqual(2, question.AnswerCount);
            Assert.AreEqual("Why does the &#39;favored&#39; rating change from start of game to end of game in Starcraft 2?", question.Title);
            Assert.AreEqual(2, question.Tags.Length);
            Assert.AreEqual("starcraft-2", question.Tags.First());
            Assert.AreEqual(96, question.ViewCount);
            Assert.AreEqual(2030, question.Owner.UserId);
            Assert.AreEqual("Kelsey", question.Owner.DisplayName);
            Assert.AreEqual(273, question.Owner.Reputation);
            Assert.AreEqual(Users.UserType.Registered, question.Owner.UserType);
            Assert.AreEqual("http://www.gravatar.com/avatar/395b66642a372ee82bbc821bdc2697a4?d=identicon&r=PG", question.Owner.ProfileImage);
            Assert.AreEqual("http://gaming.stackexchange.com/users/2030/kelsey", question.Owner.Link);
            Assert.AreEqual("http://gaming.stackexchange.com/questions/62531/why-does-the-favored-rating-change-from-start-of-game-to-end-of-game-in-starcr", question.Link);
            Assert.IsTrue(question.IsAnswered);
        }

        [TestMethod]
        public void Questions_by_id_test()
        {
            var mock = new Mock<StacManClient>(null, null);

            //http://api.stackexchange.com/2.0/questions/13332?order=desc&sort=activity&site=gaming
            mock.FakeGET(response: @"{""items"":[{""question_id"":13332,""last_edit_date"":1296990699,""creation_date"":1278527266,""last_activity_date"":1296990699,""score"":1,""answer_count"":1,""migrated_from"":{""question_id"":161024,""other_site"":{""site_type"":""main_site"",""name"":""Super User"",""logo_url"":""http://cdn.sstatic.net/superuser/img/logo.png"",""api_site_parameter"":""superuser"",""site_url"":""http://superuser.com"",""audience"":""computer enthusiasts and power users"",""icon_url"":""http://cdn.sstatic.net/superuser/img/apple-touch-icon.png"",""site_state"":""normal"",""styling"":{""link_color"":""#1086A4"",""tag_foreground_color"":""#1087A4"",""tag_background_color"":""#FFFFFF""},""launch_date"":1250553600,""favicon_url"":""http://cdn.sstatic.net/superuser/img/favicon.ico"",""related_sites"":[{""name"":""Meta Super User"",""site_url"":""http://meta.superuser.com"",""relation"":""meta"",""api_site_parameter"":""meta.superuser""},{""name"":""Chat Stack Exchange"",""site_url"":""http://chat.stackexchange.com"",""relation"":""chat""}],""twitter_account"":""StackSuper_User""},""on_date"":1293296833},""title"":""How do I run Worms: World Party on Ubuntu?"",""tags"":[""linux"",""unix"",""ubuntu""],""view_count"":845,""owner"":{""display_name"":""gcc"",""user_type"":""does_not_exist""},""link"":""http://gaming.stackexchange.com/questions/13332/how-do-i-run-worms-world-party-on-ubuntu"",""is_answered"":true}],""quota_remaining"":-89970,""quota_max"":300,""has_more"":false}");

            var client = mock.Object;

            var result = client.Questions.GetByIds("gaming", new int[] { 13332 }, order: Order.Desc, sort: Questions.Sort.Activity).Result;
            Assert.IsTrue(result.Success);

            var question = result.Data.Items.Single();
            Assert.AreEqual(13332, question.QuestionId);
            Assert.IsNull(question.AcceptedAnswerId);
            Assert.AreEqual(161024, question.MigratedFrom.QuestionId);
            Assert.AreEqual("Super User", question.MigratedFrom.OtherSite.Name);
            Assert.AreEqual("#1086A4", question.MigratedFrom.OtherSite.Styling.LinkColor);
            Assert.AreEqual(2, question.MigratedFrom.OtherSite.RelatedSites.Length);
            Assert.AreEqual(question.Tags.Length, 3);
            Assert.AreEqual("gcc", question.Owner.DisplayName);
            Assert.AreEqual(Users.UserType.DoesNotExist, question.Owner.UserType);
            Assert.IsNull(question.Owner.UserId);
            Assert.IsNull(question.Owner.Reputation);
        }
    }
}
