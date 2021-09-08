using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DTO;

namespace Services.Profile
{
    public interface IProfileService
    {
        Task RemoveLotAsync(int lotId, ClaimsPrincipal sellerClaims);
        Task AddLotAsync(LotCreationDto lotCreationDto, ClaimsPrincipal sellerClaims);

    }
}
