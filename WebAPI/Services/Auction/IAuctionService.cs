using System.Security.Claims;
using System.Threading.Tasks;
using Entity.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Auction
{
    public interface IAuctionService
    {
        Task BidAsync(int lotId, ClaimsPrincipal bidderClaims);
    }
}
