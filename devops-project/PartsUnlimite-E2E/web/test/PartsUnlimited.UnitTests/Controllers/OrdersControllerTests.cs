using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PartsUnlimited.Controllers;
using PartsUnlimited.Models;
using PartsUnlimited.UnitTests.Mocks;
using PartsUnlimited.Utils;
using PartsUnlimited.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace PartsUnlimited.UnitTests.Controllers
{
    [TestClass]
    public class OrdersControllerTests
    {
        [TestMethod]
        public async Task Order_Index()
        {
            // arrange
            var mockDb = new MockDataContext();
            var mockModel = new OrdersModel(mockDb.Orders.ToList(), "bob", new DateTimeOffset(), new DateTimeOffset(), null, false);

            var mockOrdersQuery = new Mock<IOrdersQuery>();
            mockOrdersQuery.Setup(o => o.IndexHelperAsync("bob", null, null, null, false))
                .ReturnsAsync(mockModel);

            var mockTelemetryProvider = new Mock<ITelemetryProvider>();
			var mockShippingTaxCalc = new Mock<IShippingTaxCalculator>();
            OrdersController controller = GetOrdersController(mockOrdersQuery, mockTelemetryProvider, mockShippingTaxCalc);

            // act
            var resultTask = await controller.Index(null, null, null);
            var viewResult = resultTask as ViewResult;

            // assert
            Assert.IsNotNull(viewResult);
            mockOrdersQuery.Verify(o => o.IndexHelperAsync("bob", null, null, null, false), Times.Once, "IndexHelperAsync not called correctly");
            var model = viewResult.Model as OrdersModel;
            Assert.IsNotNull(model);
            Assert.AreSame(model, mockModel);
        }

        [TestMethod]
        public async Task Order_DetailWithNullId()
        {
            // arrange
            var mockOrdersQuery = new Mock<IOrdersQuery>();
            var mockTelemetryProvider = new Mock<ITelemetryProvider>();
            var mockShippingTaxCalc = new Mock<IShippingTaxCalculator>();
			mockTelemetryProvider.Setup(t => t.TrackTrace("Order/Server/NullId"));
            var queryString = new NameValueCollection();
            queryString.Add("id", null);
            var controller = GetOrdersController(mockOrdersQuery, mockTelemetryProvider, mockShippingTaxCalc, "bob", queryString);

            // act
            var resultTask = await controller.Details(null);
            var redirect = resultTask as RedirectToRouteResult;

            // assert
            Assert.IsTrue(redirect.RouteValues.Any(v => v.Key == "action" && v.Value.ToString() == "Index"));
            Assert.IsTrue(redirect.RouteValues.Any(v => v.Key == "invalidOrderSearch" && v.Value == null));
            mockTelemetryProvider.Verify(t => t.TrackTrace("Order/Server/NullId"), Times.Once);
        }

        [TestMethod]
        public async Task Order_DetailWithUserMismatch()
        {
            // arrange
            var order = new Order()
            {
                Username = "bob",
                OrderId = 1
            };
            var mockOrdersQuery = new Mock<IOrdersQuery>();
            mockOrdersQuery.Setup(o => o.FindOrderAsync(1))
                .ReturnsAsync(order);

            var mockTelemetryProvider = new Mock<ITelemetryProvider>();
            mockTelemetryProvider.Setup(t => t.TrackTrace("Order/Server/UsernameMismatch"));

            var mockShippingTaxCalc = new Mock<IShippingTaxCalculator>();
            var controller = GetOrdersController(mockOrdersQuery, mockTelemetryProvider, mockShippingTaxCalc, "ted");

            // act
            var resultTask = await controller.Details(1);
            var redirect = resultTask as RedirectToRouteResult;

            // assert
            Assert.IsTrue(redirect.RouteValues.Any(v => v.Key == "action" && v.Value.ToString() == "Index"));
            Assert.IsTrue(redirect.RouteValues.Any(v => v.Key == "invalidOrderSearch" && v.Value.ToString() == "1"));
            mockTelemetryProvider.Verify(t => t.TrackTrace("Order/Server/UsernameMismatch"), Times.Once);
        }

        [TestMethod]
        public async Task Order_DetailWithNoDetails()
        {
            // arrange
            var order = new Order()
            {
                Username = "bob",
                OrderId = 1
            };
            var mockOrdersQuery = new Mock<IOrdersQuery>();
            mockOrdersQuery.Setup(o => o.FindOrderAsync(1))
                .ReturnsAsync(order);

            var props = new Dictionary<string, string>()
            {
                { "Id", "1" },
                { "Username", "bob" }
            };

            var mockTelemetryProvider = new Mock<ITelemetryProvider>();
            mockTelemetryProvider.Setup(t => t.TrackEvent("Order/Server/NullDetails", props, null));

			var mockShippingTaxCalc = new Mock<IShippingTaxCalculator>();
			var controller = GetOrdersController(mockOrdersQuery, mockTelemetryProvider, mockShippingTaxCalc, "bob");

            // act
            var resultTask = await controller.Details(1);
            var viewResult = resultTask as ViewResult;

            // assert
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as OrderDetailsViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(0.ToString("C"), model.OrderCostSummary.CartSubTotal);
            Assert.AreEqual(0.ToString("C"), model.OrderCostSummary.CartShipping);
            Assert.AreEqual(0.ToString("C"), model.OrderCostSummary.CartTax);
            Assert.AreEqual(0.ToString("C"), model.OrderCostSummary.CartTotal);
            Assert.AreSame(order, model.Order);
            mockTelemetryProvider.Verify(t => t.TrackEvent("Order/Server/NullDetails", props, null), Times.Once);
        }

        [TestMethod]
        public async Task Order_DetailWithOrderDetails()
        {
            // arrange
            var order = new Order()
            {
                Username = "bob",
                OrderId = 1,
                OrderDetails = new List<OrderDetail>()
                {
                    new OrderDetail()
                    {
                        Count = 2,
                        Product = new Product()
                        {
                            Price = 10
                        }
                    },
                    new OrderDetail()
                    {
                        Count = 3,
                        Product = new Product()
                        {
                            Price = 5
                        }
                    }
                }
            };
            var mockOrdersQuery = new Mock<IOrdersQuery>();
            mockOrdersQuery.Setup(o => o.FindOrderAsync(1))
                .ReturnsAsync(order);

            var props = new Dictionary<string, string>()
            {
                { "Id", "1" },
                { "Username", "bob" }
            };
            var measures = new Dictionary<string, double>()
            {
                { "LineItemCount", 2 }
            };

            var mockTelemetryProvider = new Mock<ITelemetryProvider>();
            mockTelemetryProvider.Setup(t => t.TrackEvent("Order/Server/Details", props, measures));

			var mockShippingTaxCalc = new Mock<IShippingTaxCalculator>();
			var controller = GetOrdersController(mockOrdersQuery, mockTelemetryProvider, mockShippingTaxCalc, "bob");

            // act
            var resultTask = await controller.Details(1);
            var viewResult = resultTask as ViewResult;

            // assert
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as OrderDetailsViewModel;
            Assert.IsNotNull(model);
            //Assert.AreEqual(35.ToString("C"), model.OrderCostSummary.CartSubTotal);
            //Assert.AreEqual(25.ToString("C"), model.OrderCostSummary.CartShipping);
            //Assert.AreEqual(3.ToString("C"), model.OrderCostSummary.CartTax);
            //Assert.AreEqual(63.ToString("C"), model.OrderCostSummary.CartTotal);
            Assert.AreSame(order, model.Order);
            mockTelemetryProvider.Verify(t => t.TrackEvent("Order/Server/Details", props, measures), Times.Once);
        }

        private static OrdersController GetOrdersController(Mock<IOrdersQuery> mockOrdersQuery, Mock<ITelemetryProvider> mockTelemetryProvider,
            Mock<IShippingTaxCalculator> mockShippingTaxCalc, string username = "bob", NameValueCollection queryString = null)
        {
            var controller = new OrdersController(mockOrdersQuery.Object, mockTelemetryProvider.Object, mockShippingTaxCalc.Object);
            controller.ControllerContext = new ControllerContext()
            {
                Controller = controller,
                RequestContext = new RequestContext(new MockHttpContext(username, queryString), new RouteData())
            };
            return controller;
        }
    }
}