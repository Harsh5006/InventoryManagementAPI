using System;
using System.Collections;
using System.Collections.Generic;

namespace InventoryManagementAPI.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

        TEntity SingleOrDefault(Func<TEntity, bool> predicate);

        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
