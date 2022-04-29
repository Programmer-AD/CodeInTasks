using System.Linq.Expressions;

namespace CodeInTasks.Infrastructure
{
    internal static class ExpressionConvertExtensions
    {
        public static Expression<Func<T, bool>> ConvertToFunc<T>(this Expression<Predicate<T>> predicate)
        {
            var convertedPredicate = Expression.Lambda<Func<T, bool>>(predicate.Body, predicate.Parameters);
            return convertedPredicate;
        }
    }
}