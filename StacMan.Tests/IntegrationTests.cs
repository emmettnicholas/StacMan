using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace StackExchange.StacMan.Tests
{
    [TestClass] // these actually talk over the network
    public class IntegrationTests
    {

        [TestMethod]
        public void BasicTest()
        {
            var client = new StacManClient();
            List<string> urls = new List<string>();
            client.SetUrlManager(x =>
            {
                lock (urls) urls.Add(x);
                System.Console.WriteLine("Querying: " + x);
                return x;
            });

            var sites = client.Sites.GetAll(pagesize: 50).Result;
            Assert.AreEqual("http://api.stackexchange.com/2.0/sites?pagesize=50", sites.ApiUrl);
            Assert.AreEqual(50, sites.Data.Items.Count());
            Assert.IsTrue(sites.Data.HasMore);

            lock(urls)
            {
                Assert.AreEqual(1, urls.Count);
                Assert.AreEqual("http://api.stackexchange.com/2.0/sites?pagesize=50", urls[0]);
            }
        }


    }
}
