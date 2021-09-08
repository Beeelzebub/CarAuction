using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Entity;
using Entity.Models;
using Entity.RequestFeatures;
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

        public void AddCar(LotCreationDto carDtoForCreation, string userId)
        {
            var lot = _mapper.Map<Lot>(carDtoForCreation);
            lot.SellerId = userId;
            lot.CurrentCost = lot.StartingPrice;

            var car = _mapper.Map<Car>(carDtoForCreation);
            car.Lot = lot;

            _bdContext.Cars.Add(car);
        }

        public async Task<Car> GetCarByUserAsync(int id, string idUser)
        {
            return await _bdContext.Cars.SingleOrDefaultAsync(c => c.Id.Equals(id) && c.Lot.SellerId.Equals(idUser));
        }

        public async Task<IEnumerable<Car>> GetCarsByStatusAsync(LotStatus status)
        {
            return await _bdContext.Cars.Where(sl => sl.Lot.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetListCarsAsync(CarParameters carParameters)
        {
            var query = _bdContext.Cars
                .Include(m => m.Model)
                .ThenInclude(b => b.Brand)
                .Where(l => l.Lot.Status == LotStatus.Approved);

            if (carParameters.BrandId != 0)
            {
                query = query.Where(l => l.Model.BrandId == carParameters.BrandId);
            }

            if (carParameters.ModelId != 0)
            {
                query = query.Where(l => l.Model.Id == carParameters.ModelId);
            }

            var cars = await query.ToListAsync();

            return PagedList<Car>.ToPagedList(cars, carParameters.PageNumber, carParameters.PageSize);
        }

        public async Task<IEnumerable<Car>> GetListCarsProfileAsync(string currentUserId, CarsParametersInProfile carsParametersInProfile)
        {
            var query = _bdContext.Cars.Where(l => l.Lot.SellerId.Equals(currentUserId));

            if (carsParametersInProfile.Status != null)
            {
                query = query.Where(l => l.Lot.Status.Equals(carsParametersInProfile.Status));
            }

            var cars = await query.ToListAsync();

            return PagedList<Car>.ToPagedList(cars, carsParametersInProfile.PageNumber, carsParametersInProfile.PageSize);
        }
    }
}
