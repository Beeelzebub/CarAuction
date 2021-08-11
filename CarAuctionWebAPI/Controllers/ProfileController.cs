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
        private readonly CarAuctionContext _carAuctionContext;
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IMapper mapper, CarAuctionContext carAuctionContext, IProfileRepository profileRepository)
        {
            _mapper = mapper;
            _carAuctionContext = carAuctionContext;
            _profileRepository = profileRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] CarDtoForCreation carDtoForCreation)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            _profileRepository.AddCar(carDtoForCreation, currentUserId);
            await _carAuctionContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("MyCars")]
        public async Task<IActionResult> GetCarsForUser()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cars =await _profileRepository.GetCarsProfileAsync(currentUserId);
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);
            return Ok(returnData);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _profileRepository.GetCarAsync(id);
            if (car == null)
            {
                return BadRequest();
            }

            var lot = await _profileRepository.GetLotAsync(car.LotId);
            if (lot == null)
            {
                return BadRequest();
            }
            _carAuctionContext.Cars.Remove(car);
            _carAuctionContext.Lots.Remove(lot);
            await _carAuctionContext.SaveChangesAsync();
            return Ok();
        }
    }
}
