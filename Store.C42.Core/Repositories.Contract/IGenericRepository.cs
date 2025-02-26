using Store.C42.Core.Entities;
using Store.C42.Core.Specifications;
using Store.C42.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.C42.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(TKey id);
        
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> specifications);
        Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, TKey> specifications);
        Task<int> GetCountAsync(ISpecifications<TEntity, TKey> specifications);

        Task AddAsyAsync(TEntity entity);

        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
