﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CodeInTasks.Infrastructure.Persistance.EF
{
    internal class EfGenericRepository<T> : IRepository<T>
        where T : ModelBase
    {
        private readonly DbSet<T> dbSet;

        public EfGenericRepository(DbContext dbContext)
        {
            dbSet = dbContext.Set<T>();
        }

        public Task<Guid> AddAsync(T entity)
        {
            var id = Guid.NewGuid();
            dbSet.Add(entity);
            return Task.FromResult(id);
        }

        public async Task<long> CountAsync(
            Expression<Predicate<T>> predicate = null,
            int take = 0,
            int skip = 0,
            bool includeDeleted = false)
        {
            var source = GetSetupedSource(predicate, orderFunc: null, take, skip, includeDeleted);
            var result = await source.LongCountAsync();
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await dbSet.FindAsync(id);
            var entityExists = entity != null;

            if (entityExists)
            {
                entity.IsDeleted = true;
                dbSet.Update(entity);
            }

            return entityExists;
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Predicate<T>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderFunc = null,
            int take = 0,
            int skip = 0,
            bool includeDeleted = false)
        {
            var source = GetSetupedSource(predicate, orderFunc, take, skip, includeDeleted);
            var result = await source.ToListAsync();
            return result;
        }

        public Task<T> GetAsync(Guid id, bool includeDeleted = false)
        {
            return GetAsync(x => x.Id == id, includeDeleted);
        }

        public Task<T> GetAsync(Expression<Predicate<T>> predicate, bool includeDeleted = false)
        {
            var convertedPredicate = predicate.ConvertToFunc();
            var source = GetSource(includeDeleted);
            var resultTask = source.FirstOrDefaultAsync(convertedPredicate);
            return resultTask;
        }

        public Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task<bool> AnyAsync(Expression<Predicate<T>> predicate, bool includeDeleted)
        {
            var source = GetSource(includeDeleted);
            var convertedPredicate = predicate.ConvertToFunc();
            var resultTask = source.AnyAsync(convertedPredicate);
            return resultTask;
        }

        private IQueryable<T> GetSource(bool includeDeleted)
        {
            var source = (IQueryable<T>)dbSet;

            if (!includeDeleted)
            {
                source = source.Where(x => !x.IsDeleted);
            }

            return source;
        }

        private IQueryable<T> GetSetupedSource(
            Expression<Predicate<T>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderFunc,
            int take,
            int skip,
            bool includeDeleted)
        {
            var source = GetSource(includeDeleted);

            if (predicate != null)
            {
                var convertedPredicate = predicate.ConvertToFunc();
                source = source.Where(convertedPredicate);
            }
            if (orderFunc != null)
            {
                source = orderFunc(source);
            }
            if (skip > 0)
            {
                source = source.Skip(skip);
            }
            if (take > 0)
            {
                source = source.Take(take);
            }

            return source;
        }
    }
}
