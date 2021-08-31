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
        Task<ActionResult> LoginAsync(UserForAuthenticationDto userForAuthenticationDto);
        Task<ActionResult> Registration(UserForRegistrationDto userForRegistrationDto, ModelStateDictionary modelState);
    }
}
