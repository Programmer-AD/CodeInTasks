using System.Linq.Expressions;

namespace CodeInTasks.Application.Filtration
{
    internal class FiltrationPipelineResult<TEntity>
    {
        public Expression<Predicate<TEntity>> FilterExpression { get; private set; } = null;
        public Expression<Func<TEntity, IComparable>> OrderExpression { get; set; } = null;

        public void AddFilter(Expression<Predicate<TEntity>> newFilterExpression)
        {
            if (FilterExpression == null)
            {
                FilterExpression = newFilterExpression;
            }
            else
            {
                FilterExpression = MergeExpressions(FilterExpression, newFilterExpression);
            }
        }

        private Expression<Predicate<TEntity>> MergeExpressions(
            Expression<Predicate<TEntity>> baseExpression,
            Expression<Predicate<TEntity>> expressionToMerge)
        {
            var andExpression = Expression.AndAlso(baseExpression.Body, expressionToMerge.Body);
            var resultExpression = Expression.Lambda<Predicate<TEntity>>(andExpression, baseExpression.Parameters);
            return resultExpression;
        }
    }
}
