﻿using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        public async Task<IActionResult> GetAllCars([FromQuery] CarParameters carParameters)
        {
            var cars = await _carAuctionContext.Cars.ToListAsync();
            var carData = PagedList<Car>.ToPagedList(cars, carParameters.PageNumber, carParameters.PageSize);
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(carData);

            return Ok(returnData);
        }

        [HttpGet]
        public async Task<IActionResult> GetCarsByCondition([FromQuery] CarParameters carParameters)
        {
            if (!carParameters.ValidYearRange)
            {
                return BadRequest();
            }
            var cars = await _carAuctionContext.Cars.Where(c=> (c.Year>= carParameters.MinYear && c.Year<= carParameters.MaxYear)
                                                         && c.Model.Name.Equals(carParameters.Model)
                                                         && c.Model.Brand.BrandName.Equals(carParameters.Brand)).ToListAsync();
            var carData = PagedList<Car>.ToPagedList(cars, carParameters.PageNumber, carParameters.PageSize);
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(carData);

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

        [HttpPut("{id}")]
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

            var lot = _carAuctionContext.Lots.SingleOrDefault(i => i.Id == car.LotId);
            if (lot == null)
            {
                return BadRequest("Lot not found");
            }
            if (currentUserId ==lot.SellerId)
            {
                return BadRequest("You cannot bet");
            }

            var  bids= _carAuctionContext.Bids.Where(x=>x.LotId.Equals(lot.Id) );
            foreach (var item in bids)
            {
                if (item.BuyerId == currentUserId && item.BidStatus == 0)
                {
                    return BadRequest("You have already placed a bet");
                }

                item.BidStatus = BidStatus.Outbid;
                
            }
            _carAuctionContext.SaveChanges();
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
