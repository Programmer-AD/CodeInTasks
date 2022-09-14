namespace CodeInTasks.Application.Filtration
{
    internal class FiltrationPipeline<TFilterModel, TEntity> : IFiltrationPipeline<TFilterModel, TEntity>
    {
        private readonly IEnumerable<FiltrationAction<TFilterModel, TEntity>> filtrationActions;

        public FiltrationPipeline(IEnumerable<FiltrationAction<TFilterModel, TEntity>> filtrationActions)
        {
            this.filtrationActions = filtrationActions;
        }

        public IFiltrationPipelineResult<TEntity> GetResult(TFilterModel filterModel)
        {
            var result = new FiltrationPipelineResult<TEntity>();

            foreach (var action in filtrationActions)
            {
                action(filterModel, result);
            }

            return result;
        }
    }
}
