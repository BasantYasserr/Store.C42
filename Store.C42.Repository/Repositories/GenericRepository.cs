using Microsoft.EntityFrameworkCore;
using Store.C42.Core.Entities;
using Store.C42.Core.Repositories.Contract;
using Store.C42.Core.Specifications;
using Store.C42.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.C42.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> specifications)
        {
            return SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specifications);
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if(typeof(TEntity) == typeof(Product))
            {
                return (IEnumerable<TEntity>) await _context.Products.OrderBy(P => P.Name).Include(P => P.Brand).Include(P => P.Type).ToListAsync();
            }
             
            return await _context.Set<TEntity>().ToListAsync();  
        }
        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
            }

            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsyAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
             _context.Update(entity);
        }
        public void Delete(TEntity entity)
        {
             _context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> specifications)
        {
             return await ApplySpecifications(specifications).ToListAsync();   //the "ToListAsync()" to cast from IQuerable to IEnumerable
        }
        public async Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await ApplySpecifications(specifications).FirstOrDefaultAsync();
        }

        public Task<int> GetCountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return ApplySpecifications(specifications).CountAsync();
        }
    }
}
