using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;

namespace Repositories
{
    public interface IBidRepository : IRepositoryBase<Bid>
    {
        public Task<Bid> GetActiveBidAsync(int lotId);

        public Task<List<Bid>> GetListAsync(int lotId);
    }
}
