using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartsUnlimited.UnitTests.Mocks;
using PartsUnlimited.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsUnlimited.UnitTests.Utils
{
    [TestClass]
    public class OrdersQueryTests
    {
        [TestMethod]
        public async Task OrdersQuery_IndexHelperWithNoUsername()
        {
            var ordersQuery = new OrdersQuery(new MockDataContext());
            var model = await ordersQuery.IndexHelperAsync(null, new DateTime(2016, 1, 1), new DateTime(2016, 1, 3), null, false);

            Assert.AreEqual(2, model.Orders.Count());
        }

        [TestMethod]
        public async Task OrdersQuery_IndexHelperWithUsername()
        {
            var ordersQuery = new OrdersQuery(new MockDataContext());
            var model = await ordersQuery.IndexHelperAsync("bob", new DateTime(2016, 1, 1), new DateTime(2016, 1, 3), null, false);

            Assert.AreEqual(1, model.Orders.Count());
            Assert.IsTrue(model.Orders.Any(o => o.Username == "bob"));
            Assert.IsFalse(model.Orders.Any(o => o.Username == "sue"));
        }
    }
}
