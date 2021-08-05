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
    public class LotsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CarAuctionContext _carAuctionContext;

        public LotsController(IMapper mapper, CarAuctionContext carAuctionContext) 
        {
            _mapper = mapper;
            _carAuctionContext = carAuctionContext;
        }


        [HttpGet]
        public IActionResult GetAllLots()
        {
            var lots = _carAuctionContext.Lots.ToList();


            return Ok(lots);
        }

    }
}
