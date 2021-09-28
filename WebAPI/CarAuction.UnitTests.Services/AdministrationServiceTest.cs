using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarAuction.UnitTests.Shared;
using CarAuctionWebAPI;
using DTO;
using DTO.Response;
using Enums;
using Moq;
using Repositories;
using Services.Administration;
using Xunit;

namespace CarAuction.UnitTests.Services
{
    public class AdministrationServiceTest
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly IAdministrationService _administrationService;

        public AdministrationServiceTest()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            _administrationService = new AdministrationService(_repositoryManagerMock.Object, mapper);
        }

        [Fact]
        public async Task GetPendingCarsAsync_ActionExecutes_ReturnListCars()
        {
            _repositoryManagerMock.Setup(repo => repo.Car.GetCarsByStatusAsync(LotStatus.Pending))
                .Returns(Task.FromResult(TestData.GetTestCarsList()));
            var result = await _administrationService.GetPendingCarsAsync();
            Assert.IsType<Response<List<CarDto>>>(result);
        }

        [Fact]
        public async Task GetPendingCarAsync_ActionExecutes_ReturnCar()
        {
            var id = 1;
            _repositoryManagerMock.Setup(repo => repo.Car.GetAsync(id))
                .Returns(Task.FromResult(TestData.GetTestCar()));
            
            var result = await _administrationService.GetPendingCarAsync(id);
            
            Assert.Equal(ErrorCode.Success, result.ErrorCode);
            Assert.IsType<Response<CarDto>>(result);
        }

    }
}
