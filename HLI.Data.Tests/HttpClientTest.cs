using System.Linq;
using System.Threading.Tasks;

using HLI.Data.Clients;
using HLI.Data.Tests.Models;

using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace HLI.Data.Tests
{
    /// <summary>
    ///     Tests <see cref="HliHttpClient" /> using http://httpbin.org/
    /// </summary>
    [TestFixture]
    public class HttpClientTest
    {
        #region Constants

        private const string apiKey = "AIzaSyCiejZ8wxQtHIKREo_lnowlJQl7TZJrFj8";

        private const string coordinates = "59.27,18.13";

        #endregion

        #region Public Methods and Operators

        [Test]
        public async Task GetDataAsTypeAsync_GoogleLocation_ReturnsLocations()
        {
            // Arrange
            var apiRoute = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={coordinates}&radius=500&key={apiKey}";
            var client = new HliHttpClient(apiRoute);

            // Act
            var result = await client.GetDataAsTypeAsync<GoogleLocation>();

            // Assert
            Assert.IsTrue(result.results.Any());
        }

        [Test]
        public async Task GetDataAsTypeAsync_HttpBinuserAgent_ReturnsUserAgent()
        {
            // Arrange
            var client = new HliHttpClient("http://httpbin.org/", "ip");

            // Act
            var result = await client.GetDataAsTypeAsync<IpResponse>();

            // Assert
            Assert.IsTrue(result.Origin.Length > 0);
        }

        [Test]
        public async Task PostDataAsTypeAsync_HttpBinPost_ReturnsHeaders()
        {
            // Arrange
            var client = new HliHttpClient("http://httpbin.org/", "post");

            // Act
            var result = await client.PostDataAsTypeAsync<HttpBinPost>(null);

            // Assert
            Assert.AreEqual("httpbin.org", result.headers.Host);
        }

        #endregion
    }
}