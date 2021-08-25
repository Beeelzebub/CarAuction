using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entity;
using Entity.DTO;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;

        public AdminController(IMapper mapper, IAdminRepository adminRepository)
        {
            _mapper = mapper;
            _adminRepository = adminRepository;
        }

        [HttpGet("cars")]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _adminRepository.GetCarsByStatusAsync(Status.Pending);
            var returnData = _mapper.Map<IEnumerable<CarDtoForGet>>(cars);

            return Ok(returnData);
        }

        [HttpGet("cars/{id}")]
        public async Task<IActionResult> GetOneCar(int id)
        {
            var car = await _adminRepository.GetCarAsync(id);

            if (car == null)
            {
                return BadRequest("Car is not found");
            }

            var returnData = _mapper.Map<CarDtoForGet>(car);

            return Ok(returnData);
        }

        [HttpPut("cars/{id}")]
        public async Task<IActionResult> ChangeLotStatus(int id, [FromBody] LotDtoForChangeStatus statusLot)
        {
            var lot = await _adminRepository.GetLotAsync(id);

            if (lot == null)
            {
                return BadRequest("Car not found");
            }

            lot.Status = statusLot.Status;
            lot.StartDate = DateTime.Now;
            lot.EndDate = DateTime.Now.AddMinutes(5);
            await _adminRepository.SaveAsync();
            return Ok();
        }
    }
}
