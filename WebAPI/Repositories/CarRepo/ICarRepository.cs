using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Entity.Models;
using Entity.RequestFeatures;

namespace Repositories
{
    public interface ICarRepository : IRepositoryBase<Car>
    {
        public Task<IEnumerable<Car>> GetCarsByStatusAsync(LotStatus status);
        public Task<IEnumerable<Car>> GetListCarsAsync(CarParameters carParameters);
        public void AddCar(LotCreationDto carDtoForCreation, string userId);
        public Task<IEnumerable<Car>> GetListCarsProfileAsync(string currentUserId, CarsParametersInProfile carsParametersInProfile);
        public Task<Car> GetCarByUserAsync(int id, string idUser);


    }
}
