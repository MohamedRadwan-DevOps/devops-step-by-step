using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PartsUnlimited.Recommendations;
using PartsUnlimited.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static PartsUnlimited.Recommendations.AzureMLFrequentlyBoughtTogetherRecommendationEngine;

namespace PartsUnlimited.UnitTests.Recommendations
{
    [TestClass]
    public class AzureMLFrequentlyBoughtTogetherRecommendationsTests
    {
        [TestMethod]
        public async Task AzureMLRecommendation_Exception()
        {
            var mockClient = new Mock<IHttpClient>();
            mockClient.Setup(c => c.GetStringAsync(It.IsAny<string>())).ThrowsAsync(new HttpRequestException());

            var mockTelemetryProvider = new Mock<ITelemetryProvider>();
            mockTelemetryProvider.Setup(t => t.TrackException(It.IsAny<Exception>()));

            var engine = new AzureMLFrequentlyBoughtTogetherRecommendationEngine(mockClient.Object, mockTelemetryProvider.Object);
            var recs = await engine.GetRecommendationsAsync("1");

            Assert.AreEqual(0, recs.Count());
            mockTelemetryProvider.Verify(t => t.TrackException(It.IsAny<HttpRequestException>()), Times.Once);
        }

        [TestMethod]
        public async Task AzureMLRecommendation_Result()
        {
            var mockClient = new Mock<IHttpClient>();
            mockClient.Setup(c => c.GetStringAsync(It.IsAny<string>())).ReturnsAsync("{\"ItemSet\":[\"2\",\"3\"],\"Value\":1}");

            var mockTelemetryProvider = new Mock<ITelemetryProvider>();

            var engine = new AzureMLFrequentlyBoughtTogetherRecommendationEngine(mockClient.Object, mockTelemetryProvider.Object);
            var recs = await engine.GetRecommendationsAsync("1");

            Assert.AreEqual(2, recs.Count());
        }

        [TestMethod]
        public async Task AzureMLRecommendation_NoResult()
        {
            var mockClient = new Mock<IHttpClient>();
            mockClient.Setup(c => c.GetStringAsync(It.IsAny<string>())).ReturnsAsync("{}");

            var mockTelemetryProvider = new Mock<ITelemetryProvider>();

            var engine = new AzureMLFrequentlyBoughtTogetherRecommendationEngine(mockClient.Object, mockTelemetryProvider.Object);
            var recs = await engine.GetRecommendationsAsync("5");

            Assert.AreEqual(0, recs.Count());
        }
    }
}
