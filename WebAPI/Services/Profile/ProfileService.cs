using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Entity.Models;
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
            var userId = _userManager.GetUserId(sellerClaims);

            var lot = await _repositoryManager.Lot.GetAsync(lotId);

            if (lot == null)
            {
                throw new BadRequestException($"Lot with id {lotId} is not found");
            }

            if (lot.SellerId != userId)
            {
                throw new ForbiddenException("You have no permissions to delete someone else's lot");
            }

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
        }
    }
}
