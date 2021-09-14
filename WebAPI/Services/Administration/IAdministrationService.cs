using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DTO.Response;
using Entity.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Administration
{
    public interface IAdministrationService
    {
        Task<BaseResponse> ChangeLotStatusAsync(int lotId, JsonPatchDocument<Lot> jsonPatch);
        Task<BaseResponse> GetPendingCarsAsync();
        Task<BaseResponse> GetPendingCarAsync(int id);
    }
}
