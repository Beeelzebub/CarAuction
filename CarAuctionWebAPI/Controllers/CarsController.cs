using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        public IActionResult AddCar([FromBody] CarDtoForCreation carDtoForCreation)
        {
            var car = _mapper.Map<Car>(carDtoForCreation);
            _carAuctionContext.Cars.Add(car);
            _carAuctionContext.SaveChanges();
            return Ok();
        }
    }
}
