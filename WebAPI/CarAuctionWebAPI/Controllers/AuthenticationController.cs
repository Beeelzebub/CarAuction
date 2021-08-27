﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web.Mvc;
using Contracts.Services;
using DTO;
using Swashbuckle.AspNetCore.Annotations;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

namespace CarAuctionWebAPI.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/login")]
        [SwaggerOperation(Summary = "Authentication user")]
        [SwaggerResponse(200, "Authentication user")]
        [SwaggerResponse(401, "Authentication user failed")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            return await _authenticationService.LoginAsync(userForAuthentication);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/register")]
        [SwaggerOperation(Summary = "Registration user")]
        [SwaggerResponse(400, "If the user is registered")]
        [SwaggerResponse(401, "Registration user failed")]
        [SwaggerResponse(200, "Registration success")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            return await _authenticationService.Registration(userForRegistrationDto, ModelState);
        }
    }
}
