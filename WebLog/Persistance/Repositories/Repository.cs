﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.Repositories;

namespace WebLog.Persistance.Repositories
{

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public virtual T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).AsEnumerable();
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefault(predicate);
        }

        public virtual void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public virtual void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }
    }
}