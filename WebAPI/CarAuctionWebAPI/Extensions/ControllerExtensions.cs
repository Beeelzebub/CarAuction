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
                ErrorCode.LotNotFoundError or 
                ErrorCode.AlreadyPlacedBetError or
                ErrorCode.CarNotFound or
                ErrorCode.WrongUsernameOrPasswordError => controller.BadRequest(baseResponse),
                ErrorCode.NoPermissionsError => controller.StatusCode(StatusCodes.Status403Forbidden, baseResponse),
                _ => controller.StatusCode(StatusCodes.Status500InternalServerError, baseResponse)
            };
        }
    }
}
