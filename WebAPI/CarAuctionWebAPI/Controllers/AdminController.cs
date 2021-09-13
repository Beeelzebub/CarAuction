using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarAuctionWebAPI.Extensions;
using CarAuctionWebAPI.Filters;
using DTO;
using Entity.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Repositories;
using Services.Auction;
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
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        private readonly IAdministrationService _administrationService;

        public AdminController(IMapper mapper, IRepositoryManager repository, IAdministrationService administrationService)
        {
            _mapper = mapper;
            _repository = repository;
            _administrationService = administrationService;
        }

        [HttpGet("cars")]
        [SwaggerOperation(Summary = "Get all the user's cars that have the status Pending")]
        [SwaggerResponse(200, "Get all cars ")]
        public async Task<IActionResult> GetCars()
        {
            var result = await _administrationService.GetPendingCarsAsync();

            return this.Answer(result, Ok(result));
        }

        [HttpGet("cars/{id}")]
        [SwaggerOperation(Summary = "Get one the user's car that have the status Pending")]
        [SwaggerResponse(400, "If car not found")]
        [SwaggerResponse(200, "Get one car")]
        public async Task<IActionResult> GetCar(int id)
        {
            var result = await _administrationService.GetPendingCarAsync(id);

            return this.Answer(result, Ok(result));
        }

        [HttpPatch("cars/{lotId}")]
        [SwaggerOperation(Summary = "Change status car")]
        [SwaggerResponse(400, "If car not found")]
        [SwaggerResponse(200, "Change lot status")]
        public async Task<IActionResult> ChangeLotStatus(int lotId, [FromBody] JsonPatchDocument<Lot> patchDoc)
        {
            var result = await _administrationService.ChangeLotStatusAsync(lotId, patchDoc);

            return this.Answer(result, Ok(result));
        }
    }
}
