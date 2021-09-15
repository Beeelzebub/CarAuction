using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using DTO.Response;
using Entity;
using Entity.Models;
using Entity.RequestFeatures;
using Enums;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Repositories;
using Services.Exceptions;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Auction
{
    public class AuctionService : IAuctionService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;

        public AuctionService(IMapper mapper, IRepositoryManager repositoryManager, UserManager<User> userManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _userManager = userManager;
        }

        public async Task<BaseResponse> BidAsync(int lotId, ClaimsPrincipal bidderClaims)
        {
            var bidderId = _userManager.GetUserId(bidderClaims);

            var lot = await _repositoryManager.Lot.GetAsync(lotId);

            if (lot == null || lot.Status != LotStatus.Approved)
            {
                return BaseResponse.Fail(ErrorCode.LotNotFoundError);
            }

            var activeBid = _repositoryManager.Bid.GetActiveBid(lotId);

            if (activeBid != null)
            {
                if (activeBid.BuyerId == bidderId)
                {
                    return BaseResponse.Fail(ErrorCode.AlreadyPlacedBetError);
                }

                activeBid.BidStatus = BidStatus.Outbid;
            }

            lot.CurrentCost += lot.MinimalStep;

            var newBid = new Bid
            {
                LotId = lot.Id,
                BuyerId = bidderId
            };

            await _repositoryManager.Bid.CreateAsync(newBid);

            await _repositoryManager.SaveAsync();

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> GetCarsAsync(CarParameters carParameters)
        {
            var cars = await _repositoryManager.Car.GetListCarsAsync(carParameters);

            var carDtoList = _mapper.Map<List<CarDto>>(cars);

            return BaseResponse.Success(carDtoList);
        }

        public async Task<BaseResponse> GetCarAsync(int carId)
        {
            var car = await _repositoryManager.Car.GetCarAsync(carId);

            if (car == null)
            {
                return BaseResponse.Fail(ErrorCode.CarNotFound);
            }

            var carDto = _mapper.Map<GetOneCarDto>(car);

            return BaseResponse.Success(carDto);
        }

        public async Task<BaseResponse> GetModelsWithBrands()
        {
            var models = await _repositoryManager.GetRepositoryByEntity<Model>().GetListAsync();
            var brands = await _repositoryManager.GetRepositoryByEntity<Brand>().GetListAsync();

            var returnModels = models.Select(n => n.Name);
            var returnBrands = brands.Select(n => n.BrandName);
            var returnData = new BrandModelDto { BrandNames = returnBrands, ModelNames = returnModels };
            return BaseResponse.Success(returnData);
        }
    }
}
