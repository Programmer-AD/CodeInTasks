namespace CodeInTasks.Application.Filtration
{
    internal class FiltrationPipeline<TFilterDto, TEntity> : IFiltrationPipeline<TFilterDto, TEntity>
    {
        private readonly IEnumerable<FiltrationAction<TFilterDto, TEntity>> filtrationActions;

        public FiltrationPipeline(IEnumerable<FiltrationAction<TFilterDto, TEntity>> filtrationActions)
        {
            this.filtrationActions = filtrationActions;
        }

        public IFiltrationPipelineResult<TEntity> GetResult(TFilterDto filterDto)
        {
            var result = new FiltrationPipelineResult<TEntity>();

            foreach (var action in filtrationActions)
            {
                action(filterDto, result);
            }

            return result;
        }
    }
}
