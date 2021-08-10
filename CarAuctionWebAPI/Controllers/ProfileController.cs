using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Entity;
using Entity.Models;
using System.Security.Claims;
using Entity.DTO;
using Microsoft.EntityFrameworkCore;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CarAuctionContext _carAuctionContext;

        public ProfileController(IMapper mapper, CarAuctionContext carAuctionContext)
        {
            _mapper = mapper;
            _carAuctionContext = carAuctionContext;
        }
        [HttpPost]
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
            //_carAuctionContext.Lots.Remove(lot);
            await _carAuctionContext.SaveChangesAsync();
            return Ok();
        }
    }
}
