using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.Services;
using DTO;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Services
{
    public class AuthenticationService : ActionResult, IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private User _user;

        public AuthenticationService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ActionResult> Registration(UserForRegistrationDto userForRegistrationDto, ModelStateDictionary modelState)
        {
            var user = _mapper.Map<User>(userForRegistrationDto);
            var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    modelState.AddModelError(error.Code, error.Description);
                }

                return new ObjectResult(modelState.Select(m => m.Value.Errors).ToList());
            }

            if (userForRegistrationDto.UserName == "Admin")
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            if (!await ValidateUser(userForRegistrationDto.UserName, userForRegistrationDto.Password))
            {
                return new UnauthorizedResult();
            }

            return new OkObjectResult(new { Token = CreateToken().Result });
        }

        public async Task<ActionResult> LoginAsync(UserForAuthenticationDto userForAuthenticationDto)
        {
            if (!await ValidateUser(userForAuthenticationDto.UserName, userForAuthenticationDto.Password))
            {
                return new UnauthorizedResult();
            }

            return new OkObjectResult(new { Token = CreateToken().Result });
        }

        private async Task<bool> ValidateUser(string userName, string password)
        {
            _user = await _userManager.FindByNameAsync(userName);

            return (_user != null && await _userManager.CheckPasswordAsync(_user, password));
        }
        private async Task<string> CreateToken()
        {
            var key = "secret123456789secret!!!!!";
            var claims = await GetClaims();
            return new JwtSecurityTokenHandler().WriteToken(
                new JwtSecurityToken
                (
                    issuer: "CarAuctionWebApi",
                    audience: "https://localhost:5001",
                    claims: claims,
                    expires:
                    DateTime.Now.AddDays(1),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256))
            );
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, _user.Id)
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }   
    }
}
