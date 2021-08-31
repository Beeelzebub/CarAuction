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
            var query = _carAuctionContext.Cars
                .Include(m=>m.Model)
                .ThenInclude(b=>b.Brand)
                .Where(l => l.Lot.Status == Status.Approved);

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
       
        public IQueryable<Bid> GetBids(int lotId)
        {
            return _carAuctionContext.Bids.Where(x => x.LotId.Equals(lotId)).AsQueryable();
        }

        public async Task<Lot> GetLotAsync(int id)
        {
            return await _carAuctionContext.Lots.FirstOrDefaultAsync(i => i.Id.Equals(id));
        }

        public async Task<Bid> GetActiveBidAsync(int lotId)
        {
            return await _carAuctionContext.Bids.FirstOrDefaultAsync(b =>
                b.LotId.Equals(lotId) && b.BidStatus.Equals(BidStatus.Active));
        }

        public Task SaveAsync()
        {
            return _carAuctionContext.SaveChangesAsync();
        }

        public void Save()
        {
            _carAuctionContext.SaveChanges();
        }

        public Lot GetLot(int lotId)
        {
            return _carAuctionContext.Lots.FirstOrDefault(i => i.Id.Equals(lotId));
        }

        public Bid GetActiveBid(int lotId)
        {
            return _carAuctionContext.Bids.FirstOrDefault(b =>
                b.LotId.Equals(lotId) && b.BidStatus.Equals(BidStatus.Active));
        }
    }
}
