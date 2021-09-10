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
        Task ValidateUser(UserAuthenticationDto userForAuthenticationDto);
        Task RegistrationAsync(UserRegistrationDto userForRegistrationDto, ModelStateDictionary modelState);
        Task<string> CreateTokenAsync();
    }
}
