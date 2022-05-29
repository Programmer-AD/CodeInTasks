using System.Linq.Expressions;

namespace CodeInTasks.Application.Filtration
{
    internal class FiltrationPipelineResult<TEntity> : IFiltrationPipelineResult<TEntity>
    {
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderFunction { get; set; } = null;

        public Expression<Predicate<TEntity>> FilterExpression { get; private set; } = null;

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

        private static Expression<Predicate<TEntity>> MergeExpressions(
            Expression<Predicate<TEntity>> baseExpression,
            Expression<Predicate<TEntity>> expressionToMerge)
        {
            var targetParameter = baseExpression.Parameters[0];

            var andExpression = Expression.AndAlso(baseExpression.Body, expressionToMerge.Body);
            var parameterChanger = new ParameterChangerVisitor(targetParameter);
            var bodyExpression = parameterChanger.Visit(andExpression);

            var resultExpression = Expression.Lambda<Predicate<TEntity>>(bodyExpression, baseExpression.Parameters);
            return resultExpression;
        }

        private class ParameterChangerVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression targetParameter;

            public ParameterChangerVisitor(ParameterExpression targetParameter)
            {
                this.targetParameter = targetParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return targetParameter;
            }
        }
    }
}
