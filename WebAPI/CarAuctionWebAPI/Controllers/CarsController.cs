using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DTO;
using Entity.Models;
using Entity.RequestFeatures;
using System.Threading.Tasks;
using CarAuctionWebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Repositories;
using Services.Auction;
using Swashbuckle.AspNetCore.Annotations;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        private readonly IAuctionService _auctionService;

        public CarsController(IMapper mapper, IRepositoryManager repository, IAuctionService auctionService) 
        {
            _mapper = mapper;
            _repository = repository;
            _auctionService = auctionService;
        }
        

        [HttpGet]
        [SwaggerOperation(Summary = "Get all cars")]
        [SwaggerResponse(200, "Get all cars")]
        public async Task<IActionResult> GetCars([FromQuery] CarParameters carParameters)
        {
            var cars = await _repository.Car.GetListCarsAsync(carParameters);

            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);

            return Ok(returnData);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get one car")]
        [SwaggerResponse(400, "Car not found")]
        [SwaggerResponse(200, "Get one cars")]
        [ServiceFilter(typeof(ValidationFilterAttribute<Car>))]
        public IActionResult GetCar(int id)
        {
            var car = HttpContext.Items["entity"] as Car;

            var returnData = _mapper.Map<CarDtoForGet>(car);

            return Ok(returnData);
        }

        [HttpPost("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Place a bet")]
        [SwaggerResponse(400, "Lot not found")]
        [SwaggerResponse(400, "If current user id and seller id equal that user cannot bet")]
        [SwaggerResponse(400, "If bid user active that user cannot bet")]
        [SwaggerResponse(200, "Bid is accepted")]
        [ServiceFilter(typeof(ValidationFilterAttribute<Lot>))]
        public async Task<IActionResult> Bid(int id)
        {
            await _auctionService.BidAsync(id, User);

            return Ok("Your bid is accepted");
        }
    }
}
