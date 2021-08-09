using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Entity.DTO;
using Entity.Models;
using Entity.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CarAuctionContext _carAuctionContext;

        public CarsController(IMapper mapper, CarAuctionContext carAuctionContext) 
        {
            _mapper = mapper;
            _carAuctionContext = carAuctionContext;
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
        //[Authorize(Roles = "Seller")]
        public async Task<IActionResult> AddCar([FromBody] CarDtoForCreation carDtoForCreation)
        {
            var car = _mapper.Map<Car>(carDtoForCreation);
            _carAuctionContext.Cars.Add(car);
            await _carAuctionContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Seller")]
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
