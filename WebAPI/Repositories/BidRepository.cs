using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Entity.Models;
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

        public async Task<Bid> GetActiveBidAsync(int lotId) =>
            await _bdContext.Bids.FirstOrDefaultAsync(b => b.LotId == lotId && b.BidStatus == BidStatus.Active);

        public async Task<List<Bid>> GetListAsync(int lotId) =>
            await _bdContext.Bids.Where(b => b.LotId == lotId).ToListAsync();
        public async Task<List<Bid>> GetBidsByUserAsync(string currentUserId)
        {
            var bids = await _bdContext.Bids.Where(i => i.BuyerId.Equals(currentUserId)).ToListAsync();

            var distinctBids = bids.GroupBy(x => x.LotId).Select(x => x.Last());

            return distinctBids.ToList();
        }


    }
}
