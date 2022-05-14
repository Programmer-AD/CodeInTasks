using CodeInTasks.Application.Dtos.Task;

namespace CodeInTasks.Application.Interfaces.Services
{
    public interface ITaskService
    {
        Task<Guid> AddAsync(TaskCreateDto taskCreateDto);
        Task UpdateAsync(TaskUpdateDto taskUpdateDto);
        Task DeleteAsync(Guid taskId);

        Task<TaskViewDto> GetAsync(Guid taskId);
        Task<IEnumerable<TaskViewDto>> GetFilteredAsync(TaskFilterDto filterDto);
    }
}
