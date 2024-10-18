﻿using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfcoreGenericRepository<TEntity> : IRepository<TEntity>
       where TEntity : class
    {
        protected readonly DbContext context;
        public EfcoreGenericRepository(DbContext ctx)
        {
            this.context = ctx;
        }
        public void Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
                context.Set<TEntity>().Remove(entity);
        }

        public List<TEntity> GetAll()
        {
                return context.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
                return context.Set<TEntity>().Find(id);
        }

        public virtual void Update(TEntity entity)
        {
                context.Entry(entity).State = EntityState.Modified;
        }
    }
}