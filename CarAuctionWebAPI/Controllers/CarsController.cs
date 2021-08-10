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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCar([FromBody] CarDtoForCreation carDtoForCreation)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                Car car = new Car
                {
                    Year = carDtoForCreation.Year,
                    ImageUrl = carDtoForCreation.ImageUrl,
                    Fuel = carDtoForCreation.Fuel,
                    CarBody = carDtoForCreation.CarBody,
                    DriveUnit = carDtoForCreation.DriveUnit,
                    Model = new Model
                    {
                        Name = carDtoForCreation.Model,
                        Brand = new Brand
                        {
                            BrandName = carDtoForCreation.Brand
                        }
                    },
                    Lot = new Lot
                    {
                        StartDate = DateTime.Now.AddDays(0),
                        EndDate = DateTime.Now.AddDays(7),
                        MinimalStep = carDtoForCreation.MinimalStep,
                        StartingPrice = carDtoForCreation.StartingPrice,
                        CurrentCost = carDtoForCreation.StartingPrice,
                        RedemptionPrice = carDtoForCreation.RedemptionPrice,
                        SellerId = currentUserId
                    }


                };
            _carAuctionContext.Cars.Add(car);
            await _carAuctionContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("MyCars")]
        [Authorize]
        public async Task<IActionResult> GetCarsForUser()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cars = await _carAuctionContext.Cars.Where(i => i.Lot.SellerId == currentUserId).ToListAsync();
            return Ok(cars);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _carAuctionContext.Cars.SingleOrDefaultAsync(c => c.Id.Equals(id));
            if (car == null)
            {
                return BadRequest();
            }
            var lot = _carAuctionContext.Lots.SingleOrDefault(c => c.Id.Equals(car.LotId));
            _carAuctionContext.Cars.Remove(car);
            _carAuctionContext.Lots.Remove(lot);
            await _carAuctionContext.SaveChangesAsync();
            return Ok();
        }
    }
}
