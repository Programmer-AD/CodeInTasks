using CodeInTasks.Application.Abstractions.Dtos.Task;

namespace CodeInTasks.Application.Abstractions.Interfaces.Services
{
    public interface ITaskService
    {
        Task<Guid> AddAsync(TaskCreateDto taskCreateDto);
        Task UpdateAsync(TaskUpdateDto taskUpdateDto);
        Task DeleteAsync(Guid taskId);

        Task<TaskModel> GetAsync(Guid taskId);
        Task<IEnumerable<TaskModel>> GetFilteredAsync(TaskFilterDto filterDto);
        Task<bool> IsOwnerAsync(Guid taskId, Guid userId);
    }
}
