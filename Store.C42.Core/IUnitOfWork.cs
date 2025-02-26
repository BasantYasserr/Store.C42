using Store.C42.Core.Entities;
using Store.C42.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.C42.Core
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();

        //Create Repository<T> And Return it
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
}
