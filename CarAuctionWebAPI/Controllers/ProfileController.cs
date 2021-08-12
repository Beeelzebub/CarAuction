using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Entity;
using System.Security.Claims;
using Contracts;
using Entity.DTO;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IMapper mapper, IProfileRepository profileRepository)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] CarDtoForCreation carDtoForCreation)
        {
            ClaimsPrincipal currentUser = User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            _profileRepository.AddCar(carDtoForCreation, currentUserId);
            _profileRepository.Save();
            return StatusCode(201);
        }
        
        [HttpGet("MyCars")]
        public async Task<IActionResult> GetCarsForUser()
        {
            ClaimsPrincipal currentUser = User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cars =await _profileRepository.GetCarsProfileAsync(currentUserId);
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);
            return Ok(returnData);
        }
        [HttpDelete("MyCars/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            ClaimsPrincipal currentUser = User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var car = await _profileRepository.GetCarAsync(id, currentUserId);
            if (car == null)
            {
                return BadRequest();
            }

            var lot = await _profileRepository.GetLotAsync(car.LotId);
            if (lot == null)
            {
                return BadRequest();
            }

            _profileRepository.DeleteLotWithCar(car, lot);
            _profileRepository.Save();
            return Ok();
        }

        [HttpGet("MyCars/{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            ClaimsPrincipal currentUser = User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var car = await _profileRepository.GetCarAsync(id, currentUserId);
            var returnData = _mapper.Map<CarDtoForGet>(car);
            return Ok(returnData);
        }
    }
}
