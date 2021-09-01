using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DTO;
using Entity.Models;
using Entity.RequestFeatures;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;

        public CarsController(IMapper mapper, IRepositoryManager repository, UserManager<User> userManager) 
        {
            _mapper = mapper;
            _repository = repository;
            _userManager = userManager;
        }
        

        [HttpGet]
        [SwaggerOperation(Summary = "Get all cars")]
        [SwaggerResponse(400, "If Year not valid")]
        [SwaggerResponse(200, "Get all cars")]
        public async Task<IActionResult> GetCars([FromQuery] CarParameters carParameters)
        {
            if (!carParameters.ValidYearRange)
            {
                return BadRequest();
            }

            var cars = await _repository.Car.GetListCarsAsync(carParameters);
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);
            return Ok(returnData);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get one car")]
        [SwaggerResponse(400, "Car not found")]
        [SwaggerResponse(200, "Get one cars")]
        public async Task<IActionResult> GetCar(int id)
        {
            var car = await _repository.Car.GetAsync(id);

            if (car == null)
            {
                return BadRequest();
            }

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
        public async Task<IActionResult> Bid(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            var lot = await _repository.Lot.GetAsync(id);

            if (lot == null)
            {
                return BadRequest("Lot is not found");
            }

            if (currentUserId == lot.SellerId)
            {
                return BadRequest("You cannot bet");
            }
            
            var activeBid = await _repository.Bid.GetActiveBidAsync(id);

            if (activeBid != null)
            {
                if (activeBid.BuyerId == currentUserId)
                {
                    return BadRequest("You have already placed a bet");
                }
                
                activeBid.BidStatus = BidStatus.Outbid;
            }

            lot.CurrentCost += lot.MinimalStep;
            var bidInfo = new Bid
            {
                LotId = lot.Id,
                BuyerId = currentUserId
            };
            await _repository.Bid.CreateAsync(bidInfo); 
            _repository.Bid.Save();
            return Ok("Your bid is accepted");
        }
    }
}
