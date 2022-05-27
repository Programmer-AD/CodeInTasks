namespace CodeInTasks.Application.Filtration
{
    internal delegate void FiltrationAction<TFilterDto, TEntity>(TFilterDto filterDto, FiltrationPipelineResult<TEntity> pipelineResult);
}
