using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entity.DTO;
using Entity.Models;
using Microsoft.AspNetCore.Identity;

namespace CarAuctionWebAPI.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AuthenticationController(IAuthenticationService authenticationManager, IMapper mapper, UserManager<User> userManager)
        {
            _authenticationManager = authenticationManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            if (!await _authenticationManager.ValidateUser(userForAuthentication.UserName, userForAuthentication.Password))
            {
                return Unauthorized();
            }

            return Ok(new { Token = _authenticationManager.CreateToken().Result });
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var user = _mapper.Map<User>(userForRegistrationDto);
            var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            if (userForRegistrationDto.UserName == "Admin")
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            if (!await _authenticationManager.ValidateUser(userForRegistrationDto.UserName, userForRegistrationDto.Password))
            {
                return Unauthorized();
            }

            return StatusCode(201, new {Token = _authenticationManager.CreateToken().Result});
        }
    }
}
