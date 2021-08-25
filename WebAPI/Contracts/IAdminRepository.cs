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
        Task SaveAsync();
        Task<IEnumerable<Car>> GetCarsByStatusAsync(Status status);
        Task<Car> GetCarAsync(int id);
        Task<Lot> GetLotAsync(int id);
        void ChooseWinner(int lotId);
    }
}
