using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Entity;
using System.Security.Claims;
using Contracts;
using Entity.DTO;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;
        private readonly UserManager<User> _userManager;

        public ProfileController(IMapper mapper, IProfileRepository profileRepository, UserManager<User> userManager)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
            _userManager = userManager;
        }
        [HttpPost("AddCar")]
        public IActionResult AddCar([FromBody] CarDtoForCreation carDtoForCreation)
        {
            var currentUserId = _userManager.GetUserId(User);
            _profileRepository.AddCar(carDtoForCreation, currentUserId);
            _profileRepository.Save();
            return StatusCode(201);
        }
        
        [HttpGet("MyCars/Pending")]
        public async Task<IActionResult> GetCarsForUserIsPending()
        {
            var currentUserId = _userManager.GetUserId(User);
            var cars =await _profileRepository.GetCarsProfileIsPendingAsync(currentUserId);
            if (cars == null)
            {
                return BadRequest("Cars not found");
            }
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);
            return Ok(returnData);
        }
        [HttpGet("MyCars/Approved")]
        public async Task<IActionResult> GetCarsForUserIsApproved()
        {
            var currentUserId = _userManager.GetUserId(User);
            var cars = await _profileRepository.GetCarsProfileIsApprovedAsync(currentUserId);
            if (cars == null)
            {
                return BadRequest("Cars not found");
            }
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);
            return Ok(returnData);
        }
        [HttpDelete("MyCars/Pending/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            var car = await _profileRepository.GetCarIsPendingAsync(id, currentUserId);
            if (car == null)
            {
                return BadRequest("Car not found");
            }

            var lot = await _profileRepository.GetLotAsync(car.LotId);
            if (lot == null)
            {
                return BadRequest("Lot not found");
            }

            _profileRepository.DeleteLotWithCar(car, lot);
            _profileRepository.Save();
            return Ok();
        }

        [HttpGet("MyCars/Pending/{id}")]
        public async Task<IActionResult> GetCarIsPending(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            var car = await _profileRepository.GetCarIsPendingAsync(id, currentUserId);
            if (car == null)
            {
                return BadRequest("Car not found");
            }
            var returnData = _mapper.Map<CarDtoForGet>(car);
            return Ok(returnData);
        }
        [HttpGet("MyCars/Approved/{id}")]
        public async Task<IActionResult> GetCarIsApproved(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            var car = await _profileRepository.GetCarIsApprovedAsync(id, currentUserId);
            if (car == null)
            {
                return BadRequest("Car not found");
            }
            var returnData = _mapper.Map<CarDtoForGet>(car);
            return Ok(returnData);
        }

        [HttpGet("MyBids")]
        public async Task<IActionResult> GetBidsUser()
        {
            var currentUserId = _userManager.GetUserId(User);
            var bids = await _profileRepository.UserBidsAsync(currentUserId);
            var returnData = _mapper.Map<IEnumerable<BidsDtoForGet>>(bids);
            return Ok(returnData);
        }
    }
}
