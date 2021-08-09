using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Entity;
using Entity.DTO;
using Entity.Models;

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


        [HttpGet]
        public IActionResult GetAllCars()
        {
            var cars = _carAuctionContext.Cars.ToList();
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);

            return Ok(returnData);
        }

        [HttpGet("{id}")]
        public IActionResult GetCar(int id)
        {
            var car = _carAuctionContext.Cars.SingleOrDefault(c => c.Id.Equals(id));
            if (car == null)
            {
                return BadRequest();
            }
            var returnData = _mapper.Map<CarDtoForGet>(car);
            return Ok(returnData);
        }

        [HttpPost]
        public IActionResult AddCar([FromBody] CarDtoForCreation carDtoForCreation)
        {
            var car = _mapper.Map<Car>(carDtoForCreation);
            _carAuctionContext.Cars.Add(car);
            _carAuctionContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = _carAuctionContext.Cars.SingleOrDefault(c => c.Id.Equals(id));
            if (car == null)
            {
                return BadRequest();
            }
            var lot = _carAuctionContext.Lots.SingleOrDefault(c => c.Id.Equals(car.LotId));
            _carAuctionContext.Cars.Remove(car);
            _carAuctionContext.Lots.Remove(lot);
            _carAuctionContext.SaveChanges();
            return Ok();
        }
    }
}
