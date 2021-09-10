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
            await _profileService.AddLotAsync(lotCreationDto, User);

            return StatusCode(201);
        }
        
        [HttpGet("MyCars")]
        [SwaggerOperation(Summary = "Show all user's cars")]
        [SwaggerResponse(200, "Get user's cars")]
        public async Task<IActionResult> GetCarsForUser([FromQuery] CarsParametersInProfile carsParametersInProfile)
        {
            var returnData = await _profileService.GetUsersCarsAsync(carsParametersInProfile, User);

            return Ok(returnData);
        }

        [HttpDelete("MyCars/{id}")]
        [SwaggerOperation(Summary = "Delete user's lot")]
        [SwaggerResponse(400, "Lot not found")]
        [SwaggerResponse(200, "Lot has been deleted")]
        public async Task<IActionResult> RemoveLot(int id)
        {
            await _profileService.RemoveLotAsync(id, User);

            return Ok();
        }

        [HttpGet("MyCars/{id}")]
        [SwaggerOperation(Summary = "Show one user's cars")]
        [SwaggerResponse(400, "Car not found")]
        [SwaggerResponse(200, "Get user one car")]
        public async Task<IActionResult> GetUsersCarInfo(int id)
        {
            var returnData = await _profileService.GetUsersCarInfoAsync(id, User);

            return Ok(returnData);
        }

        [HttpGet("MyBids")]
        [SwaggerOperation(Summary = "Show last user's bids")]
        [SwaggerResponse(200, "Get user's bids")]
        public async Task<IActionResult> GetUsersBids()
        {
            var returnData = await _profileService.GetUsersBidsAsync(User);

            return Ok(returnData);
        }
    }
}
