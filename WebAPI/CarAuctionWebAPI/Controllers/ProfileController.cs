using DTO;
using Entity.RequestFeatures;
using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Profile;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using CarAuctionWebAPI.Extensions;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost("AddLot")]
        [SwaggerOperation(Summary = "Adding a lot")]
        [SwaggerResponse(201, "Lot has been added")]
        public async Task<IActionResult> AddLot([FromBody] LotCreationDto lotCreationDto)
        {
            var result = await _profileService.AddLotAsync(lotCreationDto, User);

            return this.Answer(result, StatusCode(201, result));
        }

        [HttpGet("MyCars")]
        [SwaggerOperation(Summary = "Show all user's cars")]
        [SwaggerResponse(200, "Get user's cars")]
        public async Task<IActionResult> GetCarsForUser([FromQuery] CarsParametersInProfile carsParametersInProfile)
        {
            var result = await _profileService.GetUsersCarsAsync(carsParametersInProfile, User);

            return this.Answer(result, Ok(result));
        }

        [HttpDelete("MyCars/{id}")]
        [SwaggerOperation(Summary = "Delete user's lot")]
        [SwaggerResponse(400, "Lot not found")]
        [SwaggerResponse(200, "Lot has been deleted")]
        public async Task<IActionResult> RemoveLot(int id)
        {
            var result = await _profileService.RemoveLotAsync(id, User);

            return this.Answer(result, Ok(result));
        }

        [HttpGet("MyCars/{id}")]
        [SwaggerOperation(Summary = "Show one user's cars")]
        [SwaggerResponse(400, "Car not found")]
        [SwaggerResponse(200, "Get user one car")]
        public async Task<IActionResult> GetUsersCarInfo(int id)
        {
            var result = await _profileService.GetUsersCarInfoAsync(id, User);

            return this.Answer(result, Ok(result));
        }

        [HttpGet("MyBids")]
        [SwaggerOperation(Summary = "Show last user's bids")]
        [SwaggerResponse(200, "Get user's bids")]
        public async Task<IActionResult> GetUsersBids()
        {
            var result = await _profileService.GetUsersBidsAsync(User);
            
            return this.Answer(result, Ok(result));
        }
    }
}
