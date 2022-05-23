﻿using System.Linq.Expressions;
using CodeInTasks.Domain.Models;

namespace CodeInTasks.Application.Abstractions.Interfaces.Infrastructure.Persistance
{
    public interface IRepository<T>
        where T : ModelBase
    {
        Task<Guid> AddAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task UpdateAsync(T entity);

        Task<T> GetAsync(Guid id, bool includeDeleted = false);
        Task<T> GetAsync(Expression<Predicate<T>> predicate, bool includeDeleted = false);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Predicate<T>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderFunc = null,
            int take = 0,
            int skip = 0,
            bool includeDeleted = false);

        Task<long> CountAsync(
            Expression<Predicate<T>> predicate = null,
            int take = 0,
            int skip = 0,
            bool includeDeleted = false);

        Task<bool> AnyAsync(Expression<Predicate<T>> predicate, bool includeDeleted = false);
    }
}