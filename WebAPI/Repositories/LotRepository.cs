using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Entity.Models;

namespace Repositories
{
    public  class LotRepository : RepositoryBase<Lot>, ILotRepository
    {
        private readonly CarAuctionContext _bdContext;

        public LotRepository(CarAuctionContext bdContext) : base(bdContext)
        {
            _bdContext = bdContext;
        }

        public async Task<List<Lot>> GetLotsByStatusAsync(LotStatus status) =>
            await _bdContext.Lots.Where(l => l.Status == status).ToListAsync();


    }
}
