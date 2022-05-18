using CodeInTasks.Application.Abstractions.Dtos.Task;

namespace CodeInTasks.Application.Abstractions.Interfaces.Services
{
    public interface ITaskService
    {
        Task<Guid> AddAsync(TaskCreateDto taskCreateDto);
        Task UpdateAsync(TaskUpdateDto taskUpdateDto);
        Task DeleteAsync(Guid taskId);

        Task<TaskViewDto> GetAsync(Guid taskId);
        Task<IEnumerable<TaskViewDto>> GetFilteredAsync(TaskFilterDto filterDto);
        Task<bool> IsOwnerAsync(Guid taskId, Guid userId);
    }
}
