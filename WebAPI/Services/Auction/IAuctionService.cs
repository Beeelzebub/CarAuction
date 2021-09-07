using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Services.Auction
{
    public interface IAuctionService
    {
        Task BidAsync(int lotId, ClaimsPrincipal bidderClaims);
    }
}
