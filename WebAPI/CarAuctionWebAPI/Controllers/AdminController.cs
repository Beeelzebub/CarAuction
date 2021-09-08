using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarAuctionWebAPI.Filters;
using DTO;
using Entity.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Repositories;
using Services.Auction;
using Swashbuckle.AspNetCore.Annotations;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        private readonly IAuctionService _auctionService;

        public AdminController(IMapper mapper, IRepositoryManager repository, IAuctionService auctionService)
        {
            _mapper = mapper;
            _repository = repository;
            _auctionService = auctionService;
        }

        [HttpGet("cars")]
        [SwaggerOperation(Summary = "Get all the user's cars that have the status Pending")]
        [SwaggerResponse(200, "Get all cars ")]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _repository.Car.GetCarsByStatusAsync(LotStatus.Pending);

            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);

            return Ok(returnData);
        }

        [HttpGet("cars/{id}")]
        [SwaggerOperation(Summary = "Get one the user's car that have the status Pending")]
        [SwaggerResponse(400, "If car not found")]
        [SwaggerResponse(200, "Get one car")]
        [ServiceFilter(typeof(ValidationFilterAttribute<Car>))]
        public IActionResult GetCar(int id)
        {
            var car = HttpContext.Items["entity"] as Car;

            var returnData = _mapper.Map<CarDtoForGet>(car);

            return Ok(returnData);
        }

        [HttpPut("cars/{lotId}")]
        [SwaggerOperation(Summary = "Change status car")]
        [SwaggerResponse(400, "If car not found")]
        [SwaggerResponse(200, "Change lot status")]
        public async Task<IActionResult> ChangeLotStatus(int lotId, [FromBody] LotDtoForChangeStatus lotStatusDto)
        {
            await _auctionService.ChangeLotStatus(lotId, lotStatusDto.Status);

            return Ok();
        }
    }
}
