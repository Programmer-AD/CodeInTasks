namespace CodeInTasks.Application.Abstractions.Interfaces.Filtration
{
    public interface IFiltrationPipeline<TFilterModel, TEntity>
    {
        IFiltrationPipelineResult<TEntity> GetResult(TFilterModel filterModel);
    }
}
