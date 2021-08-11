using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Models;
using Entity.RequestFeatures;

namespace Contracts
{
    public interface ICarRepository
    { 
        Task<IEnumerable<Car>> GetCarsAsync(CarParameters carParameters);
        Task<IEnumerable<Car>> GetCarsByConditionAsync(CarParameters carParameters);
        Task<Car> GetCarAsync(int id);

    }
}
