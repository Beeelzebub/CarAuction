using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entity;
using Entity.DTO;
using Entity.Models;
using Entity.RequestFeatures;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly CarAuctionContext _carAuctionContext;
        private readonly IMapper _mapper;

        public ProfileRepository(CarAuctionContext carAuctionContext, IMapper mapper)
        {
            _carAuctionContext = carAuctionContext;
            _mapper = mapper;
        }

        public void AddCar(CarDtoForCreation carDtoForCreation, string userId)
        {
            var lot = _mapper.Map<Lot>(carDtoForCreation);
            lot.SellerId = userId;
            lot.CurrentCost = lot.StartingPrice;

            var car = _mapper.Map<Car>(carDtoForCreation);
            car.Lot = lot;
            
            _carAuctionContext.Cars.Add(car);
        }

        public void DeleteLotWithCar(Car car, Lot lot)
        {
            _carAuctionContext.Cars.Remove(car);
            _carAuctionContext.Lots.Remove(lot);
        }

        public async Task<Car> GetCarByUserAsync(int id, string idUser)
        {
            return await _carAuctionContext.Cars.SingleOrDefaultAsync(c => c.Id.Equals(id) && c.Lot.SellerId.Equals(idUser));
        }

        public async Task<IEnumerable<Car>> GetCarsProfileAsync(string id, CarsParametersInProfile carsParametersInProfile)
        {
            var query = _carAuctionContext.Cars.Where(l => l.Lot.SellerId.Equals(id));

            if (carsParametersInProfile.Status != null)
            {
                query = query.Where(l => l.Lot.Status.Equals(carsParametersInProfile.Status));
            }

            var cars = await query.ToListAsync();

            return PagedList<Car>.ToPagedList(cars, carsParametersInProfile.PageNumber, carsParametersInProfile.PageSize);
        }

        public async Task<Lot> GetLotAsync(int id)
        {
            return await _carAuctionContext.Lots.FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public Task SaveAsync()
        {
            return _carAuctionContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Bid>> GetBidsByUserAsync(string userId)
        {
            var bids = await _carAuctionContext.Bids.Where(i => i.BuyerId.Equals(userId)).ToListAsync();

            var distinctBids = bids.GroupBy(x => x.LotId).Select(x => x.Last());

            return distinctBids;
        }
    }
}
