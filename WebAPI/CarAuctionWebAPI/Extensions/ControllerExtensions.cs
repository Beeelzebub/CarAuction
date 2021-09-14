using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DTO.Response;
using Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionWebAPI.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult Answer(this ControllerBase controller, BaseResponse baseResponse, IActionResult expectedResponse)
        {
            return baseResponse.ErrorCode switch
            {
                ErrorCode.Success => expectedResponse,
                var c when BadRequestCodes.Contains(c) => controller.BadRequest(baseResponse),
                var c when NoPermissionsCodes.Contains(c) => controller.StatusCode(StatusCodes.Status403Forbidden, baseResponse),
                _ => controller.StatusCode(StatusCodes.Status500InternalServerError, baseResponse)
            };
        }


        private static readonly ErrorCode[] BadRequestCodes =
        {
            ErrorCode.LotNotFoundError,
            ErrorCode.CarNotFound,
            ErrorCode.AlreadyPlacedBetError,
            ErrorCode.WrongUsernameOrPasswordError,
            ErrorCode.RegistrationError
        };

        private static readonly ErrorCode[] NoPermissionsCodes =
        {
            ErrorCode.NoPermissionsError
        };
    }
}
