using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Contracts.Services
{
    public interface IAuthenticationService
    {
        //Task<bool> ValidateUser(string userName, string password);
        //Task<string> CreateToken();
        Task<ActionResult> LoginAsync(UserForAuthenticationDto userForAuthenticationDto);
        Task<ActionResult> Registration(UserForRegistrationDto userForRegistrationDto, ModelStateDictionary modelState);
    }
}
