using System.Security.Claims;
using System.Threading.Tasks;
using DTO.Response;
using Entity.Models;
using Entity.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Auction
{
    public interface IAuctionService
    {
        Task<BaseResponse> BidAsync(int lotId, ClaimsPrincipal userClaims);
        Task<BaseResponse> GetCarsAsync(CarParameters carParameters);
        Task<BaseResponse> GetCarAsync(int carId);
        Task<BaseResponse> GetModelsWithBrands();
        Task<BaseResponse> RedemptionAsync(int lotId, ClaimsPrincipal userClaims);
    }
}
