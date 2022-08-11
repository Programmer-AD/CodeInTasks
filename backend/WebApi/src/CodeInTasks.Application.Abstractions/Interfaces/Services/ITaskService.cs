using CodeInTasks.WebApi.Models.Task;

namespace CodeInTasks.Application.Abstractions.Interfaces.Services
{
    public interface ITaskService
    {
        Task<Guid> AddAsync(TaskCreateModel taskCreateModel);
        Task UpdateAsync(TaskUpdateModel taskUpdateModel);
        Task DeleteAsync(Guid taskId);

        Task<TaskModel> GetAsync(Guid taskId);
        Task<IEnumerable<TaskModel>> GetFilteredAsync(TaskFilterModel filterModel);
        Task<bool> IsOwnerAsync(Guid taskId, Guid userId);
    }
}
