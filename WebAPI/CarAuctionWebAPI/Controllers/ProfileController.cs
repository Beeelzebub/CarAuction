using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DTO;
using Entity.Models;
using Entity.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Adding a car")]
        [SwaggerResponse(201, "Car is added")]
        public async Task<IActionResult> AddCar([FromBody] CarDtoForCreation carDtoForCreation)
        {
            var currentUserId = _userManager.GetUserId(User);

            _profileRepository.AddCar(carDtoForCreation, currentUserId);
            await _profileRepository.SaveAsync();

            return StatusCode(201);
        }
        
        [HttpGet("MyCars")]
        [SwaggerOperation(Summary = "Show all user's cars")]
        [SwaggerResponse(400, "Cars not found")]
        [SwaggerResponse(200, "Get user's cars")]
        public async Task<IActionResult> GetCarsForUser([FromQuery] CarsParametersInProfile carsParametersInProfile)
        {
            var currentUserId = _userManager.GetUserId(User);
            var cars =await _profileRepository.GetCarsProfileAsync(currentUserId, carsParametersInProfile);

            if (cars == null)
            {
                return BadRequest("Cars are not found");
            }

            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);

            return Ok(returnData);
        }

        [HttpDelete("MyCars/{id}")]
        [SwaggerOperation(Summary = "Delete user car")]
        [SwaggerResponse(400, "Car not found")]
        [SwaggerResponse(400, "Lot not found")]
        [SwaggerResponse(200, "Delete car")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            var car = await _profileRepository.GetCarByUserAsync(id, currentUserId);

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
            await _profileRepository.SaveAsync();

            return Ok();
        }

        [HttpGet("MyCars/{id}")]
        [SwaggerOperation(Summary = "Show one user's cars")]
        [SwaggerResponse(400, "Car not found")]
        [SwaggerResponse(200, "Get user one car")]
        public async Task<IActionResult> GetCar(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            var car = await _profileRepository.GetCarByUserAsync(id, currentUserId);

            if (car == null)
            {
                return BadRequest("Car is not found");
            }

            var returnData = _mapper.Map<CarDtoForGet>(car);

            return Ok(returnData);
        }

        [HttpGet("MyBids")]
        [SwaggerOperation(Summary = "Show last user's bids on cars")]
        [SwaggerResponse(200, "Get user bids")]
        public async Task<IActionResult> GetUsersBids()
        {
            var currentUserId = _userManager.GetUserId(User);
            var bids = await _profileRepository.GetBidsByUserAsync(currentUserId);
            var returnData = _mapper.Map<IEnumerable<BidsDtoForGet>>(bids);

            return Ok(returnData);
        }
    }
}
