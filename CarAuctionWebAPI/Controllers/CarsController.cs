using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Entity;
using Entity.DTO;
using Entity.Models;
using Entity.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CarAuctionContext _carAuctionContext;
        private readonly UserManager<User> _userManager;

        public CarsController(IMapper mapper, CarAuctionContext carAuctionContext, UserManager<User> userManager) 
        {
            _mapper = mapper;
            _carAuctionContext = carAuctionContext;
            _userManager = userManager;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carAuctionContext.Cars.ToListAsync();
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);

            return Ok(returnData);
        }

        [HttpGet]
        public async Task<IActionResult> GetCarsByCondition([FromQuery] RequestParameters requestParameters)
        {
            if (!requestParameters.ValidYearRange)
            {
                return BadRequest();
            }
            var cars = await _carAuctionContext.Cars.Where(c=> (c.Year>=requestParameters.MinYear && c.Year<=requestParameters.MaxYear)
                                                         && c.Model.Name.Equals(requestParameters.Model)
                                                         && c.Model.Brand.BrandName.Equals(requestParameters.Brand)).ToListAsync();
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);

            return Ok(returnData);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var car = await _carAuctionContext.Cars.SingleOrDefaultAsync(c => c.Id.Equals(id));
            if (car == null)
            {
                return BadRequest();
            }
            var returnData = _mapper.Map<CarDtoForGet>(car);
            return Ok(returnData);
        }

        [HttpPatch("{id}")]
        public IActionResult Bid(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("Log in to the system ");
            }
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var car = _carAuctionContext.Cars.SingleOrDefault(i => i.Id==id);
            if (car == null)
            {
                return BadRequest("Car not found");
            }

            Lot lot = _carAuctionContext.Lots.SingleOrDefault(i => i.Id == car.LotId);
            if (lot == null)
            {
                return BadRequest("Lot not found");
            }
            lot.CurrentCost += lot.MinimalStep;
            var bid = new Bid
            {
                LotId = lot.Id,
                BuyerId = currentUserId,
                BidStatus = 0
            };
            


            _carAuctionContext.Bids.Add(bid);
            _carAuctionContext.SaveChanges();

            return Ok("Your bid is accepted");
        }
    }
}
