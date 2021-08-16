using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;

namespace Contracts
{
    public interface IAdminRepository
    {
        void Save();
        Task<IEnumerable<Car>> GetCarsByStatusAsync();
        Task<Car> GetCarAsync(int id);
        Task<Lot> GetLotAsync(int id);
    }
}
