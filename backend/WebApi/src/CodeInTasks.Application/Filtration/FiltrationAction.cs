namespace CodeInTasks.Application.Filtration
{
    internal delegate void FiltrationAction<TFilterModel, TEntity>(TFilterModel filterModel, FiltrationPipelineResult<TEntity> pipelineResult);
}
