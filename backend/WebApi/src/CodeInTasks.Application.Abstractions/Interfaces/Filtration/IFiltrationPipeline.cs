namespace CodeInTasks.Application.Abstractions.Interfaces.Filtration
{
    public interface IFiltrationPipeline<TFilterDto, TEntity>
    {
        IFiltrationPipelineResult<TEntity> GetResult(TFilterDto filterDto);
    }
}
