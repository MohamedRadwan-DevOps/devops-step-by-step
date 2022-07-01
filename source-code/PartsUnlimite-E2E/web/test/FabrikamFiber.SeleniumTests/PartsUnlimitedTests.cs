using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

namespace FabrikamFiber.SeleniumTests
{
    [TestClass]
	[Ignore]
    public class PartsUnlimitedTests
    {
        static IWebDriver driver;

        [AssemblyInitialize]
        public static void Setup(TestContext context)
        {
            driver = new ChromeDriver();
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void TestShoppingCart()
        {
            var homeUrl = "http://cdrm-pu-demo-dev.azurewebsites.net";
            driver.Navigate().GoToUrl($"{homeUrl}/ShoppingCart");

            // check that the cart is empty
            var container = driver.FindElement(By.Id("shopping-cart-page"));
            Assert.AreEqual("Review your Cart", container.FindElement(By.TagName("h2")).Text);
            var empty = container.FindElement(By.Id("empty-cart"));
            Assert.IsNotNull(empty);

            // go to the first category
            driver.Navigate().GoToUrl($"{homeUrl}/Store/Browse?CategoryId=1");
            // find the 1st element
            var item = driver.FindElements(By.ClassName("list-item-part")).First();
            var itemName = item.FindElement(By.TagName("h4")).Text;
            var price = item.FindElement(By.TagName("h5")).Text;
            // naviate to the item
            item.FindElement(By.TagName("a")).Click();

            // add it to the cart
            driver.FindElement(By.ClassName("btn")).Click();

            // check the contents of the cart
            var cartContainer = driver.FindElement(By.Id("shopping-cart-page"));
            Assert.AreEqual("Review your Cart", cartContainer.FindElement(By.TagName("h2")).Text);
            var cartItems = driver.FindElements(By.ClassName("cart-item"));
            Assert.AreEqual(1, cartItems.Count);
            var cartItem = cartItems.First();
            Assert.IsTrue(cartItem.FindElements(By.TagName("a")).Any(e => e.Text == itemName));
            Assert.AreEqual(price, cartItem.FindElement(By.ClassName("item-price")).Text);

            Assert.AreEqual(price, cartContainer.FindElement(By.Id("cart-sub-total")).Text);
        }

        [TestMethod]
        public void TestSearch()
        {
            driver.Navigate().GoToUrl("http://cdrm-pu-demo-dev.azurewebsites.net/");
            driver.FindElement(By.Id("search-box")).SendKeys("oil");
            driver.FindElement(By.Id("search-link")).Click();

            Assert.AreEqual(3, driver.FindElement(By.Id("search-page")).FindElements(By.ClassName("list-item-part")).Count);
        }
    }
}
