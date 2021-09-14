using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarAuctionWebAPI.Extensions;
using DTO;
using DTO.Response;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.JsonPatch;
using Services.Administration;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdministrationService _administrationService;

        public AdminController(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        [HttpGet("cars")]
        [SwaggerOperation(Summary = "Get all cars that have the status Pending")]
        [SwaggerResponse(200, "Get all cars ", typeof(Response<List<CarDto>>))]
        public async Task<IActionResult> GetCars()
        {
            var result = await _administrationService.GetPendingCarsAsync();

            return this.Answer(result, Ok(result));
        }

        [HttpGet("cars/{id}")]
        [SwaggerOperation(Summary = "Get one car that have the status Pending")]
        [SwaggerResponse(200, "Get one car", typeof(Response<CarDto>))]
        [SwaggerResponse(400, "If car not found", typeof(Response))]
        public async Task<IActionResult> GetCar(int id)
        {
            var result = await _administrationService.GetPendingCarAsync(id);

            return this.Answer(result, Ok(result));
        }

        [HttpPatch("cars/{lotId}")]
        [SwaggerOperation(Summary = "Change car status")]
        [SwaggerResponse(200, "Change lot status", typeof(Response))]
        [SwaggerResponse(400, "If car not found", typeof(Response))]
        public async Task<IActionResult> ChangeLotStatus(int lotId, [FromBody] JsonPatchDocument<Lot> patchDoc)
        {
            var result = await _administrationService.ChangeLotStatusAsync(lotId, patchDoc);

            return this.Answer(result, Ok(result));
        }
    }
}
