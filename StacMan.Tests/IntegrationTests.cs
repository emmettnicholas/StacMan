using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace StackExchange.StacMan.Tests
{
    [TestClass] // these actually talk over the network
    public class IntegrationTests
    {

        [TestMethod]
        public void BasicTest_NoHost()
        {
            Execute(null);
        }

        [TestMethod]
        public void BasicTest_BlankHost()
        {
            Execute(" ");
        }

        [TestMethod]
        public void BasicTest_DefaultHost()
        {
            Execute("api.stackexchange.com");
        }

        private void Execute(string host)
        {
            var client = new StacManClient();
            if (host != null)
                client.SetHost(host);

            if (string.IsNullOrWhiteSpace(host)) host = "api.stackexchange.com";
            var sites = client.Sites.GetAll(pagesize: 50).Result;
            Assert.AreEqual("http://" + host + "/2.0/sites?pagesize=50", sites.ApiUrl);
            Assert.AreEqual(50, sites.Data.Items.Count());
            Assert.IsTrue(sites.Data.HasMore);
        }
    }
}
