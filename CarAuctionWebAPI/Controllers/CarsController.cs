using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Entity.DTO;
using Entity.Models;
using Entity.RequestFeatures;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        private readonly UserManager<User> _userManager;

        public CarsController(IMapper mapper, ICarRepository carRepository, UserManager<User> userManager) 
        {
            _mapper = mapper;
            _carRepository = carRepository;
            _userManager = userManager;
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCars([FromQuery] CarParameters carParameters)
        {
            var cars = await _carRepository.GetCarsAsync(carParameters);
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);

            return Ok(returnData);
        }

        [HttpGet]
        public async Task<IActionResult> GetCarsByCondition([FromQuery] CarParameters carParameters)
        {
            if (!carParameters.ValidYearRange)
            {
                return BadRequest();
            }

            var cars = await _carRepository.GetCarsByConditionAsync(carParameters);
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);

            return Ok(returnData);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var car = await _carRepository.GetCarAsync(id);
            if (car == null)
            {
                return BadRequest();
            }
            var returnData = _mapper.Map<CarDtoForGet>(car);
            return Ok(returnData);
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<IActionResult> Bid(int id)
        {
            
            var currentUserId = _userManager.GetUserId(User);


            var car = await _carRepository.GetCarAsync(id);
            if (car == null)
            {
                return BadRequest("Car not found");
            }

            var lot = await _carRepository.GetLotAsync(car.LotId);
            if (lot == null)
            {
                return BadRequest("Lot not found");
            }
            if (currentUserId == lot.SellerId)
            {
                return BadRequest("You cannot bet");
            }

            var  bids= _carRepository.GetListBids(lot.Id);
            foreach (var item in bids)
            {
                if (item.BuyerId == currentUserId && item.BidStatus == 0)
                {
                    return BadRequest("You have already placed a bet");
                }

                if (!item.BidStatus.Equals(BidStatus.Won))
                {
                    item.BidStatus = BidStatus.Outbid;
                }
            }
            lot.CurrentCost += lot.MinimalStep;
            _carRepository.AddBid(lot.Id, currentUserId);
            
            _carRepository.Save();
            return Ok("Your bid is accepted");
        }
    }
}
