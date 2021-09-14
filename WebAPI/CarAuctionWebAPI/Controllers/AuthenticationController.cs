using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarAuctionWebAPI.Extensions;
using DTO;
using DTO.Response;
using Enums;
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
        [SwaggerResponse(200, "Authentication user", typeof(Response<JwtTokenDto>))]
        [SwaggerResponse(400, "Authentication user failed", typeof(Response))]
        public async Task<IActionResult> Login([FromBody] UserAuthenticationDto userForAuthentication)
        {
            var result = await _authenticationService.ValidateUser(userForAuthentication);

            if (result.ErrorCode == ErrorCode.Success)
            {
                result = await _authenticationService.CreateTokenAsync();
            }

            return this.Answer(result, Ok(result));
        }

        [HttpPost]
        [Route("api/register")]
        [SwaggerOperation(Summary = "Registration user")]
        [SwaggerResponse(200, "Registration success", typeof(Response<JwtTokenDto>))]
        [SwaggerResponse(400, "If the user is registered", typeof(Response))]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userForRegistrationDto)
        { 
            var result = await _authenticationService.RegistrationAsync(userForRegistrationDto, ModelState);
            
            if (result.ErrorCode == ErrorCode.Success)
            {
                result = await _authenticationService.CreateTokenAsync();
            }

            return this.Answer(result, Ok(result));
        }
    }
}