using CodeInTasks.Application.Dtos.Task;

namespace CodeInTasks.Application.Filtration.FiltrationActions
{
    internal static class TaskFiltrationActions
    {
        internal static readonly IEnumerable<FiltrationAction<TaskFilterDto, TaskModel>> Actions 
            = new FiltrationAction<TaskFilterDto, TaskModel>[] {  };

    }
}
