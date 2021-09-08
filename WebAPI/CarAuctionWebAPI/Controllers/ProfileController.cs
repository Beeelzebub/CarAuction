using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DTO;
using Entity.Models;
using Entity.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Repositories;
using Services.Profile;
using Swashbuckle.AspNetCore.Annotations;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService, IMapper mapper, IRepositoryManager repository, UserManager<User> userManager)
        {
            _profileService = profileService;
            _mapper = mapper;
            _repository = repository;
            _userManager = userManager;
        }

        [HttpPost("AddLot")]
        [SwaggerOperation(Summary = "Adding a lot")]
        [SwaggerResponse(201, "Lot has been added")]
        public async Task<IActionResult> AddLot([FromBody] LotCreationDto lotCreationDto)
        {
            await _profileService.AddLotAsync(lotCreationDto, User);

            return StatusCode(201);
        }
        
        [HttpGet("MyCars")]
        [SwaggerOperation(Summary = "Show all user's cars")]
        [SwaggerResponse(200, "Get user's cars")]
        public async Task<IActionResult> GetCarsForUser([FromQuery] CarsParametersInProfile carsParametersInProfile)
        {
            var currentUserId = _userManager.GetUserId(User);
            var cars = await _repository.Car.GetListCarsProfileAsync(currentUserId, carsParametersInProfile);
            var returnData = _mapper.Map<IEnumerable<CarDto>>(cars);
            return Ok(returnData);
        }

        [HttpDelete("MyCars/{lotId}")]
        [SwaggerOperation(Summary = "Delete user's lot")]
        [SwaggerResponse(400, "Lot not found")]
        [SwaggerResponse(200, "Lot has been deleted")]
        public async Task<IActionResult> RemoveLot(int lotId)
        {
            await _profileService.RemoveLotAsync(lotId, User);

            return Ok();
        }

        [HttpGet("MyCars/{id}")]
        [SwaggerOperation(Summary = "Show one user's cars")]
        [SwaggerResponse(400, "Car not found")]
        [SwaggerResponse(200, "Get user one car")]
        public async Task<IActionResult> GetCar(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            var car = await _repository.Car.GetCarByUserAsync(id, currentUserId);

            if (car == null)
            {
                return BadRequest("Car is not found");
            }

            var returnData = _mapper.Map<CarDto>(car);

            return Ok(returnData);
        }

        [HttpGet("MyBids")]
        [SwaggerOperation(Summary = "Show last user's bids")]
        [SwaggerResponse(200, "Get user's bids")]
        public async Task<IActionResult> GetUsersBids()
        {
            var currentUserId = _userManager.GetUserId(User);
            var bids = await _repository.Bid.GetBidsByUserAsync(currentUserId);
            var returnData = _mapper.Map<IEnumerable<GetBidsDto>>(bids);

            return Ok(returnData);
        }
    }
}
