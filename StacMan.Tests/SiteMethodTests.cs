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
    public class SiteMethodTests
    {
        [TestMethod]
        public async Task Sites_get_all_test()
        {
            var mock = new Mock<StacManClient>(null, null);

            //http://api.stackexchange.com/2.0/sites?page=1&pagesize=1
            mock.FakeGET(response: @"{""items"":[{""site_type"":""main_site"",""name"":""Stack Overflow"",""logo_url"":""http://cdn.sstatic.net/stackoverflow/img/logo.png"",""api_site_parameter"":""stackoverflow"",""site_url"":""http://stackoverflow.com"",""audience"":""professional and enthusiast programmers"",""icon_url"":""http://cdn.sstatic.net/stackoverflow/img/apple-touch-icon.png"",""aliases"":[""http://www.stackoverflow.com""],""site_state"":""normal"",""styling"":{""link_color"":""#0077CC"",""tag_foreground_color"":""#3E6D8E"",""tag_background_color"":""#E0EAF1""},""launch_date"":1221436800,""favicon_url"":""http://cdn.sstatic.net/stackoverflow/img/favicon.ico"",""related_sites"":[{""name"":""Stack Overflow Chat"",""site_url"":""http://chat.stackoverflow.com"",""relation"":""chat""}],""markdown_extensions"":[""Prettify""]}],""quota_remaining"":-50833,""quota_max"":300,""has_more"":true}");

            var client = mock.Object;

            var result = await client.Sites.GetAll(filter: "default", page: 1, pagesize: 1);
            Assert.IsTrue(result.Success);

            var site = result.Data.Items.Single();

            Assert.AreEqual("main_site", site.SiteType);
            Assert.AreEqual("Stack Overflow", site.Name);
            Assert.AreEqual("http://cdn.sstatic.net/stackoverflow/img/logo.png", site.LogoUrl);
            Assert.AreEqual("stackoverflow", site.ApiSiteParameter);
            Assert.AreEqual("http://stackoverflow.com", site.SiteUrl);
            Assert.AreEqual("professional and enthusiast programmers", site.Audience);
            Assert.AreEqual("http://cdn.sstatic.net/stackoverflow/img/apple-touch-icon.png", site.IconUrl);
            Assert.AreEqual("http://www.stackoverflow.com", site.Aliases.Single());
            Assert.AreEqual(StacMan.Sites.SiteState.Normal, site.SiteState);
            Assert.AreEqual("#0077CC", site.Styling.LinkColor);
            Assert.AreEqual("#3E6D8E", site.Styling.TagForegroundColor);
            Assert.AreEqual("#E0EAF1", site.Styling.TagBackgroundColor);
            Assert.AreEqual(1221436800L.ToDateTime(), site.LaunchDate);
            Assert.AreEqual("http://cdn.sstatic.net/stackoverflow/img/favicon.ico", site.FaviconUrl);
            Assert.AreEqual("Stack Overflow Chat", site.RelatedSites.Single().Name);
            Assert.AreEqual("http://chat.stackoverflow.com", site.RelatedSites.Single().SiteUrl);
            Assert.AreEqual("chat", site.RelatedSites.Single().Relation);
            Assert.AreEqual("Prettify", site.MarkdownExtensions.Single());
        }
    }
}
