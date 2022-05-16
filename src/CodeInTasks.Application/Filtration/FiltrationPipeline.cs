using CodeInTasks.Application.Interfaces.Filtration;

namespace CodeInTasks.Application.Filtration
{
    internal class FiltrationPipeline<TFilterDto, TEntity> : IFiltrationPipeline<TFilterDto, TEntity>
    {
        private readonly IEnumerable<FiltrationAction<TFilterDto, TEntity>> filtrationActions;

        public FiltrationPipeline(IEnumerable<FiltrationAction<TFilterDto, TEntity>> filtrationActions)
        {
            this.filtrationActions = filtrationActions;
        }

        public FiltrationPipelineResult<TEntity> GetResult(TFilterDto filterDto)
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
