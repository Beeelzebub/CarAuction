using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<BaseResponse> ValidateUser(UserAuthenticationDto userForAuthenticationDto);
        Task<BaseResponse> RegistrationAsync(UserRegistrationDto userForRegistrationDto, ModelStateDictionary modelState);
        Task<BaseResponse> CreateTokenAsync();
    }
}
