using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DTO;
using Hangfire.Storage.Monitoring;
using Swashbuckle.AspNetCore.Annotations;
using Services.Authentication;

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

        [HttpPost]
        [Route("api/login")]
        [SwaggerOperation(Summary = "Authentication user")]
        [SwaggerResponse(200, "Authentication user")]
        [SwaggerResponse(401, "Authentication user failed")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            await _authenticationService.ValidateUser(userForAuthentication);

            var token = await _authenticationService.CreateTokenAsync();

            return Ok(new { Token = token });
        }

        [HttpPost]
        [Route("api/register")]
        [SwaggerOperation(Summary = "Registration user")]
        [SwaggerResponse(400, "If the user is registered")]
        [SwaggerResponse(401, "Registration user failed")]
        [SwaggerResponse(200, "Registration success")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        { 
            await _authenticationService.RegistrationAsync(userForRegistrationDto, ModelState);

            var token = _authenticationService.CreateTokenAsync();

            return Ok(new { Token = token });
        }
    }
}