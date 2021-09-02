using System;
using System.Collections.Generic;
using System.Text;
using Entity.Models;

namespace Repositories
{
    public interface IEntityRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity
    {

    }
}
