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

        public void AddBid(int lotId, string userId)
        {
            var bid = new Bid
            {
                LotId = lotId,
                BuyerId = userId,
                BidStatus = BidStatus.Active
            };
            _carAuctionContext.Bids.Add(bid);
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _carAuctionContext.Cars.SingleOrDefaultAsync(c => c.Id.Equals(id) && c.Lot.Status.Equals(Status.Approved));
        }

        public async Task<IEnumerable<Car>> GetCarsAsync(CarParameters carParameters)
        {
            var cars = await _carAuctionContext.Cars.Where(i => i.Lot.Status.Equals(Status.Approved)).ToListAsync();
            return PagedList<Car>.ToPagedList(cars, carParameters.PageNumber, carParameters.PageSize);
            
        }

        public async Task<IEnumerable<Car>> GetCarsByConditionAsync(CarParameters carParameters)
        {
            var cars = await _carAuctionContext.Cars.Where(c => (c.Year >= carParameters.MinYear && c.Year <= carParameters.MaxYear)
                                                                && c.Model.Name.Equals(carParameters.Model)
                                                                && c.Model.Brand.BrandName.Equals(carParameters.Brand)
                                                                && c.Lot.Status.Equals(Status.Approved)).ToListAsync();
            return PagedList<Car>.ToPagedList(cars, carParameters.PageNumber, carParameters.PageSize);
        }

        public IQueryable<Bid> GetListBids(int id)
        {
            return _carAuctionContext.Bids.Where(x => x.LotId.Equals(id)).AsQueryable();
            
        }

        public async Task<Lot> GetLotAsync(int id)
        {
            return await _carAuctionContext.Lots.SingleOrDefaultAsync(i => i.Id.Equals(id));
        }

        public void Save()
        {
             _carAuctionContext.SaveChanges();
        }
    }
}
