using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.DTO;
using Entity.Models;
using Entity.RequestFeatures;

namespace Contracts
{
    public interface IProfileRepository
    {
        void AddCar(CarDtoForCreation carDtoForCreation, string userId);
        Task<Car> GetCarAsync(int id, string idUser);
        Task<Lot> GetLotAsync(int id);
        void Save();
        void DeleteLotWithCar(Car car, Lot lot);
        Task<IEnumerable<Bid>> UserBidsAsync(string userId);
        Task<IEnumerable<Car>> GetCarsAsync(string userId, CarsParametersInProfile carsParametersInProfile);
    }
}
