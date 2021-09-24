using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CarAuctionWebAPI;
using DTO;
using DTO.Response;
using Entity;
using Entity.Models;
using Enums;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using Xunit;

namespace CarAuction.IntegrationTests.Services.ControllersTest
{

    public class Des
    {
        public ErrorCode ErrorCode { get; set; }
        public List<Car> Car { get; set; }
    }
    public class CarsControllerTest: IClassFixture<TestStartup<Startup>>
    {
        private readonly HttpClient _client;
        public CarsControllerTest(TestStartup<Startup> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task GetCars_ActionExecutes_ReturnErrorCode()
        {
            var response = await _client.GetAsync("api/Cars");
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Des>(result);
            Assert.Equal(ErrorCode.Success, data.ErrorCode);
        }
        [Fact]
        public async Task GetCar_ActionExecutes_ReturnErrorCode()
        {
            var response = await _client.GetAsync("api/Cars/1");
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Des>(result);
            Assert.Equal(ErrorCode.CarNotFound, data.ErrorCode);
        }

        [Fact]
        public async Task Bid_ActionExecutes_ReturnErrorCode()
        {
            var response = await _client.GetAsync("api/Cars/1");
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Des>(result);
            Assert.Equal(ErrorCode.CarNotFound, data.ErrorCode);
        }
    }
}
