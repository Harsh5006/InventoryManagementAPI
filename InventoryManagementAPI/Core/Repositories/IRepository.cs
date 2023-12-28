using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
        TEntity SingleOrDefault(Func<TEntity, bool> predicate);

        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
