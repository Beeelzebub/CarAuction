using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entity;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly CarAuctionContext _carAuctionContext;

        public AdminRepository(CarAuctionContext carAuctionContext)
        {
            _carAuctionContext = carAuctionContext;
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _carAuctionContext.Cars.SingleOrDefaultAsync(i => i.Id.Equals(id) && i.Lot.Status.Equals(Status.Pending));
        }

        public async Task<IEnumerable<Car>> GetCarsByStatusAsync(Status status)
        {
            return await _carAuctionContext.Cars.Where(i => i.Lot.Status.Equals(status)).ToListAsync();
        }

        public async Task<Lot> GetLotAsync(int id)
        {
            return await _carAuctionContext.Lots.SingleOrDefaultAsync(i=>i.Id.Equals(id));
        }

        public Task SaveAsync()
        {
            return _carAuctionContext.SaveChangesAsync();
        }
    }
}
