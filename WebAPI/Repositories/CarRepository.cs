using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Entity.Models;
using Entity.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        private readonly CarAuctionContext _bdContext;

        public CarRepository(CarAuctionContext bdContext) : base(bdContext)
        {
            _bdContext = bdContext;
        }
        
    }
}
