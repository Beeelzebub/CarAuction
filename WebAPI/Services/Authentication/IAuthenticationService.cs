using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Services.Authentication
{
    public interface IAuthenticationService
    {
        Task ValidateUser(UserForAuthenticationDto userForAuthenticationDto);
        Task RegistrationAsync(UserForRegistrationDto userForRegistrationDto, ModelStateDictionary modelState);
        Task<string> CreateTokenAsync();
    }
}
