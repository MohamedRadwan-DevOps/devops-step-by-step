using FakeDbSet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PartsUnlimited.Controllers;
using PartsUnlimited.Models;
using PartsUnlimited.UnitTests.Mocks;
using PartsUnlimited.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PartsUnlimited.UnitTests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Home_Index()
        {
            // arrange
            var controller = new HomeController(new MockDataContext());

            // act
            ViewResult result = controller.Index() as ViewResult;

            // assert
            Assert.IsNotNull(result);
            var model = result.Model as HomeViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(4, model.TopSellingProducts.Count);
            Assert.IsTrue(model.TopSellingProducts.Any(t => t.ProductId == 1));
            Assert.IsTrue(model.TopSellingProducts.Any(t => t.ProductId == 2));
            Assert.IsTrue(model.TopSellingProducts.Any(t => t.ProductId == 3));
            Assert.IsTrue(model.TopSellingProducts.Any(t => t.ProductId == 4));
            Assert.IsFalse(model.TopSellingProducts.Any(t => t.ProductId == 5));
            Assert.IsFalse(model.TopSellingProducts.Any(t => t.ProductId == 6));
            Assert.AreEqual(4, model.NewProducts.Count);
            Assert.AreEqual(4, model.CommunityPosts.Count);
        }
    }
}
