using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.DTO;
using Entity.Models;

namespace Contracts
{
    public interface IProfileRepository
    {
        void AddCar(CarDtoForCreation carDtoForCreation, string userId);
        Task<IEnumerable<Car>> GetCarsProfileIsPendingAsync(string id);
        Task<IEnumerable<Car>> GetCarsProfileIsApprovedAsync(string id);
        Task<Car> GetCarIsPendingAsync(int id, string idUser);
        Task<Car> GetCarIsApprovedAsync(int id, string idUser);
        Task<Lot> GetLotAsync(int id);
        void Save();
        void DeleteLotWithCar(Car car, Lot lot);
    }
}
