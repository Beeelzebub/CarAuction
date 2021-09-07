using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarAuctionWebAPI.Filters;
using DTO;
using Entity.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Repositories;
using Services.Background;
using Swashbuckle.AspNetCore.Annotations;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        private readonly IBackgroundService _backgroundService;

        public AdminController(IMapper mapper, IRepositoryManager repository, IBackgroundService backgroundService)
        {
            _mapper = mapper;
            _repository = repository;
            _backgroundService = backgroundService;
        }

        [HttpGet("cars")]
        [SwaggerOperation(Summary = "Get all the user's cars that have the status Pending")]
        [SwaggerResponse(200, "Get all cars ")]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _repository.Car.GetCarsByStatusAsync(LotStatus.Pending);

            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);

            return Ok(returnData);
        }

        [HttpGet("cars/{id}")]
        [SwaggerOperation(Summary = "Get one the user's car that have the status Pending")]
        [SwaggerResponse(400, "If car not found")]
        [SwaggerResponse(200, "Get one car")]
        [ServiceFilter(typeof(ValidationFilterAttribute<Car>))]
        public IActionResult GetOneCar(int id)
        {
            var car = HttpContext.Items["entity"] as Car;

            var returnData = _mapper.Map<CarDtoForGet>(car);

            return Ok(returnData);
        }

        [HttpPut("cars/{id}")]
        [SwaggerOperation(Summary = "Change status car")]
        [SwaggerResponse(400, "If car not found")]
        [SwaggerResponse(200, "Change lot status")]
        [ServiceFilter(typeof(ValidationFilterAttribute<Lot>))]
        public IActionResult ChangeLotStatus(int id, [FromBody] LotDtoForChangeStatus statusLot)
        {
            var lot = HttpContext.Items["entity"] as Lot;

            lot.Status = statusLot.Status;

            if (lot.Status == LotStatus.Approved)
            {
                lot.StartDate = DateTime.Now;
                lot.EndDate = DateTime.Now.AddMinutes(5);
                BackgroundJob.Schedule(() => _backgroundService.ChooseWinner(id), TimeSpan.FromMinutes(5));
            }
            _repository.Lot.Save();
            return Ok();
        }
    }
}
