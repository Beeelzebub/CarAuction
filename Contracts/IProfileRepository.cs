using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Entity.DTO;
using Entity.Models;
using Entity.RequestFeatures;

namespace Contracts
{
    public interface IProfileRepository
    {
        void AddCar(CarDtoForCreation carDtoForCreation, string userId);
        Task<IEnumerable<Car>> GetCarsProfileAsync(string id, CarsParametersInProfile carsParametersInProfile);
        Task<Car> GetCarAsync(int id, string idUser);
        Task<Lot> GetLotAsync(int id);
        void Save();
        void DeleteLotWithCar(Car car, Lot lot);
        Task<IEnumerable<Bid>> UserBidsAsync(string userId);
    }
}
