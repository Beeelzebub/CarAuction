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
using DTO.Response;
using Entity.Models;
using Enums;
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

        public async Task<BaseResponse> RegistrationAsync(UserRegistrationDto userForRegistrationDto, ModelStateDictionary modelState)
        {
            _user = _mapper.Map<User>(userForRegistrationDto);

            var result = await _userManager.CreateAsync(_user, userForRegistrationDto.Password);

            if (!result.Succeeded)
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Code, error.Description);
                }

                return BaseResponse.Fail(ErrorCode.RegistrationError, errors);
            }

            if (userForRegistrationDto.UserName == "Admin")
            {
                await _userManager.AddToRoleAsync(_user, "Admin");
            }

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> ValidateUser(UserAuthenticationDto userForAuthenticationDto)
        {
            _user = await _userManager.FindByNameAsync(userForAuthenticationDto.UserName);
            
            if (_user == null || !(await _userManager.CheckPasswordAsync(_user, userForAuthenticationDto.Password)))
            {
                return BaseResponse.Fail(ErrorCode.WrongUsernameOrPasswordError);
            }

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> CreateTokenAsync()
        {
            var key = "secret123456789secret!!!!!";
            var claims = await GetClaims();
            var token = new JwtSecurityTokenHandler().WriteToken(
                new JwtSecurityToken
                (
                    issuer: "CarAuctionWebApi",
                    audience: "https://localhost:5001",
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256))
            );

            return BaseResponse.Success(new JwtTokenDto() { Token = token });
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
