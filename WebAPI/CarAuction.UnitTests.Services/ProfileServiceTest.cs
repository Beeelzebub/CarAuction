using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CarAuction.UnitTests.Shared;
using CarAuctionWebAPI;
using DTO;
using DTO.Response;
using Entity.Models;
using Enums;
using Microsoft.AspNetCore.Identity;
using Xunit;
using Moq;
using Repositories;
using Services.Profile;

namespace CarAuction.UnitTests.Services
{
    public class ProfileServiceTest
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly IProfileService _profileService;

        public ProfileServiceTest()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            var userStoreMock = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mappingConfig.CreateMapper();

            _profileService = new ProfileService(_mockRepositoryManager.Object, _mockUserManager.Object, mapper);
        }

        [Fact]
        public async Task GetUsersCarInfoAsyncTest()
        {
            var lotId = 1;
            var userClaims = new ClaimsPrincipal();
            _mockRepositoryManager.Setup(repo => repo.Lot.GetAsync(lotId))
                .Returns(Task.FromResult(TestData.GetTestLot()));
            _mockUserManager.Setup(mg => mg.GetUserId(userClaims))
                .Returns(TestData.TestUserId);

            var result = await _profileService.GetUsersCarInfoAsync(lotId, userClaims);

            Assert.Equal(ErrorCode.Success, result.ErrorCode);
            Assert.IsType<Response<CarDto>>(result);
        }

        [Fact]
        public async Task RemoveLotAsyncTest()
        {
            var lotId = 1;
            var userClaims = new ClaimsPrincipal();
            _mockRepositoryManager.Setup(repo => repo.Lot.GetAsync(lotId))
                .Returns(Task.FromResult(TestData.GetTestLot()));
            _mockRepositoryManager.Setup(repo => repo.SaveAsync())
                .Returns(Task.CompletedTask);
            _mockUserManager.Setup(mg => mg.GetUserId(userClaims))
                .Returns(TestData.TestUserId);
            
            var result = await _profileService.RemoveLotAsync(lotId, userClaims);

            Assert.Equal(ErrorCode.Success, result.ErrorCode);
        }

        [Fact]
        public async Task GetUsersCarsAsyncTest()
        {
            var userClaims = new ClaimsPrincipal();
            _mockRepositoryManager.Setup(repo => repo.Car.GetListByParametersAsync(TestData.TestUserId, null))
                .Returns(Task.FromResult(TestData.GetTestCarsList()));
            _mockUserManager.Setup(mg => mg.GetUserId(userClaims))
                .Returns(TestData.TestUserId);

            var result = await _profileService.GetUsersCarsAsync(null, userClaims);

            Assert.Equal(ErrorCode.Success, result.ErrorCode);
            Assert.IsType<Response<List<CarDto>>>(result);
        }

        [Fact]
        public async Task GetUsersBidsAsyncTest()
        {
            var userClaims = new ClaimsPrincipal(); 
            _mockRepositoryManager.Setup(repo => repo.Bid.GetBidsByUserAsync(TestData.TestUserId))
                .Returns(Task.FromResult(TestData.GetTestBidsList() as List<Bid>));
            _mockUserManager.Setup(mg => mg.GetUserId(userClaims))
                .Returns(TestData.TestUserId);

            var result = await _profileService.GetUsersBidsAsync(userClaims);

            Assert.Equal(ErrorCode.Success, result.ErrorCode);
            Assert.IsType<Response<List<BidsDto>>>(result);
        }
    }
}
