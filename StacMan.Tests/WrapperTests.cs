using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.StacMan.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace StackExchange.StacMan.Tests
{
    [TestClass]
    public class WrapperTests
    {
        [TestMethod]
        public async Task Wrapper_fields_test()
        {
            var mock = new Mock<StacManClient>(null, null);

            //http://api.stackexchange.com/2.0/filters/!*bOpvmsY(F)
            mock.FakeGETForUrlPattern("filters", response: @"{""items"":[{""filter"":""!*bOpvmsY(F)"",""included_fields"":["".backoff"","".error_id"","".error_message"","".error_name"","".has_more"","".items"","".page"","".page_size"","".quota_max"","".quota_remaining"","".total"","".type"",""badge.badge_id""],""filter_type"":""safe""}],""quota_remaining"":271,""quota_max"":300,""has_more"":false}");

            //http://api.stackexchange.com/2.0/badges?page=2&pagesize=5&site=stackoverflow&filter=!*bOpvmsY(F)
            mock.FakeGETForUrlPattern("badges", @"{""total"":1713,""page_size"":5,""page"":2,""type"":""badge"",""items"":[{""badge_id"":460},{""badge_id"":461},{""badge_id"":462},{""badge_id"":463},{""badge_id"":464}],""quota_remaining"":273,""quota_max"":300,""has_more"":true}");

            var client = mock.Object;
            var result = await client.Badges.GetAll("stackoverflow", page: 2, pagesize: 5, filter: "!*bOpvmsY(F)");
            var wrapper = result.Data;

            Assert.AreEqual(1713, wrapper.Total);
            Assert.AreEqual(5, wrapper.PageSize);
            Assert.AreEqual(2, wrapper.Page);
            Assert.AreEqual(5, wrapper.Items.Length);
            Assert.AreEqual("badge", wrapper.Type);
            Assert.AreEqual(273, wrapper.QuotaRemaining);
            Assert.AreEqual(300, wrapper.QuotaMax);
            Assert.AreEqual(true, wrapper.HasMore);
            Assert.IsNull(wrapper.Backoff);
            Assert.IsNull(wrapper.ErrorId);
            Assert.IsNull(wrapper.ErrorName);
            Assert.IsNull(wrapper.ErrorMessage);
        }
    }
}
