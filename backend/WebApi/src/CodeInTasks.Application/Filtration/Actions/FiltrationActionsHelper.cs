using System.Reflection;

namespace CodeInTasks.Application.Filtration.Actions
{
    internal static class FiltrationActionsHelper
    {
        /// <summary>
        /// Gets all actions that are "public", "static" and match <see cref="FiltrationAction{TFilterModel, TEntity}"/>
        /// </summary>
        /// <param name="containerType">Type from which to get methods</param>
        public static IEnumerable<FiltrationAction<TFilterModel, TEntity>> GetActions<TFilterModel, TEntity>(Type containerType)
        {
            var resultDelegateType = typeof(FiltrationAction<TFilterModel, TEntity>);

            var result = containerType
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(x => Delegate.CreateDelegate(resultDelegateType, x, throwOnBindFailure: false))
                .OfType<FiltrationAction<TFilterModel, TEntity>>()
                .ToArray();

            return result;
        }
    }
}
