using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entity.DTO;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Repositories
{
    public class AuthenticationManager: IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private User _user;
        
        public AuthenticationManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ValidateUser(string userName, string password)
        {
            _user = await _userManager.FindByNameAsync(userName);
            return (_user != null && await _userManager.CheckPasswordAsync(_user, password));
        }
        
        public async Task<string> CreateToken()
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
                        DateTime.Now.AddMinutes(60),
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
