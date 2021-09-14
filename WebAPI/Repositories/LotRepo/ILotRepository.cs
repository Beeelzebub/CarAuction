using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;
using Enums;

namespace Repositories
{
    public interface ILotRepository : IRepositoryBase<Lot>
    {
        public Task<List<Lot>> GetLotsByStatusAsync(LotStatus status);
    }
}
