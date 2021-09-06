using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using Entity.Models;

namespace Repositories
{
    public class EntityRepository<TEntity> : RepositoryBase<TEntity>, IEntityRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly CarAuctionContext _bdContext;

        public EntityRepository(CarAuctionContext bdContext) : base(bdContext)
        {
            _bdContext = bdContext;
        }

    }
}
