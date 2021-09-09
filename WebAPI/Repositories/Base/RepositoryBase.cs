using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Entity.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly CarAuctionContext _dbContext;

        protected RepositoryBase(CarAuctionContext bdContext)
        {
            _dbContext = bdContext;
        }

        public virtual async Task<TEntity> GetAsync(int id) =>
            await _dbContext.Set<TEntity>().FindAsync(id);

        public virtual TEntity Get(int id) =>
             _dbContext.Set<TEntity>().Find(id);

        public virtual async Task<IEnumerable<TEntity>> GetListAsync() =>
            await _dbContext.Set<TEntity>().ToListAsync();

        public virtual async Task CreateAsync(TEntity entity) =>
            await _dbContext.Set<TEntity>().AddAsync(entity);

        public virtual void Delete(TEntity entity) =>
            _dbContext.Set<TEntity>().Remove(entity);

        public virtual void Update(TEntity entity) =>
            _dbContext.Set<TEntity>().Update(entity);
    }
}
