using InventoryManagementAPI.Core.Repositories;
using InventoryManagementAPI.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementAPI.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IdentityDbContext Context;

        public Repository(IdentityDbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public TEntity SingleOrDefault(Func<TEntity, bool> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }
    }
}
