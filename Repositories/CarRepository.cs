using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entity;
using Entity.Models;
using Entity.RequestFeatures;
using LinqKit;
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
            var predicate = PredicateBuilder.New<Car>( l => l.Lot.Status == Status.Approved);
            if (!string.IsNullOrEmpty(carParameters.Brand))
            {
                predicate = predicate.And(l => l.Model.Brand.BrandName == carParameters.Brand && l.Lot.Status == Status.Approved);
            }
            if (!string.IsNullOrEmpty(carParameters.Model))
            {
                predicate = predicate.And(l => l.Model.Name == carParameters.Model && l.Lot.Status == Status.Approved);
            }

            var cars = await _carAuctionContext.Cars.Where(predicate).ToListAsync();
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
