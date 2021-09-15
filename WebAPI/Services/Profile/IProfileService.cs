using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DTO;
using DTO.Response;
using Entity.RequestFeatures;

namespace Services.Profile
{
    public interface IProfileService
    {
        Task<BaseResponse> RemoveLotAsync(int lotId, ClaimsPrincipal userClaims);
        Task<BaseResponse> AddLotAsync(LotCreationDto lotCreationDto, ClaimsPrincipal userClaims);
        Task<BaseResponse> GetUsersCarInfoAsync(int carId, ClaimsPrincipal userClaims);
        Task<BaseResponse> GetUsersBidsAsync(ClaimsPrincipal userClaims);
        Task<BaseResponse> GetUsersCarsAsync(CarsParametersInProfile carsParametersInProfile, ClaimsPrincipal userClaims);
    }
}
