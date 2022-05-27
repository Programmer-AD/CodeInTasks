using System.Linq.Expressions;

namespace CodeInTasks.Application.Abstractions.Interfaces.Infrastructure.Persistance
{
    public class RepositoryFilter<T>
    {
        public Expression<Predicate<T>> FiltrationPredicate { get; set; } = null;
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderFunction { get; set; } = null;
        public int Take { get; set; } = 0;
        public int Skip { get; set; } = 0;
        public bool IncludeDeleted { get; set; } = false;
    }
}
