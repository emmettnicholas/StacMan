using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StackExchange.StacMan.Tests.Utilities
{
    public static class Assert2
    {
        public static async Task Throws<T>(Func<Task> func) where T : Exception
        {
            try
            {
                await func();
                Assert.Fail("An exception of type {0} was expected, but not thrown", typeof(T));
            }
            catch (T) { }
        }

        public static async Task ThrowsArgumentException(Func<Task> func, string paramName)
        {
            try
            {
                await func();
                Assert.Fail("An ArgumentException for param {0} was exepcted, but not thrown", paramName);
            }
            catch (ArgumentException ex)
            {
                if (ex.ParamName != paramName)
                    Assert.Fail("An ArgumentException for param {0} was exepcted, but not thrown", paramName);
            }
        }

        public static async Task ThrowsArgumentNullException(Func<Task> func, string paramName)
        {
            try
            {
                await func();
                Assert.Fail("An ArgumentNullException for param {0} was exepcted, but not thrown", paramName);
            }
            catch (ArgumentNullException ex)
            {
                if (ex.ParamName != paramName)
                    Assert.Fail("An ArgumentNullException for param {0} was exepcted, but not thrown", paramName);
            }
        }
    }
}
