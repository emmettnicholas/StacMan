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
    public class StacManResponseTests
    {
        [TestMethod]
        public void Api_web_exception_response()
        {
            var mock = new Mock<StacManClient>(null, null);
            var client = mock.Object;

            mock.FakeGET(throws: new System.Net.WebException("timeout"));
            
            var response = client.Users.GetAll("gaming.stackexchange.com").Result;
            
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
            Assert.IsInstanceOfType(response.Error, typeof(System.Net.WebException));
            Assert.IsFalse(response.ReceivedApiResponse);
            Assert.IsNotNull(response.ApiUrl);
            Assert.IsNull(response.RawData);
        }

        [TestMethod]
        public void Test_response_debugging_properties()
        {
            var mock = new Mock<StacManClient>("myappkey", null);

            //http://api.stackexchange.com/2.0/suggested-edits?pagesize=2&site=superuser
            mock.FakeGET(response: @"{""items"":[{""suggested_edit_id"":10345,""post_id"":410422,""post_type"":""question"",""comment"":""Removed unnecessary greetings"",""creation_date"":1333996736,""proposing_user"":{""user_id"":111438,""display_name"":""dnbrv"",""reputation"":348,""user_type"":""registered"",""profile_image"":""http://www.gravatar.com/avatar/0299470f4dcad8b4d79fd01c5dc7be4a?d=identicon&r=PG"",""link"":""http://superuser.com/users/111438/dnbrv""}},{""suggested_edit_id"":10344,""post_id"":410423,""post_type"":""question"",""comment"":""updated info"",""creation_date"":1333995585,""approval_date"":1333997046,""proposing_user"":{""user_id"":127397,""display_name"":""James Wilson"",""reputation"":3,""user_type"":""unregistered"",""profile_image"":""http://www.gravatar.com/avatar/ace280d5491b40c2645d856bf20337a3?d=identicon&r=PG"",""link"":""http://superuser.com/users/127397/james-wilson""}}],""quota_remaining"":262,""quota_max"":300,""has_more"":true}");

            var client = mock.Object;

            var response = client.SuggestedEdits.GetAll("superuser", pagesize: 2).Result;
            Assert.IsTrue(response.ApiUrl.Contains("site=superuser"));
            Assert.IsTrue(response.ApiUrl.Contains("pagesize=2"));
            Assert.IsTrue(response.ApiUrl.Contains("key=myappkey"));

            Assert.IsNotNull(response.RawData);
        }

        [TestMethod]
        public void Stack_Exchange_API_Exception_response()
        {
            var mock = new Mock<StacManClient>(null, null);

            //http://api.stackexchange.com/2.0/inbox?access_token=foo
            mock.FakeGET(response: @"{""error_id"":405,""error_name"":""key_required"",""error_message"":""`key` is required when `access_token` is passed.""}");

            var client = mock.Object;

            var response = client.Inbox.Get("foo").Result;

            Assert.IsFalse(response.Success);
            Assert.IsTrue(response.ReceivedApiResponse);
            Assert.IsNotNull(response.RawData);
            Assert.IsInstanceOfType(response.Error, typeof(Exceptions.StackExchangeApiException));
            Assert.AreEqual(405, ((Exceptions.StackExchangeApiException)response.Error).ErrorId);
            Assert.AreEqual("key_required", ((Exceptions.StackExchangeApiException)response.Error).ErrorName);
            Assert.AreEqual("`key` is required when `access_token` is passed.", ((Exceptions.StackExchangeApiException)response.Error).ErrorMessage);
            Assert.IsNull(response.Data.Items);
            Assert.AreEqual(405, response.Data.ErrorId);
            Assert.AreEqual("key_required", response.Data.ErrorName);
            Assert.AreEqual("`key` is required when `access_token` is passed.", response.Data.ErrorMessage);
        }
    }
}
