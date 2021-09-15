using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;
using Entity.RequestFeatures;
using Enums;

namespace Repositories
{
    public interface ICarRepository : IRepositoryBase<Car>
    {
        public Task<IEnumerable<Car>> GetCarsByStatusAsync(LotStatus status);
        public Task<IEnumerable<Car>> GetListCarsAsync(CarParameters carParameters);
        public Task<IEnumerable<Car>> GetListByParametersAsync(string currentUserId, CarsParametersInProfile carsParametersInProfile);
        public Task<Car> GetCarByUserAsync(int id, string idUser);
        public Task<Car> GetCarAsync(int id);


    }
}
