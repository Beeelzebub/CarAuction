using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepositoryBase<TEntity>
    {
        public Task<TEntity> GetAsync(int id);
        public TEntity Get(int id);
        public Task<List<TEntity>> GetListAsync();
        public Task CreateAsync(TEntity entity);
        public void Delete(TEntity entity);
        public void Update(TEntity entity);
    }
}
