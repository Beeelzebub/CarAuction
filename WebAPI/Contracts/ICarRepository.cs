using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Models;
using Entity.RequestFeatures;

namespace Contracts
{
    public interface ICarRepository
    { 
        Task<IEnumerable<Car>> GetCarsAsync(CarParameters carParameters);
        Task<Car> GetCarAsync(int id);
        Task<Lot> GetLotAsync(int id);
        IQueryable<Bid> GetBids(int id);
        Task<Bid> GetActiveBidAsync(int lotId);
        Task SaveAsync();
        Bid GetActiveBid(int lotId);
        void Save();
        Lot GetLot(int lotId);
        void AddBid(int lotId, string userId);

    }
}
