using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartsUnlimited.ProductSearch;
using PartsUnlimited.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsUnlimited.UnitTests.ProductSearch
{
    [TestClass]
    public class ProductSearchTests
    {
        [TestMethod]
        public async Task ProductSearch_TestStringExistingProduct()
        {
            var prodSearch = new StringContainsProductSearch(new MockDataContext());
            var result = await prodSearch.Search("proDucT 1");
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Any(p => p.Title == "Product 1"));
        }

        [TestMethod]
        public async Task ProductSearch_TestStringExistingProductPartial()
        {
            var prodSearch = new StringContainsProductSearch(new MockDataContext());
            var result = await prodSearch.Search("proD");
            Assert.AreEqual(6, result.Count());
        }

        [TestMethod]
        public async Task ProductSearch_TestStringNoHit()
        {
            var prodSearch = new StringContainsProductSearch(new MockDataContext());
            var result = await prodSearch.Search("nothing");
            Assert.AreEqual(0, result.Count());
        }
    }
}
