using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DTO;
using Entity.Models;
using Entity.RequestFeatures;
using System.Threading.Tasks;
using CarAuctionWebAPI.Extensions;
using CarAuctionWebAPI.Filters;
using DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using Repositories;
using Services.Auction;
using Swashbuckle.AspNetCore.Annotations;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public CarsController(IAuctionService auctionService) 
        {
            _auctionService = auctionService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all cars")]
        [SwaggerResponse(200, "Get all cars", typeof(Response<List<CarDto>>))]
        public async Task<IActionResult> GetCars([FromQuery] CarParameters carParameters)
        {
            var result = await _auctionService.GetCarsAsync(carParameters);

            return this.Answer(result, Ok(result));
        }

        [HttpGet("/api/GetModelsWithBrands")]
        [SwaggerOperation(Summary = "Get models with brands")]
        [SwaggerResponse(200, "Get models with brands")]
        public async Task<IActionResult> GetModelsWithBrands()
        {
            var result = await _auctionService.GetModelsWithBrands();

            return this.Answer(result, Ok(result));
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get one car")]
        [SwaggerResponse(200, "Get one cars", typeof(Response<CarDto>))]
        [SwaggerResponse(400, "Car not found", typeof(Response))]
        public async Task<IActionResult> GetCar(int id)
        {
            var result = await _auctionService.GetCarAsync(id);

            return this.Answer(result, Ok(result));
        }

        [HttpPost("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Place a bet")]
        [SwaggerResponse(200, "Bid is accepted", typeof(Response))]
        [SwaggerResponse(400, "Lot not found", typeof(Response))]
        [SwaggerResponse(400, "If current user id and seller id equal that user cannot bet", typeof(Response))]
        [SwaggerResponse(400, "If bid user active that user cannot bet", typeof(Response))]
        public async Task<IActionResult> Bid(int id)
        {
            var result = await _auctionService.BidAsync(id, User);

            return this.Answer(result, Ok(result));
        }

        [HttpPost("/Redemption/{lotId}")]
        [Authorize]
        [SwaggerOperation(Summary = "Lot redemption")]
        [SwaggerResponse(200, "Bid is accepted", typeof(Response))]
        [SwaggerResponse(400, "Lot not found", typeof(Response))]
        [SwaggerResponse(400, "Seller cannot redeem his own car", typeof(Response))]
        public async Task<IActionResult> Redemption(int lotId)
        {
            var result = await _auctionService.RedemptionAsync(lotId, User);

            return this.Answer(result, Ok(result));
        }
    }

    
}
