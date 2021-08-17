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
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;

        public LoginController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            if (!await _authenticationManager.ValidateUser(userForAuthentication))
            {
                return Unauthorized();
            }
            
            return Ok(new { Token =  _authenticationManager.CreateToken().Result });
        }
    }
}
