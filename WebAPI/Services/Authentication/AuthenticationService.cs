using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services.Exceptions;

namespace Services.Authentication
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

        public async Task RegistrationAsync(UserForRegistrationDto userForRegistrationDto, ModelStateDictionary modelState)
        {
            _user = _mapper.Map<User>(userForRegistrationDto);

            var result = await _userManager.CreateAsync(_user, userForRegistrationDto.Password);

            if (!result.Succeeded)
            {
                var exception = new BadRequestException("Identity error");

                foreach (var error in result.Errors)
                {
                    exception.Data.Add(error.Code, error.Description);
                }

                throw exception;
            }

            if (userForRegistrationDto.UserName == "Admin")
            {
                await _userManager.AddToRoleAsync(_user, "Admin");
            }
        }

        public async Task ValidateUser(UserForAuthenticationDto userForAuthenticationDto)
        {
            _user = await _userManager.FindByNameAsync(userForAuthenticationDto.UserName);
            
            if (_user == null || !(await _userManager.CheckPasswordAsync(_user, userForAuthenticationDto.Password)))
            {
                throw new BadRequestException("Wrong username or password");
            }
        }

        public async Task<string> CreateTokenAsync()
        {
            var key = "secret123456789secret!!!!!";
            var claims = await GetClaims();
            return new JwtSecurityTokenHandler().WriteToken(
                new JwtSecurityToken
                (
                    issuer: "CarAuctionWebApi",
                    audience: "https://localhost:5001",
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
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
