using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Entity;
using Entity.Models;

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
        
    }
}
