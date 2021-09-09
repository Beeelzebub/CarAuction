using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entity;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class LotRepository : RepositoryBase<Lot>, ILotRepository
    {
        private readonly CarAuctionContext _bdContext;
        private readonly IMapper _mapper;

        public LotRepository(CarAuctionContext bdContext, IMapper mapper) : base(bdContext)
        {
            _bdContext = bdContext;
            _mapper = mapper;
        }

        public async Task<List<Lot>> GetLotsByStatusAsync(LotStatus status) =>
            await _bdContext.Lots.Include(l => l.Car).Where(l => l.Status == status).ToListAsync();

        public override async Task<Lot> GetAsync(int id) =>
            await _bdContext.Lots.Include(l => l.Car).FirstOrDefaultAsync(l => l.Id == id);
    }
}
