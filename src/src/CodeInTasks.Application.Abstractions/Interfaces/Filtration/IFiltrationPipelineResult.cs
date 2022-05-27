using System.Linq.Expressions;

namespace CodeInTasks.Application.Abstractions.Interfaces.Filtration
{
    public interface IFiltrationPipelineResult<TEntity>
    {
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderFunction { get; }

        Expression<Predicate<TEntity>> FilterExpression { get; }
    }
}
