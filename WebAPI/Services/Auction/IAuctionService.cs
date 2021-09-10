using System.Security.Claims;
using System.Threading.Tasks;
using DTO;
using Entity.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Auction
{
    public interface IAuctionService
    {
        Task BidAsync(int lotId, ClaimsPrincipal bidderClaims);
        Task ChangeLotStatus(int lotId, JsonPatchDocument<Lot> jsonPatch);
    }
}
