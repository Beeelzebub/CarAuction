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
        [HttpPost]
        public IActionResult AddCar([FromBody] CarDtoForCreation carDtoForCreation)
        {
            var currentUserId = _userManager.GetUserId(User);
            _profileRepository.AddCar(carDtoForCreation, currentUserId);
            _profileRepository.Save();
            return StatusCode(201);
        }
        
        [HttpGet("MyCars")]
        public async Task<IActionResult> GetCarsForUser()
        {
            var currentUserId = _userManager.GetUserId(User);
            var cars =await _profileRepository.GetCarsProfileAsync(currentUserId);
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);
            return Ok(returnData);
        }
        [HttpDelete("MyCars/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
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
            var currentUserId = _userManager.GetUserId(User);
            var car = await _profileRepository.GetCarAsync(id, currentUserId);
            var returnData = _mapper.Map<CarDtoForGet>(car);
            return Ok(returnData);
        }
    }
}
