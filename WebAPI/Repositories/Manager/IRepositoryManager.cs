using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;
using Repositories;

namespace Repositories
{
    public interface IRepositoryManager
    {
        ICarRepository Car { get; }
        ILotRepository Lot { get; }
        IBidRepository Bid { get; }
        IEntityRepository<TEntity> GetRepositoryByEntity<TEntity>() where TEntity : class, IEntity, new();
        Task SaveAsync();
    }
}
