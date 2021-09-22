using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entity;
using Entity.Models;
using Entity.RequestFeatures;
using Enums;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        private readonly CarAuctionContext _bdContext;
        private readonly IMapper _mapper;

        public CarRepository(CarAuctionContext bdContext, IMapper mapper) : base(bdContext)
        {
            _bdContext = bdContext;
            _mapper = mapper;
        }

        public async Task<Car> GetCarByUserAsync(int id, string idUser)
        {
            return await _bdContext.Cars.SingleOrDefaultAsync(c => c.Id.Equals(id) && c.Lot.SellerId.Equals(idUser));
        }

        public async Task<IEnumerable<Car>> GetCarsByStatusAsync(LotStatus status)
        {
            return await _bdContext.Cars.Include(m=>m.Model)
                .ThenInclude(b=>b.Brand)
                .Where(sl => sl.Lot.Status == status).ToListAsync();
        }
        public async Task<Car> GetCarAsync(int id)
        {
            return await _bdContext.Cars.Include(l=>l.Lot)
                .Include(m=>m.Model)
                .ThenInclude(b=>b.Brand)
                .SingleOrDefaultAsync(c => c.Id.Equals(id));
        }

        public async Task<IEnumerable<Car>> GetListCarsAsync(CarParameters carParameters)
        {
            var query = _bdContext.Cars
                .Include(m => m.Model)
                .ThenInclude(b => b.Brand)
                .Where(l => l.Lot.Status == LotStatus.Approved && (l.Year >= carParameters.MinYear && l.Year <= carParameters.MaxYear));

            if (!string.IsNullOrEmpty(carParameters.BrandName))
            {
                query = query.Where(l => l.Model.Brand.BrandName == carParameters.BrandName);
            }

            if (!string.IsNullOrEmpty(carParameters.ModelName))
            {
                query = query.Where(l => l.Model.Name == carParameters.ModelName);
            }

            var cars = await query.ToListAsync();

            return PagedList<Car>.ToPagedList(cars, carParameters.PageNumber, carParameters.PageSize);
        }

        public async Task<IEnumerable<Car>> GetListByParametersAsync(string currentUserId, CarsParametersInProfile carsParametersInProfile)
        {
            var query = _bdContext.Cars.Include(c => c.Model).ThenInclude(m => m.Brand).Where(l => l.Lot.SellerId.Equals(currentUserId));

            if (carsParametersInProfile.Status != null)
            {
                query = query.Where(l => l.Lot.Status.Equals(carsParametersInProfile.Status));
            }

            var cars = await query.ToListAsync();

            return PagedList<Car>.ToPagedList(cars, carsParametersInProfile.PageNumber, carsParametersInProfile.PageSize);
        }

        public override async Task<Car> GetAsync(int id) =>
            await _bdContext.Cars.Include(c => c.Lot).Include(c => c.Model).ThenInclude(m => m.Brand).FirstOrDefaultAsync(c => c.Id == id);
    }
}
