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
    public class TagMethodTests
    {
        [TestMethod]
        public void Tags_get_all_test()
        {
            var mock = new Mock<StacManClient>(FilterBehavior.Strict, null);

            //http://api.stackexchange.com/2.0/tags?page=3&pagesize=2&order=desc&sort=popular&site=gaming
            mock.FakeFetch(response: @"{""items"":[{""name"":""league-of-legends"",""count"":768,""is_required"":false,""is_moderator_only"":false,""has_synonyms"":true},{""name"":""pc"",""count"":607,""is_required"":false,""is_moderator_only"":false,""has_synonyms"":false}],""quota_remaining"":-47478,""quota_max"":300,""has_more"":true}");

            var client = mock.Object;

            var result = client.Tags.GetAll("gaming", page: 3, pagesize: 2, order: Order.Desc, sort: Tags.Sort.Popular).Result;
            Assert.IsTrue(result.Success);

            var tag = result.Data.Items.Skip(1).First();

            Assert.AreEqual("pc", tag.Name);
            Assert.AreEqual(607, tag.Count);
            Assert.IsFalse(tag.IsRequired);
            Assert.IsFalse(tag.IsModeratorOnly);
            Assert.IsFalse(tag.HasSynonyms);
            Assert.IsNull(tag.UserId);
            Assert2.Throws<Exceptions.FilterException>(() => { var d = tag.LastActivityDate; });
        }
    }
}
