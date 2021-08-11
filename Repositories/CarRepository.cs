using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entity;
using Entity.Models;
using Entity.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarAuctionContext _carAuctionContext;

        public CarRepository(CarAuctionContext carAuctionContext)
        {
            _carAuctionContext = carAuctionContext;
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _carAuctionContext.Cars.SingleOrDefaultAsync(c => c.Id.Equals(id));
        }

        public async Task<IEnumerable<Car>> GetCarsAsync(CarParameters carParameters)
        {
            var cars = await _carAuctionContext.Cars.ToListAsync();
            return PagedList<Car>.ToPagedList(cars, carParameters.PageNumber, carParameters.PageSize);
            
        }

        public async Task<IEnumerable<Car>> GetCarsByConditionAsync(CarParameters carParameters)
        {
            var cars = await _carAuctionContext.Cars.Where(c => (c.Year >= carParameters.MinYear && c.Year <= carParameters.MaxYear)
                                                                && c.Model.Name.Equals(carParameters.Model)
                                                                && c.Model.Brand.BrandName.Equals(carParameters.Brand)).ToListAsync();
            return PagedList<Car>.ToPagedList(cars, carParameters.PageNumber, carParameters.PageSize);
        }
    }
}
