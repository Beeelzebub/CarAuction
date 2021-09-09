using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DTO;
using Entity.RequestFeatures;

namespace Services.Profile
{
    public interface IProfileService
    {
        Task RemoveLotAsync(int lotId, ClaimsPrincipal userClaims);
        Task AddLotAsync(LotCreationDto lotCreationDto, ClaimsPrincipal userClaims);
        Task<CarDto> GetUsersCarInfoAsync(int carId, ClaimsPrincipal userClaims);
        Task<List<BidsDto>> GetUsersBidsAsync(ClaimsPrincipal userClaims);
        Task<List<CarDto>> GetUsersCarsAsync(CarsParametersInProfile carsParametersInProfile, ClaimsPrincipal userClaims);
    }
}
