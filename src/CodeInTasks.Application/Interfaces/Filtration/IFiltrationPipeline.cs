using CodeInTasks.Application.Filtration;

namespace CodeInTasks.Application.Interfaces.Filtration
{
    internal interface IFiltrationPipeline<TFilterDto, TEntity>
    {
        FiltrationPipelineResult<TEntity> GetResult(TFilterDto filterDto);
    }
}
