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
    public class ValidationTests
    {
        public ValidationTests()
        {
            var mock = new Mock<StacManClient>(null, null);
            mock.FakeGET(response: "{}");
            Client = mock.Object;
        }

        private readonly StacManClient Client;

        [TestMethod]
        public async Task Invalidates_null_or_empty_site()
        {
            await Client.Users.GetByIds("stackoverflow", new int[] { 1, 2, 3 });
            await Assert2.ThrowsArgumentNullException(async () => await Client.Users.GetByIds(null, new int[] { 1, 2, 3 }), "site");
            await Assert2.ThrowsArgumentException(async () => await Client.Users.GetByIds(String.Empty, new int[] { 1, 2, 3 }), "site");
        }

        [TestMethod]
        public async Task Invalidates_null_or_empty_vector()
        {
            await Client.Users.GetByIds("stackoverflow", new int[] { 1, 2, 3 });
            await Assert2.ThrowsArgumentNullException(async () => await Client.Users.GetByIds("stackoverflow", null), "ids");
            await Assert2.ThrowsArgumentException(async () => await Client.Users.GetByIds("stackoverflow", new int[0]), "ids");

            await Client.Filters.Read(new string[] { "foo", "bar", "baz" });
            await Assert2.ThrowsArgumentNullException(async () => await Client.Filters.Read(null), "filters");
            await Assert2.ThrowsArgumentException(async () => await Client.Filters.Read(new string[0]), "filters");
        }

        [TestMethod]
        public async Task Invalidates_bad_paging()
        {
            await Client.Users.GetByIds("stackoverflow", new int[] { 1, 2, 3 }, page: 1, pagesize: 0);
            await Assert2.ThrowsArgumentException(async () => await Client.Users.GetByIds("stackoverflow", new int[] { 1, 2, 3 }, page: 0, pagesize: 10), "page");
            await Assert2.ThrowsArgumentException(async () => await Client.Users.GetByIds("stackoverflow", new int[] { 1, 2, 3 }, page: 10, pagesize: -1), "pagesize");
        }

        [TestMethod]
        public async Task Invalidates_bad_sorts()
        {
            var today = DateTime.UtcNow;
            var yesterday = today.AddDays(-1);

            await Client.Questions.GetAll("stackoverflow");
            await Client.Questions.GetAll("stackoverflow", mindate: yesterday);
            await Client.Questions.GetAll("stackoverflow", maxdate: today);
            await Assert2.ThrowsArgumentException(async () => await Client.Questions.GetAll("stackoverflow", min: 1), "min");
            await Assert2.ThrowsArgumentException(async () => await Client.Questions.GetAll("stackoverflow", max: 1), "max");

            await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Activity);
            await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Activity, mindate: yesterday);
            await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Activity, maxdate: today);
            await Assert2.ThrowsArgumentException(async () => await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Activity, min: 1), "min");
            await Assert2.ThrowsArgumentException(async () => await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Activity, max: 1), "max");
            await Assert2.ThrowsArgumentException(async () => await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Activity, mindate: yesterday, maxdate: today, max: 1), "max");

            await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Votes);
            await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Votes, min: 1);
            await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Votes, max: 1);
            await Assert2.ThrowsArgumentException(async () => await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Votes, mindate: yesterday), "mindate");
            await Assert2.ThrowsArgumentException(async () => await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Votes, maxdate: today), "maxdate");
            await Assert2.ThrowsArgumentException(async () => await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Votes, min: 1, max: 2, maxdate: today), "maxdate");

            await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Week);
            await Assert2.ThrowsArgumentException(async () => await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Week, mindate: yesterday), "mindate");
            await Assert2.ThrowsArgumentException(async () => await Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Week, min: 1), "min");
        }
    }
}
