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
        public void Invalidates_null_or_empty_site()
        {
            Client.Users.GetByIds("stackoverflow", new int[] { 1, 2, 3 });
            Assert2.ThrowsArgumentNullException(() => Client.Users.GetByIds(null, new int[] { 1, 2, 3 }), "site");
            Assert2.ThrowsArgumentException(() => Client.Users.GetByIds(String.Empty, new int[] { 1, 2, 3 }), "site");
        }

        [TestMethod]
        public void Invalidates_null_or_empty_vector()
        {
            Client.Users.GetByIds("stackoverflow", new int[] { 1, 2, 3 });
            Assert2.ThrowsArgumentNullException(() => Client.Users.GetByIds("stackoverflow", null), "ids");
            Assert2.ThrowsArgumentException(() => Client.Users.GetByIds("stackoverflow", new int[0]), "ids");

            Client.Filters.Read(new string[] { "foo", "bar", "baz" });
            Assert2.ThrowsArgumentNullException(() => Client.Filters.Read(null), "filters");
            Assert2.ThrowsArgumentException(() => Client.Filters.Read(new string[0]), "filters");
        }

        [TestMethod]
        public void Invalidates_bad_paging()
        {
            Client.Users.GetByIds("stackoverflow", new int[] { 1, 2, 3 }, page: 1, pagesize: 0);
            Assert2.ThrowsArgumentException(() => Client.Users.GetByIds("stackoverflow", new int[] { 1, 2, 3 }, page: 0, pagesize: 10), "page");
            Assert2.ThrowsArgumentException(() => Client.Users.GetByIds("stackoverflow", new int[] { 1, 2, 3 }, page: 10, pagesize: -1), "pagesize");
        }

        [TestMethod]
        public void Invalidates_bad_sorts()
        {
            var today = DateTime.UtcNow;
            var yesterday = today.AddDays(-1);

            Client.Questions.GetAll("stackoverflow");
            Client.Questions.GetAll("stackoverflow", mindate: yesterday);
            Client.Questions.GetAll("stackoverflow", maxdate: today);
            Assert2.ThrowsArgumentException(() => Client.Questions.GetAll("stackoverflow", min: 1), "min");
            Assert2.ThrowsArgumentException(() => Client.Questions.GetAll("stackoverflow", max: 1), "max");

            Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Activity);
            Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Activity, mindate: yesterday);
            Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Activity, maxdate: today);
            Assert2.ThrowsArgumentException(() => Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Activity, min: 1), "min");
            Assert2.ThrowsArgumentException(() => Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Activity, max: 1), "max");
            Assert2.ThrowsArgumentException(() => Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Activity, mindate: yesterday, maxdate: today, max: 1), "max");

            Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Votes);
            Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Votes, min: 1);
            Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Votes, max: 1);
            Assert2.ThrowsArgumentException(() => Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Votes, mindate: yesterday), "mindate");
            Assert2.ThrowsArgumentException(() => Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Votes, maxdate: today), "maxdate");
            Assert2.ThrowsArgumentException(() => Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Votes, min: 1, max: 2, maxdate: today), "maxdate");

            Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Week);
            Assert2.ThrowsArgumentException(() => Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Week, mindate: yesterday), "mindate");
            Assert2.ThrowsArgumentException(() => Client.Questions.GetAll("stackoverflow", sort: Questions.AllSort.Week, min: 1), "min");
        }
    }
}
