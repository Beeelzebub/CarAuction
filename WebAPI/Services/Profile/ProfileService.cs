using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using DTO.Response;
using Entity.Models;
using Entity.RequestFeatures;
using Enums;
using Microsoft.AspNetCore.Identity;
using Repositories;
using Services.Exceptions;

namespace Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ProfileService(IRepositoryManager repositoryManager, UserManager<User> userManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        
        public async Task<BaseResponse> RemoveLotAsync(int lotId, ClaimsPrincipal sellerClaims)
        {
            var lot = await GetLotAsync(lotId);

            var checkResult = LotCheck(lot, sellerClaims);

            if (checkResult != ErrorCode.Success)
            {
                return BaseResponse.Fail(checkResult);
            }

            _repositoryManager.Lot.Delete(lot);

            await _repositoryManager.SaveAsync();

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> AddLotAsync(LotCreationDto lotCreationDto, ClaimsPrincipal sellerClaims)
        {
            var car = _mapper.Map<Car>(lotCreationDto);

            var lot = _mapper.Map<Lot>(lotCreationDto);
            var model = _mapper.Map<Model>(lotCreationDto); ;
            var brand = _mapper.Map<Brand>(lotCreationDto); ;

            lot.SellerId = _userManager.GetUserId(sellerClaims);
            lot.Car = car;
            lot.Car.Model = model;
            lot.Car.Model.Brand = brand;

            await _repositoryManager.Lot.CreateAsync(lot);

            await _repositoryManager.SaveAsync();

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> GetUsersCarInfoAsync(int lotId, ClaimsPrincipal userClaims)
        {
            var lot = await GetLotAsync(lotId);

            var checkResult = LotCheck(lot, userClaims);

            if (checkResult != ErrorCode.Success)
            {
                return BaseResponse.Fail(checkResult);
            }

            var carDto = _mapper.Map<CarDto>(lot.Car);

            return BaseResponse.Success(carDto);
        }

        public async Task<BaseResponse> GetUsersBidsAsync(ClaimsPrincipal userClaims)
        {
            var userId = _userManager.GetUserId(userClaims);

            var bids = await _repositoryManager.Bid.GetBidsByUserAsync(userId);

            var bidsDto = _mapper.Map<List<BidsDto>>(bids);

            return BaseResponse.Success(bidsDto);
        }

        public async Task<BaseResponse> GetUsersCarsAsync(CarsParametersInProfile carsParametersInProfile, ClaimsPrincipal userClaims)
        {
            var userId = _userManager.GetUserId(userClaims);

            var cars = await _repositoryManager.Car.GetListByParametersAsync(userId, carsParametersInProfile);

            var carDtoList = _mapper.Map<List<CarDto>>(cars);

            return BaseResponse.Success(carDtoList);
        }

        private async Task<Lot> GetLotAsync(int lotId) => 
            await _repositoryManager.Lot.GetAsync(lotId);
            

        private ErrorCode LotCheck(Lot lot, ClaimsPrincipal userClaims)
        {
            var userId = _userManager.GetUserId(userClaims);

            if (lot == null)
            {
                return ErrorCode.LotNotFoundError;
            }

            if (lot.SellerId != userId)
            {
                return ErrorCode.NoPermissionsError;
            }

            return ErrorCode.Success;
        }
    }
}
