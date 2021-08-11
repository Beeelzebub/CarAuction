using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.DTO;
using Entity.Models;

namespace Contracts
{
    public interface IProfileRepository
    {
        void AddCar(CarDtoForCreation carDtoForCreation, string userId);
        Task<IEnumerable<Car>> GetCarsProfileAsync(string id);
        Task<Car> GetCarAsync(int id);
        Task<Lot> GetLotAsync(int id);
    }
}
