using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CarAuctionWebAPI;
using DTO;
using DTO.Response;
using Entity;
using Entity.Models;
using Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using Xunit;

namespace CarAuction.IntegrationTests.Services.ControllersTest
{

    public class GetListCars
    {
        public ErrorCode ErrorCode { get; set; }
        public List<CarDto> Data { get; set; }

    }
    public class GetCar
    {
        public ErrorCode ErrorCode { get; set; }
        public CarDto Data { get; set; }

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
            var data = JsonConvert.DeserializeObject<GetListCars>(result);
            Assert.Equal(ErrorCode.Success, data.ErrorCode);
        }
        


        [Fact]
        public async Task GetCar_ActionExecutes_ReturnCar()
        {
            var response = await _client.GetAsync("api/Cars/1");
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<GetCar>(result);
            Assert.Equal(ErrorCode.Success, data.ErrorCode);
        }
    }
}
