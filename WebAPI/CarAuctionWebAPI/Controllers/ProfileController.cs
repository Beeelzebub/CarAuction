using System.Collections.Generic;
using DTO;
using Entity.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Profile;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using CarAuctionWebAPI.Extensions;
using DTO.Response;

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
        [SwaggerResponse(201, "Lot has been added", typeof(Response))]
        public async Task<IActionResult> AddLot([FromForm] LotCreationDto lotCreationDto)
        {
            var result = await _profileService.AddLotAsync(lotCreationDto, User);

            return this.Answer(result, StatusCode(201, result));
        }

        [HttpGet("MyCars")]
        [SwaggerOperation(Summary = "Show all user's cars")]
        [SwaggerResponse(200, "Get user's cars", typeof(Response<List<CarDto>>))]
        public async Task<IActionResult> GetCarsForUser([FromQuery] CarsParametersInProfile carsParametersInProfile)
        {
            var result = await _profileService.GetUsersCarsAsync(carsParametersInProfile, User);

            return this.Answer(result, Ok(result));
        }

        [HttpDelete("MyCars/{id}")]
        [SwaggerOperation(Summary = "Delete user's lot")]
        [SwaggerResponse(200, "Lot has been deleted", typeof(Response))]
        [SwaggerResponse(400, "Lot not found", typeof(Response))]
        public async Task<IActionResult> RemoveLot(int id)
        {
            var result = await _profileService.RemoveLotAsync(id, User);

            return this.Answer(result, Ok(result));
        }

        [HttpGet("MyCars/{id}")]
        [SwaggerOperation(Summary = "Show one user's cars")]
        [SwaggerResponse(200, "Get user one car", typeof(Response<CarDto>))]
        [SwaggerResponse(400, "Car not found", typeof(Response))]
        public async Task<IActionResult> GetUsersCarInfo(int id)
        {
            var result = await _profileService.GetUsersCarInfoAsync(id, User);

            return this.Answer(result, Ok(result));
        }

        [HttpGet("MyBids")]
        [SwaggerOperation(Summary = "Show last user's bids")]
        [SwaggerResponse(200, "Get user's bids", typeof(Response<List<BidsDto>>))]
        public async Task<IActionResult> GetUsersBids()
        {
            var result = await _profileService.GetUsersBidsAsync(User);
            
            return this.Answer(result, Ok(result));
        }
    }
}
