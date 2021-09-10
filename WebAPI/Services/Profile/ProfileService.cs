using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Entity.Models;
using Entity.RequestFeatures;
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
        
        public async Task RemoveLotAsync(int lotId, ClaimsPrincipal sellerClaims)
        {
            var lot = await GetLotAsync(lotId, sellerClaims);

            _repositoryManager.Lot.Delete(lot);

            await _repositoryManager.SaveAsync();
        }

        public async Task AddLotAsync(LotCreationDto lotCreationDto, ClaimsPrincipal sellerClaims)
        {
            var car = _mapper.Map<Car>(lotCreationDto);

            var lot = _mapper.Map<Lot>(lotCreationDto);

            lot.SellerId = _userManager.GetUserId(sellerClaims);
            lot.Car = car;

            await _repositoryManager.Lot.CreateAsync(lot);

            await _repositoryManager.SaveAsync();
        }

        public async Task<CarDto> GetUsersCarInfoAsync(int lotId, ClaimsPrincipal userClaims)
        {
            var lot = await GetLotAsync(lotId, userClaims);
            
            return _mapper.Map<CarDto>(lot.Car);
        }

        public async Task<List<BidsDto>> GetUsersBidsAsync(ClaimsPrincipal userClaims)
        {
            var userId = _userManager.GetUserId(userClaims);

            var bids = await _repositoryManager.Bid.GetBidsByUserAsync(userId);

            return _mapper.Map<List<BidsDto>>(bids);
        }

        public async Task<List<CarDto>> GetUsersCarsAsync(CarsParametersInProfile carsParametersInProfile, ClaimsPrincipal userClaims)
        {
            var userId = _userManager.GetUserId(userClaims);

            var cars = await _repositoryManager.Car.GetListByParametersAsync(userId, carsParametersInProfile);

            return _mapper.Map<List<CarDto>>(cars);
        }

        private async Task<Lot> GetLotAsync(int lotId, ClaimsPrincipal userClaims)
        {
            var userId = _userManager.GetUserId(userClaims);

            var lot = await _repositoryManager.Lot.GetAsync(lotId);

            if (lot == null)
            {
                throw new BadRequestException($"Lot with id {lotId} is not found");
            }

            if (lot.SellerId != userId)
            {
                throw new ForbiddenException("You have no permissions");
            }

            return lot;
        }
    }
}
