using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Entity.Models;
using Enums;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class BidRepository : RepositoryBase<Bid>, IBidRepository
    {
        private readonly CarAuctionContext _bdContext;

        public BidRepository(CarAuctionContext bdContext) : base(bdContext)
        {
            _bdContext = bdContext;
        }

        public Bid GetActiveBid(int lotId) =>
             _bdContext.Bids.FirstOrDefault(b => b.LotId == lotId && b.BidStatus == BidStatus.Active);

        public async Task<List<Bid>> GetListAsync(int lotId) =>
            await _bdContext.Bids.Where(b => b.LotId == lotId).ToListAsync();

        public async Task<List<Bid>> GetBidsByUserAsync(string currentUserId)
        {
            var bids = await _bdContext.Bids.Include(l=>l.Lot)
                .ThenInclude(c=>c.Car)
                .ThenInclude(m=>m.Model)
                .ThenInclude(b=>b.Brand)
                .Where(i => i.BuyerId.Equals(currentUserId)).ToListAsync();

            var distinctBids = bids.GroupBy(x => x.LotId).Select(x => x.Last());

            return distinctBids.ToList();
        }


    }
}
