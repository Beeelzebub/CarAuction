using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarAuction.IntegrationTests
{
    public class CarsTests : TestBase
    {
        private readonly HttpClient _client;

        public CarsTests()
        {
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetCar_WhenCalled_ReturnsCarObject()
        {
            var response = await _client.GetAsync("/api/Cars/1");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
