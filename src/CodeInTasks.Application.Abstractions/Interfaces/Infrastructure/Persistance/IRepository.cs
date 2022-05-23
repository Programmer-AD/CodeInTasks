using System.Linq.Expressions;

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

        Task<IEnumerable<T>> GetFilteredAsync(RepositoryFilter<T> filter);

        Task<long> CountAsync(RepositoryFilter<T> filter);

        Task<bool> AnyAsync(Expression<Predicate<T>> predicate, bool includeDeleted = false);
    }
}
