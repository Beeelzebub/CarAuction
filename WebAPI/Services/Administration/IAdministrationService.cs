using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Entity.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Administration
{
    public interface IAdministrationService
    {
        Task ChangeLotStatusAsync(int lotId, JsonPatchDocument<Lot> jsonPatch);
        Task<List<CarDto>> GetPendingCarsAsync();
        Task<CarDto> GetPendingCarAsync(int id);
    }
}
